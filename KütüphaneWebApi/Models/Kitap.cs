using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace KütüphaneWebApi.Models
{
    public class Kitap 
    {
        public Guid Id { get; set; }
        public string kitapAd { get; set; }
        public string kitapYazari { get; set; }
        public DateOnly kitapYazimTarihi { get; set; }
        public string kitapImg { get; set; }
        public DateOnly kitapAlimTarihi { get; set; }
        public string aktiflik { get; set; }
        public int basımNumarasi { get; set; }
        public Guid alanKişiId { get; set; }

    }
}
