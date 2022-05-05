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
    [RoutePrefix("api/clientes")]
    public class ClienteController : BaseController
    {
        [HttpGet]
        [Route("")]
        public JsonResult<List<ClienteModel>> Get(int page = 0, bool orderAsc = true, string search = null)
        {
            using (CoreController controller = new CoreController())
            {
                var listDto = controller.ClienteCore.List(page, orderAsc, search);
                return Json(listDto.Select(dto => ClienteModel.Create(dto)).ToList());
            }
        }
        
        [HttpGet]
        [Route("total")]
        public JsonResult<object> GetTotal(string search = null)
        {
            using (CoreController controller = new CoreController())
            {
                var count = controller.ClienteCore.Count(search);
                return Json<object>(new { total = count });
            }
        }
        
        [HttpPost]
        [Route("")]
        public void Post(ClienteModel model)
        {
            using (CoreController controller = new CoreController())
            {
                controller.ClienteCore.Insert(model.Convert());
            }
        } 
        
        [HttpPut]
        [Route("{id:guid}")]
        public void Put(Guid id, ClienteModel model)
        {
            using (CoreController controller = new CoreController())
            {
                controller.ClienteCore.Update(model.Convert());
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        public JsonResult<ClienteModel> GetByPK(Guid id)
        {
            using (CoreController controller = new CoreController())
            {
                var dto = controller.ClienteCore.GetByPK(id);
                return Json(ClienteModel.Create(dto));
            }
        }
        
        [HttpDelete]
        [Route("{id:guid}")]
        public void Delete(Guid id)
        {
            using (CoreController controller = new CoreController())
            {
                controller.ClienteCore.Delete(id);
            }
        }
    }
}