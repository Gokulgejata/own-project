using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Vopflag.Domain.Common;

namespace Vopflag.Domain.Models
{
    public class FlagMaterial:BaseModel
    {
        [Required]
        [Display(Name ="Material Type")]
        public string MaterialType { get; set; }
    } 
}
