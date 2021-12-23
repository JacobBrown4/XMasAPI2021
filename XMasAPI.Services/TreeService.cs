using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMasAPI.Data;
using XMasAPI.Models;
using XMasAPI.Models.Present;
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
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.Trees.Select(x => new TreeListItem
                {
                    TreeId = x.Id,
                    Description = x.Description,
                    AmountOfPresents = x.Presents.Count()
                }).ToArray();
            }
        }

        public TreeDetail GetTreeById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var tree = ctx.Trees.Single(p => p.Id == id);
                return new TreeDetail()
                {
                    Description = tree.Description,
                    HasStar = tree.HasStar,
                    PresentCount = tree.Presents.Count(),
                    Presents = tree.Presents.Select(x => new PresentListItem
                    {
                        PresentId = x.Id,
                        PresentType = ((PresentType)x.PresentType).ToString(),
                        Wrapping = x.Wrapping,
                        Contains = x.WhatsInside,
                        TreeId = x.TreeId
                    }).ToList()
                };

            }
        }
    }
}
