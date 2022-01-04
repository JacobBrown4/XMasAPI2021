using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMasAPI.Data;
using XMasAPI.Models;
using XMasAPI.Models.Ornament;
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
                Description = model.Description,
                HasStar = model.HasStar,
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
                    AmountOfPresents = x.Presents.Count(),
                    AmountOfOrnaments = x.Ornaments.Count()
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
                        Contains = x.IsWrapped ? "Who knows?" : x.Contains,
                        TreeId = x.TreeId
                    }).ToList(),
                    OrnamentCount = tree.Ornaments.Count(),
                    Ornaments = tree.Ornaments.Select(o => new OrnamentListItem
                    {
                        OrnamentId = o.Id,
                        Description = o.Description,
                        TreeId = o.TreeId
                    }).ToList(),
                    Gifts = tree.Presents.Where(p => p.IsWrapped == false).Select(x => x.Contains).ToList()
                };

            }
        }
        public bool UpdateTree(TreeEdit edited)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var tree = ctx.Trees.SingleOrDefault(p => p.Id == edited.TreeId);
                if (tree != default)
                {
                    tree.Description = edited.Description;
                    tree.HasStar = edited.HasStar;

                    return ctx.SaveChanges() == 1;


                }
                return false;
            }
        }
        public List<string> UnwrapAll(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var tree = ctx.Trees.Single(t => t.Id == id);
                var presents = tree.Presents.Where(p => p.IsWrapped == true);
                List<string> gifts = new List<string>();
                foreach (var present in presents)
                {
                    present.Unwrap();
                    gifts.Add(present.Contains);
                }
                ctx.SaveChanges();
                return gifts;
            }
        }
    }
}
