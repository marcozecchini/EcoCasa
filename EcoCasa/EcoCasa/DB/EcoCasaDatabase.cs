using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using EcoCasa.Models;
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
            database.CreateTable<Address>();
            database.CreateTable<SmartCasa>();
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

        public User GetUser(int id)
        {
            return database.Table<User>().FirstOrDefault(i => i.ID == id);
        }

        public SmartCasa GetSmartCasa(int id)
        {
            return database.Table<SmartCasa>().FirstOrDefault(i => i.Id == id);
        }

        public List<SmartCasa> GetCasas()
        {
            return database.Query<SmartCasa>("select * from [SmartCasa], [User] where [Email]==[UserEmail]");
        }

        public int SaveUserAsync(User User)
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

        public int SaveSmartCasa(SmartCasa casa)
        {
            if (casa.Id != 0)
            {
                return database.Update(casa);
            }
            else
            {
                return database.Insert(casa);
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

        public int DeleteSessionUser(SessionUser user)
        {
            return database.Delete(user);
        }

        /*public Task<List<SessionUser>> DeleteSessionUserAsync(SessionUser user)
        {
            return database.QueryAsync<SessionUser>("DELETE from SessionUser");
        }*/
    }
}
