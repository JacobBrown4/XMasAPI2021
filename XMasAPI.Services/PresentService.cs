using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMasAPI.Data;
using XMasAPI.Models;
using XMasAPI.Models.Present;

namespace XMasAPI.Services
{
    public class PresentService
    {

        private readonly Guid _userId;

        public PresentService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreatePresent(PresentCreate model)
        {
            var present = new Present()
            {
                PresentType = (int)model.PresentType,
                Wrapping = model.Wrapping,
                Hint1 = model.Hint1,
                Hint2 = model.Hint2,
                Hint3 = model.Hint3,
                TreeId = model.TreeId,
                TimesShaken = 0,
                IsWrapped = true
            };

            present.Contains = model.Contains;

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Presents.Add(present);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<PresentListItem> GetPresents()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return ctx.Presents.Select(x => new PresentListItem
                {
                    PresentId = x.Id,
                    PresentType = ((PresentType)x.PresentType).ToString(),
                    Wrapping = x.Wrapping,
                    Contains = x.Contains,
                    TreeId = x.TreeId
                }).ToArray();
            }
        }

        public PresentDetail GetPresentById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var present = ctx.Presents.SingleOrDefault(p => p.Id == id);
                if (present == default)
                {
                    return null;
                }
                return
                    new PresentDetail
                    {
                        PresentId = present.Id,
                        PresentType = ((PresentType)present.PresentType).ToString(),
                        Wrapping = present.Wrapping,
                        Contains = present.Contains,
                        TimesShaken = present.TimesShaken,
                        IsWrapped = present.IsWrapped,
                        Tree = new Models.Tree.TreeListItem
                        {
                            TreeId = present.Tree.Id,
                            Description = present.Tree.Description,
                            AmountOfPresents = present.Tree.Presents.Count(),
                            AmountOfOrnaments = present.Tree.Presents.Count()
                        }
                    };
            }
        }

        public string ShakePresent(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var present = ctx.Presents.SingleOrDefault(p => p.Id == id);
                var shake = present.Shake();
                ctx.SaveChanges(); //That way we save the Times Shaken increment
                return shake;
            }
        }
        public string UnwrapPresent(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var present = ctx.Presents.SingleOrDefault(p => p.Id == id);
                var unwrapped = present.Unwrap();
                ctx.SaveChanges(); //That way we save the unwrap status
                return unwrapped;
            }
        }

        public PresentDetail UpdatePresent(PresentEdit edited)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var present = ctx.Presents.SingleOrDefault(p => p.Id == edited.PresentId);
                if (present != default)
                {
                    present.PresentType = (int)edited.PresentType;
                    present.Wrapping = edited.Wrapping;
                    present.Hint1 = edited.Hint1;
                    present.Hint2 = edited.Hint2;
                    present.Hint3 = edited.Hint3;
                    present.TreeId = edited.TreeId;

                    if (ctx.SaveChanges() == 1)
                    {
                        return GetPresentById(present.Id);
                    }

                }
                return null;
            }
        }
    }
}
