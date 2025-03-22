using KütüphaneWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Security.Claims;

namespace KütüphaneWebApi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "User")]
    public class KullaniciController : Controller
    {
        private readonly IKitapServices _kitapServices;
        private readonly IDestekServices _destekServices;

        public KullaniciController(IKitapServices kitapServices,IDestekServices destekServices)
        {
            _kitapServices = kitapServices;
            _destekServices = destekServices;
        }

        public IActionResult Index()
        {
            
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                TempData["SweetAlertMessage"] = "Kullanıcı oturumu açık değil! Lütfen giriş yapın.";
                return Redirect("/Identity/Account/Login"); 
            }

            
            var kullaniciBilgileri = User.FindAll(System.Security.Claims.ClaimTypes.Name);

            
            string kullaniciAdi = kullaniciBilgileri.FirstOrDefault()?.Value;

            
            var kitaplar = _kitapServices.GetKitaps();

            
            Guid kullaniciId = Guid.Parse(User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier));

            
            var kullaniciKitaplari = kitaplar.Where(x => x.alanKişiId == kullaniciId).ToList();

            
            return View(kullaniciKitaplari);
        }

        [HttpGet]
        public IActionResult KitapBırak(Guid id)
        {
            
            var kitaplar = _kitapServices.GetKitaps();

            
            var kitap = kitaplar.FirstOrDefault(x => x.Id == id);

            
            if (kitap == null)
            {
                return View("Error"); 
            }

            kitap.alanKişiId = Guid.Parse("b312f6f1-4f96-4903-80d5-07f92051b0f8");
            kitap.aktiflik = "Aktif";

            _kitapServices.Guncelle(kitap);

            return RedirectToAction("Index", "Kullanici", new { area = "Admin" });


        }



    }
}
