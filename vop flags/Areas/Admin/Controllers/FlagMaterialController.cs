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
    public class FlagMaterialController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public FlagMaterialController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork=unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<FlagMaterial> Vopflag = await _unitOfWork.FlagMaterial.GetAllAsync();

            return View(Vopflag);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(FlagMaterial flagMaterial)
        {
           
            if (ModelState.IsValid)
            {
               await _unitOfWork.FlagMaterial.Create(flagMaterial);
               await _unitOfWork.saveAsync();
                TempData["success"] = CommonMessage.DetailsCreated;
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult>Details(Guid Id)
        {
            var flagMaterial = await _unitOfWork.FlagMaterial.GetByIdAsync(Id);

            if (flagMaterial == null)
            {
                return NotFound(); // Or redirect to another page, e.g., Index
            }

            return View(flagMaterial);
        }
        [HttpGet]
        public async Task <IActionResult> Edit(Guid Id)
        {
            var flagMaterial = await _unitOfWork.FlagMaterial.GetByIdAsync(Id);

            if (flagMaterial == null)
            {
                return NotFound(); // Or redirect to another page, e.g., Index
            }

            return View(flagMaterial);
        }
        [HttpPost]
        public async Task <IActionResult> Edit(FlagMaterial flagMaterial)
        {
           
            if (ModelState.IsValid)
            {
                await _unitOfWork.FlagMaterial.update(flagMaterial);
                TempData["warning"] = CommonMessage.DetailsUpdated;
                return RedirectToAction(nameof(Index));

            }
            return View();


        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var flagMaterial = await _unitOfWork.FlagMaterial.GetByIdAsync(Id);

            if (flagMaterial== null)
            {
                return NotFound(); // Or redirect to another page, e.g., Index
            }

            return View(flagMaterial);
        }
        [HttpPost]

        public async Task<IActionResult> Delete(FlagMaterial flagMaterial)
        {
       

            await _unitOfWork.FlagMaterial.Delete(flagMaterial);
            await _unitOfWork.saveAsync();
            TempData["error"] = CommonMessage.DetailsDeleted;
            return RedirectToAction(nameof(Index));

        }
    }
}