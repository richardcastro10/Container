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
    public class ClienteCore
    {
        public ClienteCore(){}

        protected ClienteDataAccess DataAccess { get { return new ClienteDataAccess(); } }

        public Int32 Count(string search)
        {
            return DataAccess.Count(search);
        }
        
        public List<Cliente> List(Int32 page, bool sortAsc, string search)
        {
            return DataAccess.List(page, sortAsc, search);
        }

     
        public void Insert(Cliente dto)
        {
            DataAccess.Insert(dto);
        }

        public Cliente GetByPK(Guid id)
        {
            return DataAccess.GetByPK(id);
        }

        public void Update(Cliente dto)
        {
            DataAccess.Update(dto);
        }

        public void Delete(Guid id)
        {
            DataAccess.Delete(id);
        }

    }
}
