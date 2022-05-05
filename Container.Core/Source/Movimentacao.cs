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
    public class MovimentacaoCore
    {
        public MovimentacaoCore(){}

        protected MovimentacaoDataAccess DataAccess { get { return new MovimentacaoDataAccess(); } }

        public Int32 Count(string search, DateTime? searchDate = null)
        {
            return DataAccess.Count(search, searchDate);
        }
        
        public List<Movimentacao> List(Int32? page, string propertyName, bool sortAsc, string search, DateTime? searchDate = null)
        {
            return DataAccess.List(page, propertyName, sortAsc, search, searchDate);
        }

     
        public void Insert(Movimentacao dto)
        {
            DataAccess.Insert(dto);
        }

        public Movimentacao GetByPK(Guid id)
        {
            return DataAccess.GetByPK(id);
        }

        public void Update(Movimentacao dto)
        {
            DataAccess.Update(dto);
        }

        public void Delete(Guid id)
        {
            DataAccess.Delete(id);
        }

    }
}
