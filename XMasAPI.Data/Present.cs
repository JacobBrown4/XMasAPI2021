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
        private string _contains;

        [Key]
        public int Id { get; set; }
        [Required]
        public int PresentType { get; set; }
        [Required]
        public string Wrapping { get; set; }
        [Required]
        public string Contains
        {
            get
            {
                if (IsWrapped)
                    return "Who knows?";
                else
                    return _contains;
            }
            set
            {
                _contains = value;
            }
        }
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
                PresentType = 1;
                return Contains;
            }
            else
                return "This present is already opened!";
        }

        public string Shake()
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
        [Required]
        public int TreeId { get; set; }
        [ForeignKey(nameof(TreeId))]
        public virtual Tree Tree { get; set; }
    }
}
