using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vopflag.Domain.Common;

namespace Vopflag.Domain.Models
{
    public class Flagdesign : BaseModel
    {
       

        [Required]
        [Display(Name = "Flag Name")]
        public string FlagName { get; set; }
        [Required]

        public string Types { get; set; }
        [Display(Name = "Flag View")]

        public string Flagview { get; set; }

    }
}
