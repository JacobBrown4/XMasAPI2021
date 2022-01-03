using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMasAPI.Data;
using XMasAPI.Models.Ornament;

namespace XMasAPI.Services
{
    public class OrnamentService
    {

        private readonly Guid _userId;

        public OrnamentService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateOrnament(OrnamentCreate model)
        {
            var ornament = new Ornament()
            {
                Description = model.Description,
                TreeId = model.TreeId,
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Ornaments.Add(ornament);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<OrnamentListItem> GetOrnaments()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.Ornaments.Select(x => new OrnamentListItem
                {
                    OrnamentId = x.Id,
                    Description = x.Description,
                    TreeId=x.TreeId,
                }).ToArray();
            }
        }

        public OrnamentDetail GetOrnamentById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var ornament = ctx.Ornaments.SingleOrDefault(p => p.Id == id);
                if (ornament == default)
                {
                    return null;
                }
                return
                    new OrnamentDetail
                    {
                        OrnamentId = ornament.Id,
                        Description = ornament.Description,
                        Tree = new Models.Tree.TreeListItem
                        {
                            TreeId = ornament.Tree.Id,
                            Description = ornament.Tree.Description,
                            AmountOfOrnaments = ornament.Tree.Ornaments.Count(),
                            AmountOfPresents = ornament.Tree.Presents.Count()
                        }
                    };
            }
        }

        public OrnamentDetail UpdateOrnament(OrnamentEdit edited)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var ornament = ctx.Ornaments.SingleOrDefault(p => p.Id == edited.OrnamentId);
                if (ornament != default)
                {
                    ornament.Description = edited.Description;
                    ornament.TreeId = edited.TreeId;

                    if (ctx.SaveChanges() == 1)
                    {
                        return GetOrnamentById(ornament.Id);
                    }

                }
                return null;
            }
        }
    }
}
