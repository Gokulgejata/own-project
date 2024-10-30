using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Vopflag.Domain.Models;
using Vopflag.Infrastructure.Common;
using Vopflag.Application.ApplicationConstants;
using Vopflag.Application.Contracts.Persistence;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vopflag.Domain.ApplicationEnum;
using Vopflag.Domain.ViewModel;


namespace vop_flags.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public PostController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork=unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Post> Vopflag = await _unitOfWork.Post.GetAllPosts();

            return View(Vopflag);
        }
        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> flagdesignList = _unitOfWork.Flagdesign.Query().Select(x => new SelectListItem
            {
                Text = x.FlagName.ToUpper(),
                Value = x.Id.ToString()
            });

            IEnumerable<SelectListItem> flagMaterialList = _unitOfWork.FlagMaterial.Query().Select(x => new SelectListItem
            {
                Text = x.MaterialType.ToUpper(),
                Value = x.Id.ToString()
            });

            IEnumerable<SelectListItem> flagMaterialTypeList = Enum.GetValues(typeof(FlagMaterialType))
            .Cast<FlagMaterialType>()
            .Select(x => new SelectListItem
            {
                Text = x.ToString().ToUpper(),
                Value = ((int)x).ToString()
            });
            IEnumerable<SelectListItem> bundleAvailabilityList = Enum.GetValues(typeof(BundleAvailability))
            .Cast<BundleAvailability>()
            .Select(x => new SelectListItem
            {
                Text = x.ToString().ToUpper(),
                Value = ((int)x).ToString()
            });
            PostVM postVM = new PostVM
            {
                Post = new Post(),
                FlagdesignList = flagdesignList,
                FlagMaterialList = flagMaterialList,
                FlagMaterialTypeList = flagMaterialTypeList,
                BundleAvailabilityList = bundleAvailabilityList,

            };
            return View(postVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(PostVM postVM)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            var file = HttpContext.Request.Form.Files;
            if (file.Count > 0)
            {
                string newFileName = Guid.NewGuid().ToString();

                var upload = Path.Combine(webRootPath, @"images\post");
                var extension = Path.GetExtension(file[0].FileName);
                using (var fileStream = new FileStream(Path.Combine(upload, newFileName + extension), FileMode.Create))
                {
                    file[0].CopyTo(fileStream);
                }
                postVM.Post.FlagImage = @"\images\post\" + newFileName + extension;
            }
            if (ModelState.IsValid)
            {
               await _unitOfWork.Post.Create(postVM.Post);
               await _unitOfWork.saveAsync();
                TempData["success"] = CommonMessage.DetailsCreated;
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult>Details(Guid Id)
        {
            var post = await _unitOfWork.Post.GetPostById(Id);

            if (post == null)
            {
                return NotFound(); // Or redirect to another page, e.g., Index
            }

            return View(post);
        }
        [HttpGet]
        public async Task <IActionResult> Edit(Guid Id)
        {
            var post = await _unitOfWork.Post.GetPostById(Id);

            IEnumerable<SelectListItem> flagdesignList = _unitOfWork.Flagdesign.Query().Select(x => new SelectListItem
            {
                Text = x.FlagName.ToUpper(),
                Value = x.Id.ToString()
            });

            IEnumerable<SelectListItem> flagMaterialList = _unitOfWork.FlagMaterial.Query().Select(x => new SelectListItem
            {
                Text = x.MaterialType.ToUpper(),
                Value = x.Id.ToString()
            });

            IEnumerable<SelectListItem> flagMaterialTypeList = Enum.GetValues(typeof(FlagMaterialType))
            .Cast<FlagMaterialType>()
            .Select(x => new SelectListItem
            {
                Text = x.ToString().ToUpper(),
                Value = ((int)x).ToString()
            });
            IEnumerable<SelectListItem> bundleAvailabilityList = Enum.GetValues(typeof(BundleAvailability))
            .Cast<BundleAvailability>()
            .Select(x => new SelectListItem
            {
                Text = x.ToString().ToUpper(),
                Value = ((int)x).ToString()
            });
            PostVM postVM = new PostVM
            {
                Post = post,
                FlagdesignList = flagdesignList,
                FlagMaterialList = flagMaterialList,
                FlagMaterialTypeList = flagMaterialTypeList,
                BundleAvailabilityList = bundleAvailabilityList,

            };

            if (post == null)
            {
                return NotFound(); // Or redirect to another page, e.g., Index
            }

            return View(postVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PostVM postVM)
        {
            if (ModelState.IsValid)
            {
                var webRootPath = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                if (files.Count > 0)
                {
                    var newFileName = Guid.NewGuid().ToString();
                    var upload = Path.Combine(webRootPath, @"images\posts");
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, newFileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    postVM.Post.FlagImage = @"\images\posts\" + newFileName + extension;
                }

                await _unitOfWork.Post.Update(postVM.Post);
                TempData["success"] = "Post updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(postVM.Post);
        


    }
    [HttpGet]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var post = await _unitOfWork.Post.GetPostById(Id);
           

            IEnumerable<SelectListItem> flagdesignList = _unitOfWork.Flagdesign.Query().Select(x => new SelectListItem
            {
                Text = x.FlagName.ToUpper(),
                Value = x.Id.ToString()
            });

            IEnumerable<SelectListItem> flagMaterialList = _unitOfWork.FlagMaterial.Query().Select(x => new SelectListItem
            {
                Text = x.MaterialType.ToUpper(),
                Value = x.Id.ToString()
            });

            IEnumerable<SelectListItem> flagMaterialTypeList = Enum.GetValues(typeof(FlagMaterialType))
            .Cast<FlagMaterialType>()
            .Select(x => new SelectListItem
            {
                Text = x.ToString().ToUpper(),
                Value = ((int)x).ToString()
            });
            IEnumerable<SelectListItem> bundleAvailabilityList = Enum.GetValues(typeof(BundleAvailability))
            .Cast<BundleAvailability>()
            .Select(x => new SelectListItem
            {
                Text = x.ToString().ToUpper(),
                Value = ((int)x).ToString()
            });
            PostVM postVM = new PostVM
            {
                Post = post,
                FlagdesignList = flagdesignList,
                FlagMaterialList = flagMaterialList,
                FlagMaterialTypeList = flagMaterialTypeList,
                BundleAvailabilityList = bundleAvailabilityList,

            };

            if (post == null)
            {
                return NotFound(); // Or redirect to another page, e.g., Index
            }

            return View(postVM);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(PostVM postVM)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;

            // Fetch the entity from the database to ensure it's the same instance tracked by the context
            var objFromDb = await _unitOfWork.Post.GetPostById(postVM.Post.Id);
            if (objFromDb == null)
            {
                return NotFound(); // Entity not found
            }

            // Check for and delete the image file if it exists
            if (!string.IsNullOrEmpty(objFromDb.FlagImage))
            {
                var oldImagePath = Path.Combine(webRootPath, objFromDb.FlagImage.Trim('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            // Now, delete the entity
            await _unitOfWork.Post.Delete(objFromDb); // Use the tracked instance
            await _unitOfWork.saveAsync();
            TempData["error"] = CommonMessage.DetailsDeleted;
            return RedirectToAction(nameof(Index));
        }

    }
}