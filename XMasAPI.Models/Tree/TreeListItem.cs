using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMasAPI.Models.Tree
{
    public class TreeListItem
    {
        public int TreeId { get; set; }
        public string Description { get; set; }
        public int AmountOfPresents { get; set; }
        public int AmountOfOrnaments { get; set; }
    }
}
