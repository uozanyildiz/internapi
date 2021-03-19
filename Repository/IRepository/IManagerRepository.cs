using System.Collections.Generic;
using internapi.Model;

namespace internapi.Repository.IRepository
{
    public interface IManagerRepository
    {
        ICollection<Manager> GetManagers();
        Manager GetManager(int id);
        Manager GetManager(string mail);

        bool CreateManager(Manager manager);
        bool UpdateManager(Manager manager);
        bool RemoveManager(Manager manager);

        bool ManagerExists(int id);
        bool ManagerExists(string mail);
        bool Save();
    }
}