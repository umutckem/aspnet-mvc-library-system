using KütüphaneWebApi.Models;

namespace KütüphaneWebApi.Services
{
    public interface IDestekServices
    {
        List<Destek> GetDesteks();
        void ekleDestek(Destek destek);
        void silDestek(Guid id);
    }
}
