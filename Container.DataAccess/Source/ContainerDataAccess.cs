using Container.DataAccess.DatabaseModels;
using Container.Shared;
using Container.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Container.DataAccess.Source
{
    public class ContainerDataAccess
    {
        public ContainerDataAccess() {}

        public Shared.DTOs.Container GetByPK(Guid id)
        {
            using (var context = new ContainerEntities())
            {
                return context.Container.Include(a => a.Cliente).FirstOrDefault(c => c.Id == id);
            }
        }

        public List<Shared.DTOs.Container> List(Int32? page, string propertyName, bool sortAsc, string search)
        {
            using (var context = new ContainerEntities())
            {
                var query = context.Set<Shared.DTOs.Container>().Include(a => a.Cliente).AsQueryable();

                if (!String.IsNullOrWhiteSpace(search))
                {
                    query = query.Where(i => i.Numero.Contains(search) || i.Cliente.Nome.Contains(search));
                }

                return query.SetOrderAndPage(page, propertyName, sortAsc).ToList();
            }
        }

        public Int32 Count(string search)
        {
            using (var context = new ContainerEntities())
            {
                var query = context.Set<Shared.DTOs.Container>().AsQueryable();

                if (!String.IsNullOrWhiteSpace(search))
                {
                    query = query.Where(i => i.Numero.Contains(search) || i.Cliente.Nome.Contains(search));
                }
                return query.Count();
            }
        }


        public void Insert(Shared.DTOs.Container dto)
        {
            using (var context = new ContainerEntities())
            {
                context.Set<Shared.DTOs.Container>().Add(dto);
                context.SaveChanges();
            }
        }

        public void Update(Shared.DTOs.Container dto)
        {
            using (var context = new ContainerEntities())
            {
                context.Set<Shared.DTOs.Container>().AddOrUpdate(dto);
                context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            using (var context = new ContainerEntities())
            {
                var dto = context.Set<Shared.DTOs.Container>().FirstOrDefault(c => c.Id == id);

                if (dto == null)
                    throw new Exception("Erro ao deletar cliente");
                context.Container.Remove(dto);
                context.SaveChanges();
            }
        }     
    }
}
