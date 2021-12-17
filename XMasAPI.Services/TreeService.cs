using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMasAPI.Data;
using XMasAPI.Models;
using XMasAPI.Models.Tree;

namespace XMasAPI.Services
{
    public class TreeService
    {

        private readonly Guid _userId;

        public TreeService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateTree(TreeCreate model)
        {
            var tree = new Tree()
            {
                Description = model.Description
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Trees.Add(tree);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<TreeListItem> GetTrees()
        {
            using(var ctx = new ApplicationDbContext())
            {
                return ctx.Trees.Select(x => new TreeListItem
                {
                    TreeId = x.Id,
                    Description = x.Description,
                    AmountOfPresents = x.Presents.Count()
                }).ToArray();
            }
        }
    }
}
