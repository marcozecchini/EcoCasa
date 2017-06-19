using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using EcoCasa.DB.Associations;
using EcoCasa.Models;
using EcoCasa.Util;
using SQLite;
using SQLitePCL;

namespace EcoCasa.DB
{
    public class EcoCasaDatabase
    {
        readonly SQLiteConnection database;

        public EcoCasaDatabase(string dbPath)
        {
            database = new SQLiteConnection(dbPath);
            database.CreateTable<User>();
//            database.CreateTable<Address>();
            database.CreateTable<SmartCasa>();
            database.CreateTable<SmartCasaUserAssiociation>();
            database.CreateTable<SmartDevice>();
        }

        public int countSmartCasaSessionUser()
        {
            return database.Query<SmartCasaUserAssiociation>("select * from [SmartCasaUserAssociation]").Count;
        }

        public int CountSessionUser()
        {
            try
            {
                return database.Query<SessionUser>("select count(*) from [SessionUser]").ToArray().Length - 1 ;
            }
            catch
            {
                return 0;
            }
        }

        public SessionUser FindSessionUser()
        {
            var res = database.Query<SessionUser>("Select * from [SessionUser]").ToArray();
            return res[0];
        }

        public User FindUser(User user)
        {
            return database.Table<User>().FirstOrDefault(u => user.Equals(u));
        }

        public SmartDevice GetSmartDevice(SmartDevice device)
        {
            return database.Table<SmartDevice>().FirstOrDefault(u => device.Equals(u));
        }

        public List<User> FindAllUsers()
        {
            return database.Table<User>().Where(user => !user.Email.Equals(Constants.User.Email) ).ToList();
        }


        public User GetUser(int id)
        {
            return database.Table<User>().FirstOrDefault(i => i.ID == id);
        }

        public SmartCasa GetSmartCasa(string code)
        {
            return database.Table<SmartCasa>().FirstOrDefault(i => i.CodeCasa.Equals(code));
        }

        public SmartCasaUserAssiociation GetSmartCasaUserAssiociation(int id)
        {
            return database.Table<SmartCasaUserAssiociation>().FirstOrDefault(i => i.ID == id);
        }

        public List<SmartCasa> GetCasas(User user)
        {
            List<SmartCasa> ris = new List<SmartCasa>();
            ris = database.Table<SmartCasa>().Where(casa => casa.Administrator.Equals(user.Email)).ToList();

            List<SmartCasaUserAssiociation> temp2 = new List<SmartCasaUserAssiociation>();
            temp2 = database.Table<SmartCasaUserAssiociation>().Where(a => a.Email.Equals(user.Email)).ToList();
            List<SmartCasa> ris2 = new List<SmartCasa>();

            foreach (var code in temp2)
            {
                var a = GetSmartCasa(code.CodeCasa);
                if (a != null) ris2.Add(a);
            }

            foreach (var t in ris2) ris.Add(t);

            return ris;
        }

        public List<SmartDevice> GetSmartDevicesOfSmartCasa(string codeCasa)
        {
            return database.Table<SmartDevice>().Where(d => d.CasaCode.Equals(codeCasa)).ToList();
        }

        public List<SmartCasaUserAssiociation> GetSmartCasaUserAssiociations(SmartCasa casa)
        {
            // return the list of utents in this association with the given smartcasa.
            return database.Table<SmartCasaUserAssiociation>().Where(association => association.CodeCasa.Equals(casa.CodeCasa)).ToList();
        }

        public int SaveUser(User User)
        {
            if (User.ID != 0)
            {
                return database.Update(User);
            }
            else
            {
                return database.Insert(User);
            }
        }

        public int SaveSmartCasaUserAssociation(SmartCasaUserAssiociation Association)
        {
            if (Association.ID != Constants.AssociationSmartCasaUserSize)
            {
                return database.Update(Association);
            }
            else
            {
                return database.Insert(Association);
            }
        }

        public int SaveSmartCasa(SmartCasa casa)
        {
            if (casa.Update)
            {
                return database.Update(casa);
            }
            else
            {
                casa.Update = true;
                return database.Insert(casa);
            }
        }

        public int SaveSmartDevice(SmartDevice device)
        {
            if (device.Update)
            {
                return database.Update(device);
            }
            else
            {
                device.Update = true;
                return database.Insert(device);
            }
        }

        public int SaveSessionUser(SessionUser User)
        {
            if (User.ID != 0)
            {
                return database.Update(User);
            }
            else
            {
                return database.Insert(User);
            }
        }

        public int DeleteUser(User user)
        {
            return database.Delete(user);
        }

        public int DeleteAllSmartCasa()
        {
            database.DropTable<SmartCasa>();
            return database.CreateTable<SmartCasa>();
        }

        public int DeleteSmartCasa(SmartCasa casa)
        {
            return database.Delete(casa);
//            database.DropTable<SmartCasa>();
//            return database.CreateTable<SmartCasa>();
        }

        public int DeleteSmartDevice(SmartDevice device)
        {
            return database.Delete(device);
        }

        public int DeleteAllSmartDevice()
        {
            database.DropTable<SmartDevice>();
            return database.CreateTable<SmartDevice>();
        }

        public int DeleteSessionUser(SessionUser user)
        {
            return database.Delete(user);
        }

        public int DeleteSmartCasaUserAssociation(SmartCasaUserAssiociation association)
        {
            return database.Delete(association);
        }

        /*public Task<List<SessionUser>> DeleteSessionUserAsync(SessionUser user)
        {
            return database.QueryAsync<SessionUser>("DELETE from SessionUser");
        }*/
        public int DeleteAlSmartCasaUserAssociation()
        {
            database.DropTable<SmartCasaUserAssiociation>();
            return database.CreateTable<SmartCasaUserAssiociation>();
        }
    }
}
