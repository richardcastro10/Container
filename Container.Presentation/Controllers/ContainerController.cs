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
    [RoutePrefix("api/container")]
    public class ContainerController : BaseController
    {
        [HttpGet]
        [Route("")]
        public JsonResult<List<ContainerModel>> Get(int page = 0, string orderBy = null, bool orderAsc = true, string search = null)
        {
            using (CoreController controller = new CoreController())
            {
                var listDto = controller.ContainerCore.List(page, ContainerModel.GetPropertyName(orderBy), orderAsc, search);
                return Json(listDto.Select(dto => ContainerModel.Create(dto)).ToList());
            }
        }
        
        [HttpGet]
        [Route("total")]
        public JsonResult<object> GetTotal(string search = null)
        {
            using (CoreController controller = new CoreController())
            {
                var count = controller.ContainerCore.Count(search);
                return Json<object>(new { total = count });
            }
        }
        
        [HttpPost]
        [Route("")]
        public void Post(ContainerModel model)
        {
            using (CoreController controller = new CoreController())
            {
                controller.ContainerCore.Insert(model.Convert());
            }
        } 
        
        [HttpPut]
        [Route("{id:guid}")]
        public void Put(Guid id, ContainerModel model)
        {
            using (CoreController controller = new CoreController())
            {
                controller.ContainerCore.Update(model.Convert());
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        public JsonResult<ContainerModel> GetByPK(Guid id)
        {
            using (CoreController controller = new CoreController())
            {
                var dto = controller.ContainerCore.GetByPK(id);
                return Json(ContainerModel.Create(dto));
            }
        }
        
        [HttpDelete]
        [Route("{id:guid}")]
        public void Delete(Guid id)
        {
            using (CoreController controller = new CoreController())
            {
                controller.ContainerCore.Delete(id);
            }
        }
    }
}