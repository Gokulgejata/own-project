using System.ComponentModel.DataAnnotations;

namespace vop_flags.Models
{
    public class Flagdesign
   
    { 
    
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Display(Name ="Flag Name")]
        public string FlagName { get; set; }
        [Required]
        
        public string Types { get; set; }
        [Display(Name ="Flag View")]

        public string Flagview {  get; set; }   


    }
}

