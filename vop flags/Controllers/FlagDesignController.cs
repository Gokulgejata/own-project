using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Vopflag.Domain.Models;
using Vopflag.Infrastructure.Common;
using Vopflag.Application.ApplicationConstants;


namespace vop_flags.Controllers
{
    public class FlagDesignController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public FlagDesignController(ApplicationDbContext dbcontext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbcontext;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Flagdesign> Vopflag = _dbContext.Flagdesign.ToList();
            return View(Vopflag);
        }
        [HttpGet]
        public IActionResult create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult create(Flagdesign flagdesign)
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
                _dbContext.Flagdesign.Add(flagdesign);
                _dbContext.SaveChanges();
                TempData["success"] = CommonMessage.DetailsCreated;
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpGet]
        public IActionResult Details(Guid Id)
        {
            var flagdesign = _dbContext.Flagdesign.FirstOrDefault(x => x.Id == Id);

            if (flagdesign == null)
            {
                return NotFound(); // Or redirect to another page, e.g., Index
            }

            return View(flagdesign);
        }
        [HttpGet]
        public IActionResult Edit(Guid Id)
        {
            var flagdesign = _dbContext.Flagdesign.FirstOrDefault(x => x.Id == Id);

            if (flagdesign == null)
            {
                return NotFound(); // Or redirect to another page, e.g., Index
            }

            return View(flagdesign);
        }
        [HttpPost]
        public IActionResult Edit(Flagdesign flagdesign)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            var file = HttpContext.Request.Form.Files;
            if (file.Count > 0)
            {
                string newFileName = Guid.NewGuid().ToString();

                var upload = Path.Combine(webRootPath, @"images\flagdesign");
                var extension = Path.GetExtension(file[0].FileName);
                var objFromDb = _dbContext.Flagdesign.AsNoTracking().FirstOrDefault(x => x.Id == flagdesign.Id);
                var oldImagePath = Path.Combine(webRootPath, objFromDb.Flagview.Trim('\\'));
                if (System.IO.File.Exists(oldImagePath))
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
                _dbContext.Flagdesign.Update(flagdesign);
                _dbContext.SaveChanges();
                TempData["warning"] = CommonMessage.DetailsUpdated;
                return RedirectToAction(nameof(Index));

            }
            return View();


        }
        [HttpGet]
        public IActionResult Delete(Guid Id)
        {
            var flagdesign = _dbContext.Flagdesign.FirstOrDefault(x => x.Id == Id);

            if (flagdesign == null)
            {
                return NotFound(); // Or redirect to another page, e.g., Index
            }

            return View(flagdesign);
        }
        [HttpPost]

        public IActionResult Delete(Flagdesign flagdesign)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            if (!string.IsNullOrEmpty(flagdesign.Flagview))
            {
                var objFromDb = _dbContext.Flagdesign.AsNoTracking().FirstOrDefault(x => x.Id == flagdesign.Id);
                var oldImagePath = Path.Combine(webRootPath, objFromDb.Flagview.Trim('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _dbContext.Flagdesign.Remove(flagdesign);
            _dbContext.SaveChanges();
            TempData["error"] = CommonMessage.DetailsDeleted;
            return RedirectToAction(nameof(Index));

        }
    }
}