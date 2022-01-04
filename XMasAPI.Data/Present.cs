using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMasAPI.Data
{
    public class Present
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public int PresentType { get; set; }
        [Required]
        public string Wrapping { get; set; }
        [Required]
        public string Contains { get; set; }
        [Required]
        public string Hint1 { get; set; }
        [Required]
        public string Hint2 { get; set; }
        [Required]
        public string Hint3 { get; set; }
        public int TimesShaken { get; set; }
        public bool IsWrapped { get; set; }
        public string Unwrap()
        {
            if (IsWrapped)
            {
                IsWrapped = false;
                PresentType = 20;
                return Contains;
            }
            else
                return "This present is already opened!";
        }

        public string Shake()
        {
            if (IsWrapped)
            {

                switch (TimesShaken++)
                {
                    case 0:
                        return Hint1;
                    case 1:
                        return Hint2;
                    case 2:
                        return Hint3;
                    default:
                        return "No more hints left";

                }
            }
            else
                return "It's already unwrapped dum dum!";
        }
        [Required]
        public int TreeId { get; set; }
        [ForeignKey(nameof(TreeId))]
        public virtual Tree Tree { get; set; }
    }
}
