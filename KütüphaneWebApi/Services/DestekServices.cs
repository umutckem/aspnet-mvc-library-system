using KütüphaneWebApi.Data;
using KütüphaneWebApi.Models;

namespace KütüphaneWebApi.Services
{
    public class DestekServices : IDestekServices
    {
        private readonly ApplicationDbContext _context;

        public DestekServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Destek> GetDesteks() 
        { 
           return _context.Destek.ToList();
        }

        public void ekleDestek(Destek destek)
        {
            _context.Destek.Add(destek);
            _context.SaveChanges();
        }

        public void silDestek(Guid id)
        {
            var silinecekDestek = _context.Destek.FirstOrDefault(x => x.Id == id);
            if(silinecekDestek == null) { return; }
            _context.Destek.Remove(silinecekDestek);
            _context.SaveChanges();
        }
    }
}
