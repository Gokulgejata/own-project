using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vopflag.Domain.ApplicationEnum;
using Vopflag.Domain.Common;

namespace Vopflag.Domain.Models
{
    public class Post:BaseModel
    {
        [Display(Name ="Flag Design")]
        public Guid FlagdesignId { get; set; } 

        [ValidateNever]
        [ForeignKey("FlagdesignId")]

        public Flagdesign flagdesign { get; set; }

        [Display(Name = "Flag Material")]
        public Guid FlagMaterialId { get; set; }

        [ValidateNever]
        [ForeignKey("FlagMaterialId")]
        public FlagMaterial flagMaterial { get; set; }

        public string Name {  get; set; }

        [Display(Name="Select Material Type")]
         public FlagMaterialType FlagMaterialType { get; set; }

        [Display(Name="Availability")]
         public BundleAvailability BundleAvailability { get; set; }

        [Display(Name="Price of Single Piece")]
        public int SinglePiecePrice { get; set; }

        [Display(Name ="Price of Single Bundle")]
        public double SingleBundlePrice { get; set; }

        [Display(Name="Images")]
        public string FlagImage {  get; set; }

        [Range(1,5,ErrorMessage = "Rating should be in range of 1 to 5")]
        public int Ratings { get; set; }
    }
}
