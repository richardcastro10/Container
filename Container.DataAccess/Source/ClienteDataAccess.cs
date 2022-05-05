using Container.DataAccess.DatabaseModels;
using Container.Shared;
using Container.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Container.DataAccess.Source
{
    public class ClienteDataAccess : IClienteDataAccesscs
    {
        public ClienteDataAccess() {}

        public Cliente GetByPK(Guid id)
        {
            using (var context = new ContainerEntities())
            {
                return context.Cliente.FirstOrDefault(c => c.Id == id);
            }
        }

        public List<Cliente> List(Int32 page, bool sortAsc, string search)
        {
            using (var context = new ContainerEntities())
            {
                var query = context.Set<Cliente>().AsQueryable();

                if (!String.IsNullOrWhiteSpace(search))
                {
                    query = query.Where(i => i.Nome.Contains(search)
                    || i.Documento.Contains(search)
                    || i.Descricao.Contains(search));
                }

                return query.SetOrderAndPage(page, nameof(Cliente.Nome), sortAsc).ToList();
            }
        }

        public Int32 Count(string search)
        {
            using (var context = new ContainerEntities())
            {
                var query = context.Set<Cliente>().AsQueryable();

                if (!String.IsNullOrWhiteSpace(search))
                {
                    query = query.Where(i => i.Nome.Contains(search)
                    || i.Documento.Contains(search)
                    || i.Descricao.Contains(search));
                }
                return query.Count();
            }
        }


        public void Insert(Cliente dto)
        {
            using (var context = new ContainerEntities())
            {
                context.Set<Cliente>().Add(dto);
                context.SaveChanges();
            }
        }

        public void Update(Cliente dto)
        {
            using (var context = new ContainerEntities())
            {
                var oldDto = context.Set<Cliente>().FirstOrDefault(c => c.Id == dto.Id);

                if (oldDto == null)
                    throw new Exception("Cliente não encontrado");

                oldDto = dto;
                context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            using (var context = new ContainerEntities())
            {
                var dto = context.Set<Cliente>().FirstOrDefault(c => c.Id == id);

                if (dto == null)
                    throw new Exception("Erro ao deletar cliente");
                context.Cliente.Remove(dto);
                context.SaveChanges();
            }
        }     
    }

    //public static class ClienteDAExt
    //{
    //    public static IQueryable<Cliente> SetOrderAndPage(this IQueryable<Cliente> query, Int32? page, bool sortAsc)
    //    {
    //        if (sortAsc)
    //            query = query.OrderBy(x => x.Nome);
    //        else
    //            query = query.OrderByDescending(x => x.Nome);

    //        return page.HasValue ? query.Skip(page.Value * 20).Take(20) : query;
    //    }
    //}
}
