    using KütüphaneWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace KütüphaneWebApi.Controllers
{
    public class KitapsController : Controller
    {
        private readonly IKitapServices _kitapServices;

        public KitapsController(IKitapServices kitapServices)
        {
            _kitapServices = kitapServices;
        }

        public IActionResult Index()
        {

            string runtimeVersion = RuntimeInformation.FrameworkDescription;
            ViewBag.RuntimeVersion = runtimeVersion;

            var kitaplar = _kitapServices.GetKitaps();
            return View(kitaplar);

        }

        [HttpGet]
        public IActionResult Detay(Guid id)
        {

            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                TempData["SweetAlertMessage"] = "Kullanıcı oturumu açık değil! Lütfen giriş yapın.";
                return Redirect("/Identity/Account/Login"); // Login sayfasına yönlendirilir
            }


            var kitap = _kitapServices.GetKitaps().FirstOrDefault(x => x.Id == id);

            if (kitap == null)
            {
                TempData["SweetAlertMessage"] = "Aradığınız kitap bulunamadı!";
                return RedirectToAction("Index");
            }

            return View(kitap);
        }

        [HttpGet]
        public IActionResult AlKitap(Guid id)
        {
            var kitap = _kitapServices.GetKitaps().FirstOrDefault(x => x.Id == id);

            if (kitap != null && kitap.aktiflik == "Aktif")
            {
                // Kitap alım işlemi yap (örneğin, kitap alındı olarak işaretlenebilir)
                kitap.aktiflik = "False";
                kitap.alanKişiId = Guid.Parse(User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier)); // Kullanıcı ID'sini al
                kitap.kitapAlimTarihi = DateOnly.FromDateTime(DateTime.Now); // Alım tarihini bugüne ayarla

                // Kitap güncellenir
                _kitapServices.Guncelle(kitap); // Bu metodun güncel bir kitap verisini kaydetmesi gerektiğini varsayıyorum.

                TempData["SweetAlertMessage"] = "Kitap başarıyla alındı!";
            }
            else
            {
                TempData["SweetAlertMessage"] = "Kitap alımı yapılamaz. Kitap mevcut değil veya aktif değil!";
            }

            return RedirectToAction("Index");
        }
    }
}
