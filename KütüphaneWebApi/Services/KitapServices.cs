using KütüphaneWebApi.Data;
using KütüphaneWebApi.Models;

namespace KütüphaneWebApi.Services
{
    public class KitapServices : IKitapServices
    {

        private readonly ApplicationDbContext _context;

        public KitapServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Kitap> GetKitaps()
        {
            return _context.Kitap
                .OrderBy(x => x.kitapAd)
                .ToList();
        }

        public void Guncelle(Kitap kitap)
        {
            // Veritabanından güncellenecek kitabı alıyoruz
            var mevcutKitap = _context.Kitap.FirstOrDefault(x => x.Id == kitap.Id);

            // Eğer kitap bulunamadıysa, güncelleme işlemine devam etmiyoruz
            if (mevcutKitap == null)
            {
                return;
            }

            // Kitap bilgilerini mevcut kitaptan alıyoruz
            mevcutKitap.kitapAd = kitap.kitapAd;
            mevcutKitap.kitapYazari = kitap.kitapYazari;
            mevcutKitap.kitapYazimTarihi = kitap.kitapYazimTarihi;
            mevcutKitap.kitapImg = kitap.kitapImg;
            mevcutKitap.kitapAlimTarihi = kitap.kitapAlimTarihi;
            mevcutKitap.aktiflik = kitap.aktiflik;
            mevcutKitap.basımNumarasi = kitap.basımNumarasi;
            mevcutKitap.alanKişiId = kitap.alanKişiId;

            // Değişiklikleri kaydediyoruz
            _context.SaveChanges();
        }

        public void kitapEkle(Kitap kitap)
        {
            _context.Kitap.Add(kitap);
            _context.SaveChanges();
        }

        public void Sil(Guid id)
        {
            var silinecek = _context.Kitap.FirstOrDefault( x => x.Id == id);
            if (silinecek is null)
            {
                return;
            }
            else
            {
                _context.Kitap.Remove(silinecek);
                _context.SaveChanges();
            }
        }
    }
}
