using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Vopflag.Domain.Models;
using Vopflag.Infrastructure.Common;
using Vopflag.Application.ApplicationConstants;
using Vopflag.Application.Contracts.Persistence;


namespace vop_flags.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FlagDesignController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public FlagDesignController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork=unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Flagdesign> Vopflag = await _unitOfWork.Flagdesign.GetAllAsync();

            return View(Vopflag);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Flagdesign flagdesign)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            var file = HttpContext.Request.Form.Files;
            if (file.Count > 0)
            {
                string newFileName = Guid.NewGuid().ToString();

                var upload = Path.Combine(webRootPath, @"images\flagdesign");
                var extension = Path.GetExtension(file[0].FileName);
                using (var fileStream = new FileStream(Path.Combine(upload, newFileName + extension), FileMode.Create))
                {
                    file[0].CopyTo(fileStream);
                }
                flagdesign.Flagview = @"\images\flagdesign\" + newFileName + extension;
            }
            if (ModelState.IsValid)
            {
               await _unitOfWork.Flagdesign.Create(flagdesign);
               await _unitOfWork.saveAsync();
                TempData["success"] = CommonMessage.DetailsCreated;
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult>Details(Guid Id)
        {
            var flagdesign = await _unitOfWork.Flagdesign.GetByIdAsync(Id);

            if (flagdesign == null)
            {
                return NotFound(); // Or redirect to another page, e.g., Index
            }

            return View(flagdesign);
        }
        [HttpGet]
        public async Task <IActionResult> Edit(Guid Id)
        {
            var flagdesign = await _unitOfWork.Flagdesign.GetByIdAsync(Id);

            if (flagdesign == null)
            {
                return NotFound(); // Or redirect to another page, e.g., Index
            }

            return View(flagdesign);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Flagdesign flagdesign)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            var file = HttpContext.Request.Form.Files;

            if (file.Count > 0)
            {
                string newFileName = Guid.NewGuid().ToString();
                var upload = Path.Combine(webRootPath, @"images\flagdesign");
                var extension = Path.GetExtension(file[0].FileName);

                // Retrieve the object from the database
                var objFromDb = await _unitOfWork.Flagdesign.GetByIdAsync(flagdesign.Id);

                if (objFromDb == null)
                {
                    // Handle the case where the object is not found in the database.
                    return NotFound(); // or another appropriate response
                }

                // Get the old image path only if Flagview is not null
                var oldImagePath = objFromDb.Flagview != null
                    ? Path.Combine(webRootPath, objFromDb.Flagview.Trim('\\'))
                    : null;

                // Delete the old image if it exists
                if (!string.IsNullOrEmpty(oldImagePath) && System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

                using (var fileStream = new FileStream(Path.Combine(upload, newFileName + extension), FileMode.Create))
                {
                    file[0].CopyTo(fileStream);
                }

                flagdesign.Flagview = @"\images\flagdesign\" + newFileName + extension;
            }

            if (ModelState.IsValid)
            {
                await _unitOfWork.Flagdesign.update(flagdesign);
                TempData["warning"] = CommonMessage.DetailsUpdated;
                return RedirectToAction(nameof(Index));
            }

            return View(flagdesign);
       


    }
    [HttpGet]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var flagdesign = await _unitOfWork.Flagdesign.GetByIdAsync(Id);

            if (flagdesign == null)
            {
                return NotFound(); // Or redirect to another page, e.g., Index
            }

            return View(flagdesign);
        }
        [HttpPost]

        public async Task<IActionResult> Delete(Flagdesign flagdesign)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            if (!string.IsNullOrEmpty(flagdesign.Flagview))
            {
                var objFromDb = await _unitOfWork.Flagdesign.GetByIdAsync(flagdesign.Id);
                var oldImagePath = Path.Combine(webRootPath, objFromDb.Flagview.Trim('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            await _unitOfWork.Flagdesign.Delete(flagdesign);
            await _unitOfWork.saveAsync();
            TempData["error"] = CommonMessage.DetailsDeleted;
            return RedirectToAction(nameof(Index));

        }
    }
}