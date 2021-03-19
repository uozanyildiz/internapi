using System.Collections.Generic;
using System.Linq;
using Bogus;
using internapi.Data;
using internapi.Model;
using internapi.Repository.IRepository;

namespace internapi.Repository
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly ApplicationDbContext _db;
        public ManagerRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateManager(Manager manager)
        {
            _db.Managers.Add(manager);
            return Save();
        }

        public Manager GetManager(int id)
        {
            return _db.Managers.FirstOrDefault(x => x.Id == id);
        }

        public Manager GetManager(string mail)
        {
            return _db.Managers.FirstOrDefault(x => x.Mail.ToLower().Trim() == mail.ToLower().Trim());
        }

        public ICollection<Manager> GetManagers()
        {
            return _db.Managers.OrderBy(x => x.Id).ToList();
        }

        public bool ManagerExists(int id)
        {
            return _db.Managers.Any(x => x.Id == id);
        }

        public bool ManagerExists(string mail)
        {
            return _db.Managers.Any(x => x.Mail.ToLower().Trim() == mail.ToLower().Trim());
        }

        public bool RemoveManager(Manager manager)
        {
            _db.Managers.Remove(manager);
            return Save();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateManager(Manager manager)
        {
            _db.Managers.Update(manager);
            return Save();
        }
    }
}