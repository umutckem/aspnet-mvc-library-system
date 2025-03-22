using KütüphaneWebApi.Models;
using KütüphaneWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KütüphaneWebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDestekServices _destekServices;

        public HomeController(ILogger<HomeController> logger,IDestekServices destekServices)
        {
            _logger = logger;
            _destekServices = destekServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Bilgi()
        {
            return View();
        }

        public IActionResult Destek()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Destek(Destek destek)
        {
            if (ModelState.IsValid)
            {
                destek.Id = new Guid();
                _destekServices.ekleDestek(destek);
                TempData["MesajDurumu"] = "Destek Mesajı başarıyla gönderildi.";
                return RedirectToAction("Destek");
            }
            TempData["MesajDurumu"] = "Mesaj gönderilemedi. Lütfen tekrar deneyin.";
            return View(destek);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
