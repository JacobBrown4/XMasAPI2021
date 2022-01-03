using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XMasAPI.Models.Ornament;
using XMasAPI.Services;

namespace XMasAPI.WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/ornaments")]
    public class OrnamentController : ApiController
    {
        private OrnamentService CreateOrnamentService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var ornamentService = new OrnamentService(userId);
            return ornamentService;
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            OrnamentService ornamentService = CreateOrnamentService();
            var ornaments = ornamentService.GetOrnaments();
            return Ok(ornaments);
        }
        [HttpPost]
        public IHttpActionResult Post(OrnamentCreate ornament)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateOrnamentService();

            if (!service.CreateOrnament(ornament))
                return InternalServerError();

            return Ok("Ornament placed on the tree!");
        }
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            OrnamentService ornamentService = CreateOrnamentService();
            var ornament = ornamentService.GetOrnamentById(id);
            if (ornament != null)
            {
                return Ok(ornament);
            }
            else return BadRequest("Item doesn't exist");
        }

        [HttpPut]
        public IHttpActionResult UpdateOrnament(OrnamentEdit edited)
        {
            var ornamentService = CreateOrnamentService();
            var ornament = ornamentService.UpdateOrnament(edited);
            if (ornament != null)
            {
                return Ok(ornament);
            }
            else return BadRequest("Item doesn't exist");
        }
    }

}
