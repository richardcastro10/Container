using Container.DataAccess.DatabaseModels;
using Container.Shared;
using Container.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Container.DataAccess.Source
{
    public class MovimentacaoDataAccess
    {
        public MovimentacaoDataAccess() { }

        public Movimentacao GetByPK(Guid id)
        {
            using (var context = new ContainerEntities())
            {
                return context.Movimentacao.Include(c => c.Container.Cliente).FirstOrDefault(c => c.Id == id);
            }
        }

        public List<Movimentacao> List(Int32? page, string propertyName, bool sortAsc, string search, DateTime? searchDate)
        {
            using (var context = new ContainerEntities())
            {
                var query = context.Set<Movimentacao>().Include(c => c.Container.Cliente).AsQueryable();

                if (searchDate.HasValue)
                {
                    query = query.Where(i => DbFunctions.TruncateTime(i.Inicio) == DbFunctions.TruncateTime(searchDate.Value) || DbFunctions.TruncateTime(i.Fim.Value) == DbFunctions.TruncateTime(searchDate.Value));
                }

                if (!String.IsNullOrWhiteSpace(search))
                {
                    query = query.Where(i => i.Container.Numero.Contains(search) || i.Container.Cliente.Nome.Contains(search));
                }

                return query.SetOrderAndPage(page, propertyName, sortAsc).ToList();
            }
        }

        public Int32 Count(string search, DateTime? searchDate = null)
        {
            using (var context = new ContainerEntities())
            {
                var query = context.Set<Movimentacao>().AsQueryable();

                if (searchDate.HasValue)
                {
                    query = query.Where(i => DbFunctions.TruncateTime(i.Inicio) == DbFunctions.TruncateTime(searchDate.Value) || DbFunctions.TruncateTime(i.Fim.Value) == DbFunctions.TruncateTime(searchDate.Value));
                }

                if (!String.IsNullOrWhiteSpace(search))
                {
                    query = query.Where(i => i.Container.Numero.Contains(search) || i.Container.Cliente.Nome.Contains(search));
                }

                return query.Count();
            }
        }


        public void Insert(Movimentacao dto)
        {
            using (var context = new ContainerEntities())
            {
                context.Set<Movimentacao>().Add(dto);
                context.SaveChanges();
            }
        }

        public void Update(Movimentacao dto)
        {
            using (var context = new ContainerEntities())
            {
                context.Set<Movimentacao>().AddOrUpdate(dto);
                context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            using (var context = new ContainerEntities())
            {
                var dto = context.Set<Movimentacao>().FirstOrDefault(c => c.Id == id);

                if (dto == null)
                    throw new Exception("Erro ao deletar cliente");
                context.Movimentacao.Remove(dto);
                context.SaveChanges();
            }
        }
    }
}
