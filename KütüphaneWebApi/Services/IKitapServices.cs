using KütüphaneWebApi.Models;

namespace KütüphaneWebApi.Services
{
    public interface IKitapServices
    {
        List<Kitap> GetKitaps();

        void kitapEkle(Kitap kitap);
        void Guncelle(Kitap kitap);
        void Sil(Guid id);
    }
}
