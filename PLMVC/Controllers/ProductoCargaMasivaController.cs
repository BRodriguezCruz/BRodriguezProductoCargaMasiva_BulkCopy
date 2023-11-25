using Microsoft.AspNetCore.Mvc;

namespace PLMVC.Controllers
{
    public class ProductoCargaMasivaController : Controller
    {

        [HttpGet]
        public IActionResult CargaMasiva()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CargaMasiva(bool variable)
        {
            IFormFile file = Request.Form.Files["archivoCargaMasiva"];

            bool success = BL.CargaMasiva.CargaMasivaBulkCopy(file);

            return View(success);
        }
    }
}
