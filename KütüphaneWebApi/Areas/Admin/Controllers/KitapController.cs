using KütüphaneWebApi.Data;
using KütüphaneWebApi.Models;
using KütüphaneWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KütüphaneWebApi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class KitapController : Controller
    {
        private readonly IKitapServices _kitapServices;
        private readonly ApplicationDbContext _context;
        private readonly IDestekServices _destekServices;

        public KitapController(IKitapServices kitapServices, ApplicationDbContext context, IDestekServices destekServices)
        {
            _kitapServices = kitapServices;
            _context = context;
            _destekServices = destekServices;
        }

        public IActionResult Index()
        {
            var kitaplar = _kitapServices.GetKitaps();
            return View(kitaplar);
        }


        [HttpGet]
        public IActionResult KitapEkle()
        {
            
            var model = new Kitap();
            return View(model);
        }

        [HttpPost]
        public IActionResult KitapEkle(Kitap kitap)
        {
            if (ModelState.IsValid)
            {
                
                _kitapServices.kitapEkle(kitap);
                TempData["SuccessMessage"] = "Kitap başarıyla eklendi!";
                return RedirectToAction("Index", "Kitap", new { area = "Admin" });
            }

            
            return View(kitap);
        }

        public IActionResult KitapSil(Guid Id)
        {
            var kitaplar = _kitapServices.GetKitaps();
            var kitap = kitaplar.FirstOrDefault(x => x.Id == Id);
            if (kitap == null)
            {
                return RedirectToAction("Index", "Kitap", new { area = "Admin" });
            }
            else {


                _kitapServices.Sil(kitap.Id);
                return RedirectToAction("Index", "Kitap", new { area = "Admin" });

            }  
        }



        [HttpGet]
        public IActionResult Guncelle(Guid id)
        {
            
            var kitaplar = _kitapServices.GetKitaps();
            var kitap = kitaplar.FirstOrDefault(x => x.Id == id);
            if (kitap == null)
            {
                return NotFound();
            }

            
            return View(kitap);
        }


        [HttpGet]
        public IActionResult Guncelle2()
        {
            
            return View(new Kitap());
        }


        [HttpPost]
        public IActionResult Guncelle2(Kitap kitap)
        {
            if (ModelState.IsValid)
            {
                
                _kitapServices.Guncelle(kitap);
                return RedirectToAction("Index");
            }

            return View(kitap); 
        }

        public IActionResult Kullanici()
        {
            var kullancilar = _context.Users
                .OrderBy(x => x.Id)
                .ToList();
            return View(kullancilar);
        }

        [HttpGet]
        public IActionResult KullaniciDetay(Guid Id)
        {
            var kitaplar = _kitapServices.GetKitaps();
            var aldıgıKitaplar = kitaplar.Where(x => x.alanKişiId == Id).ToList(); 

            
            if (!aldıgıKitaplar.Any())
            {
                ViewBag.Message = "Bu kullanıcıya ait alınan hiçbir kitap bulunmamaktadır.";
            }

            
            return View(aldıgıKitaplar);
        }

        public IActionResult Destek()
        {
            var destekMesajları = _destekServices.GetDesteks();
            return View(destekMesajları);
        }

        [HttpPost]
        public IActionResult DestekSil(Guid id)
        {
            _destekServices.silDestek(id);
            return RedirectToAction("Destek", "Kitap", new { area = "Admin" });
        }



    }
}
