using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vopflag.Domain.Models
{
    public class Flagdesign
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Flag Name")]
        public string FlagName { get; set; }
        [Required]

        public string Types { get; set; }
        [Display(Name = "Flag View")]

        public string Flagview { get; set; }

    }
}
