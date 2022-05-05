using Container.Core;
using Container.Presentation.Models;
using Container.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace Container.Presentation.Controllers
{
    [RoutePrefix("api/dashboard")]
    public class DashboardController : BaseController
    {
        [HttpGet]
        [Route("")]
        public JsonResult<DashboardModel> List()
        {
            using (CoreController controller = new CoreController())
            {
                var listDto = controller.MovimentacaoCore.List(null, MovimentacaoModel.GetPropertyName(nameof(MovimentacaoModel.start)), true, null);
                var listContainers = listDto.Select(i => i.Container);

                var grouped = listDto.GroupBy(d => new { tipo = d.Tipo, cliente = d.Container.Cliente })
                    .Select(group => new
                    {
                        Metric = group.Key,
                        TypeDescription = ((EnumTipoMovimentacao)group.Key.tipo).GetStringValue(),
                        ClientName = group.Key.cliente.Nome,
                        Count = group.Count()
                    }).ToList();

                var model = new DashboardModel()
                {
                    relatorios = grouped.Select(d => new MovimentacaoRelatorio() { 
                        clienteName = d.ClientName,
                        description = d.TypeDescription,
                        quantity = d.Count
                    }).ToList(),
                    totalExport = listContainers.Where(i => i.Categoria == (Int32)EnumTipoCategoria.Exportacao).Count(),
                    totalImport = listContainers.Where(i => i.Categoria == (Int32)EnumTipoCategoria.Importacao).Count()
                };

                return Json(model);
            } 
        }

        [HttpGet]
        [Route("panel")]
        public JsonResult<object> ListPanelData()
        {
            using (CoreController controller = new CoreController())
            {
                var listDto = controller.MovimentacaoCore.List(null, MovimentacaoModel.GetPropertyName(nameof(MovimentacaoModel.start)), true, null);

                var grouped = listDto.GroupBy(d => new { tipo = d.Container.Categoria, date = d.Inicio.Date })
                    .Select(group => new
                    {
                        Metric = group.Key,
                        Type = group.Key.tipo,
                        Date = group.Key.date,
                        Count = group.Count()
                    }).OrderBy(d => d.Date).ToList();

                List<Int32> countImport = new List<int>();
                List<Int32> countExport = new List<int>();

                var distinctDates = grouped.Select(d => d.Date).Distinct().ToList();

                foreach (DateTime date in distinctDates)
                {
                    countImport.Add(grouped.Where(d => d.Type == (Int32)EnumTipoCategoria.Importacao && d.Date == date).Sum(d => d.Count));
                    countExport.Add(grouped.Where(d => d.Type == (Int32)EnumTipoCategoria.Exportacao && d.Date == date).Sum(d => d.Count));

                }
                return Json<object>(new { 
                    dates = distinctDates.Select(d => d.ToString("d")),
                    countImport = countImport,
                    countExport = countExport
                });
            }
        }
    }
}