using Container.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Container.Presentation.Models
{
    public class DashboardModel
    {
        public List<MovimentacaoRelatorio> relatorios { get; set; }
        public Int32 totalImport { get; set; }
        public Int32 totalExport { get; set; }      
    }

    public class MovimentacaoRelatorio
    {
       public string clienteName { get; set; }
       public string description { get; set; }
       public Int32 quantity { get; set; }

    }
}