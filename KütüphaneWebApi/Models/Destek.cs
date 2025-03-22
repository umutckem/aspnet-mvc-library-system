namespace KütüphaneWebApi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Destek
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        [StringLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad alanı zorunludur.")]
        [StringLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir.")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "Telefon Numarası zorunludur.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Telefon Numarası 10 haneli olmalıdır.")]
        public string TelefonNo { get; set; }

        [Required(ErrorMessage = "Açıklama alanı zorunludur.")]
        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
        public string Aciklama { get; set; }

        [Required(ErrorMessage = "Email alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }
    }

}
