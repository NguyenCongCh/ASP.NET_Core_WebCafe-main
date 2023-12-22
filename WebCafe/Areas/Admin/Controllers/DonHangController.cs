using Microsoft.AspNetCore.Mvc;

namespace WebCafe.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
