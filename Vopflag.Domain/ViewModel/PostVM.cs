using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vopflag.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vopflag.Domain.ViewModel
{
    public class PostVM
    {
        public Post Post {get; set;}

        public IEnumerable<SelectListItem>FlagdesignList {get; set;}
        public IEnumerable<SelectListItem> FlagMaterialList { get; set; }


        public IEnumerable<SelectListItem> FlagMaterialTypeList { get; set; }
        public IEnumerable<SelectListItem> BundleAvailabilityList { get; set; }
    }
}
