using Container.DataAccess.DatabaseModels;
using Container.DataAccess.Source;
using Container.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Container.Core
{
    public class ContainerCore
    {
        public ContainerCore(){}

        protected ContainerDataAccess DataAccess { get { return new ContainerDataAccess(); } }

        public Int32 Count(string search)
        {
            return DataAccess.Count(search);
        }
        
        public List<Shared.DTOs.Container> List(Int32? page, string propertyName, bool sortAsc, string search)
        {
            return DataAccess.List(page, propertyName, sortAsc, search);
        }

     
        public void Insert(Shared.DTOs.Container dto)
        {
            DataAccess.Insert(dto);
        }

        public Shared.DTOs.Container GetByPK(Guid id)
        {
            return DataAccess.GetByPK(id);
        }

        public void Update(Shared.DTOs.Container dto)
        {
            DataAccess.Update(dto);
        }

        public void Delete(Guid id)
        {
            DataAccess.Delete(id);
        }

    }
}
