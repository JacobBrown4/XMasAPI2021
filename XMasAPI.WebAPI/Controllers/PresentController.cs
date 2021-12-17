using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XMasAPI.Models.Present;
using XMasAPI.Services;

namespace XMasAPI.WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/presents")]
    public class PresentController : ApiController
    {
        private PresentService CreatePresentService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var presentService = new PresentService(userId);
            return presentService;
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            PresentService presentService = CreatePresentService();
            var presents = presentService.GetPresents();
            return Ok(presents);
        }
        [HttpPost]
        public IHttpActionResult Post(PresentCreate present)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePresentService();

            if (!service.CreatePresent(present))
                return InternalServerError();

            return Ok("Present wrapped and placed under the tree!");
        }
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            PresentService presentService = CreatePresentService();
            var present = presentService.GetPresentById(id);
            if (present != null)
            {
                return Ok(present);
            }
            else return BadRequest("Item doesn't exist");
        }
        [HttpGet]
        [Route("{id}/shake")]
        public IHttpActionResult Shake(int id)
        {
            var presentService = CreatePresentService();
            var present = presentService.ShakePresent(id);
            if (present != default)
            {
                return Ok(present);
            }
            else return BadRequest("Item doesn't exist");
        }

        [HttpGet]
        [Route("{id}/unwrap")]
        public IHttpActionResult Unwrap(int id)
        {
            var presentService = CreatePresentService();
            var present = presentService.UnwrapPresent(id);
            if (present != null)
            {
                return Ok(present);
            }
            else return BadRequest("Item doesn't exist");
        }

        [HttpPut]
        public IHttpActionResult UpdatePresent(PresentEdit edited)
        {
            var presentService = CreatePresentService();
            var present = presentService.UpdatePresent(edited);
            if (present != null)
            {
                return Ok(present);
            }
            else return BadRequest("Item doesn't exist");
        }
    }
}
