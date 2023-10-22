using LiteDB;
using System;
using MED.ATTACH.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MED.CONTROL.Objects
{
    public class userRepos
    {
        readonly string connectionString = "";

        public userRepos(string connectionString) 
        {
            this.connectionString = connectionString;
        }

        public bool CreateUser(User user)
        {
            try
            {
                using (var db = new LiteDatabase(connectionString))
                {
                    var users = db.GetCollection<User>("User");
                    users.Insert(user);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
        public User AuthUser(string login,string password)
        {
            try
            {
                using (var db = new LiteDatabase(connectionString))
                {
                    var users = db.GetCollection<User>("User");
                    var user = users.FindOne(u => u.login == login && u.password == password);
                    return user;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }
            return null;
        }
    }
}
