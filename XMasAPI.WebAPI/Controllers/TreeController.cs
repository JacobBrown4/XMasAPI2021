using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XMasAPI.Models.Tree;
using XMasAPI.Services;

namespace XMasAPI.WebAPI.Controllers
{
    [Authorize]
    public class TreeController : ApiController
    {
        private TreeService CreateTreeService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var treeService = new TreeService(userId);
            return treeService;
        }

        public IHttpActionResult Get()
        {
            TreeService treeService = CreateTreeService();
            var trees = treeService.GetTrees();
            return Ok(trees);
        }

        public IHttpActionResult Post(TreeCreate tree)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateTreeService();

            if (!service.CreateTree(tree))
                return InternalServerError();

            return Ok("Tree mounted and watered!");
        }
    }
}
