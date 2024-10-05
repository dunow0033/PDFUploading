using Microsoft.AspNetCore.Mvc;
using PDFuploading.Models;
using PDFuploading.Services.Abstract;
using System.Diagnostics;

namespace PDFuploading.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IImageService _imageService;
        public HomeController(ILogger<HomeController> logger, IImageService imageService)
        {
            _logger = logger;
            _imageService = imageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(FileUploadViewModel model)
        {
            string fileExtension = Path.GetExtension(model.File.FileName);

            if (model.File == null)
                return View("Index");

            _imageService.UploadImageToAzure(model.File);

            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
