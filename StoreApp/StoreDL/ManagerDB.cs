using System.Collections.Generic;
using StoreModels;
using System.Linq;

namespace StoreDL
{
    public class ManagerDB
    {
        private StoreDBContext _context;
        public ManagerDB(StoreDBContext context)
        {
            _context = context;
        }

        public Manager AddManager(Manager manager){
            _context.Managers.Add(manager);

            _context.SaveChanges();
            return manager;
        }
        public List<Manager> GetAllManagers(){
            List<Manager> managers = _context.Managers
            .Select( manager => new Manager(manager.Name, manager.UserName, manager.Password))
                .ToList();
            
            return managers;
        }

        public Manager GetManager(string username){
            Manager found = _context.Managers.FirstOrDefault(manager =>  manager.UserName == username);

            if (found == null) return null;
            else{
                return new Manager(found.Name, found.UserName, found.Password);
            }
        }
    }
}