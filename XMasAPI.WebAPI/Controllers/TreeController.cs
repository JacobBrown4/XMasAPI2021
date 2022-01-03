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
    [RoutePrefix("api/tree")]
    public class TreeController : ApiController
    {
        private TreeService CreateTreeService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var treeService = new TreeService(userId);
            return treeService;
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            TreeService treeService = CreateTreeService();
            var trees = treeService.GetTrees();
            return Ok(trees);
        }
        [HttpPost]
        public IHttpActionResult Post(TreeCreate tree)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateTreeService();

            if (!service.CreateTree(tree))
                return InternalServerError();

            return Ok("Tree mounted and watered!");
        }
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetById(int id)
        {
            TreeService treeService = CreateTreeService();
            var tree = treeService.GetTreeById(id);
            return Ok(tree);
        }

        [Route("{id}/UnwrapAll")]
        public IHttpActionResult UnwrapAll(int id)
        {
            TreeService treeService = CreateTreeService();
            var presents = treeService.UnwrapAll(id);
            return Ok(presents);
        }
        [HttpPut]
        public IHttpActionResult Edit(TreeEdit edited)
        {
            var treeService = CreateTreeService();
            var result = treeService.UpdateTree(edited);
            return Ok(result);
        }
    }
}
