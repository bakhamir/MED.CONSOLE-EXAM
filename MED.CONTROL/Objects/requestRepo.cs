using LiteDB;
using MED.ATTACH.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MED.CONTROL.Objects
{
    public class requestRepo
    {
        readonly string connectionString = "";

        public requestRepo(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool CreateRequest(UserRequests request)
        {
            try
            {
                using (var db = new LiteDatabase(connectionString))
                {
                    var requests = db.GetCollection<UserRequests>("UserRequests");
                    requests.Insert(request);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
    }
}
