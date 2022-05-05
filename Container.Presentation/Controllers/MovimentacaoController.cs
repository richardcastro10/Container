using Container.Core;
using Container.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace Container.Presentation.Controllers
{
    [RoutePrefix("api/movimentacao")]
    public class MovimentacaoController : BaseController
    {
        [HttpGet]
        [Route("")]
        public JsonResult<List<MovimentacaoModel>> List(int page = 0, string orderBy = null, bool orderAsc = true, string search = null, DateTime ? searchDate = null)
        {
            using (CoreController controller = new CoreController())
            {
                var listDto = controller.MovimentacaoCore.List(page, MovimentacaoModel.GetPropertyName(orderBy), orderAsc, search, searchDate);
                return Json(listDto.Select(dto => MovimentacaoModel.Create(dto)).ToList());
            }
        }
        
        [HttpGet]
        [Route("total")]
        public JsonResult<object> GetTotal(string search = null, DateTime? searchDate = null)
        {
            using (CoreController controller = new CoreController())
            {
                var count = controller.MovimentacaoCore.Count(search, searchDate);
                return Json<object>(new { total = count });
            }
        }
        
        [HttpPost]
        [Route("")]
        public void Post(MovimentacaoModel model)
        {
            using (CoreController controller = new CoreController())
            {
                controller.MovimentacaoCore.Insert(model.Convert());
            }
        } 
        
        [HttpPut]
        [Route("{id:guid}")]
        public void Put(Guid id, MovimentacaoModel model)
        {
            using (CoreController controller = new CoreController())
            {
                controller.MovimentacaoCore.Update(model.Convert());
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        public JsonResult<MovimentacaoModel> GetByPK(Guid id)
        {
            using (CoreController controller = new CoreController())
            {
                var dto = controller.MovimentacaoCore.GetByPK(id);
                return Json(MovimentacaoModel.Create(dto));
            }
        }
        
        [HttpDelete]
        [Route("{id:guid}")]
        public void Delete(Guid id)
        {
            using (CoreController controller = new CoreController())
            {
                controller.MovimentacaoCore.Delete(id);
            }
        }
    }
}