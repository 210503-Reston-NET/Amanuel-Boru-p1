using System;
using StoreDL;
using StoreModels;
using System.Collections.Generic;

namespace StoreBL
{
    public class ManagerBL
    {
        private ManagerDB _managersDB;

        public ManagerBL(ManagerDB managerDB)
        {
            _managersDB = managerDB;
        }
        public Manager AddManager(Manager manager){
            if (UserNameExists(manager.UserName)){
                throw new Exception("Username has already been taken");
            }
            return _managersDB.AddManager(manager);
        }

        public bool UserNameExists(string userName){
            if (this.GetManager(userName) != null) return true;

            return false;
        }

        public List<Manager> GetAllManagers(){
            return _managersDB.GetAllManagers();
        }

        public Manager GetManager(string username){
            return _managersDB.GetManager(username);
        }
    }
}