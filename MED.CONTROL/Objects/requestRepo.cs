using LiteDB;
using MED.ATTACH.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        public void ShowRequests()
        {

                using (var db = new LiteDatabase(connectionString))
                {
                    var requests = db.GetCollection<UserRequests>("UserRequests");
                    List<UserRequests> requestslist = requests.FindAll().ToList();
                    foreach (var request in requestslist)
                    {
                        Console.WriteLine($"{request.Name} {request.CreationDate} {request.Patient.FullName} {request.status}\t");
                    }
                }
        }
        public void ShowRequestsUser(string Name)
        {

            using (var db = new LiteDatabase(connectionString))
            {
                var requests = db.GetCollection<UserRequests>("UserRequests");
                var LimitedRequests = requests.Find(u => u.Name == Name);
                List<UserRequests> limitedReqs = LimitedRequests.ToList();
                foreach (var request in limitedReqs)
                {
                    Console.WriteLine($"{request.Name} {request.CreationDate} {request.Patient.FullName} {request.status}\t");
                }
            }
        }
        public void ViewRequests(string newStatus,string Name,string PatientName)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var requests = db.GetCollection<UserRequests>("UserRequests");
                var chosenReq = requests.FindOne(u => u.Name == Name && u.Patient.FullName == PatientName);
                if (chosenReq != null)
                {
                    // Изменяем поле status
                    chosenReq.status = newStatus;

                    // Обновляем запись в базе данных
                    _ = requests.Update(chosenReq);

                    Console.WriteLine("Статус запроса успешно обновлен.");
                }
                else
                {
                    Console.WriteLine("Запрос не найден.");
                }


            }
        }
        public void finishedRequests()
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var requests = db.GetCollection<UserRequests>("UserRequests");
                var finishedRequests = requests.Find(u => u.status == "accepted");
                List<UserRequests> finish = finishedRequests.ToList();
                foreach (var request in finish)
                {
                    Console.WriteLine($"{request.Name} --- {request.Patient.FullName}\t");
                }
            }
        }
        public void finishedRequestsUser(User user)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var requests = db.GetCollection<UserRequests>("UserRequests");
                var finishedRequests = requests.Find(u => u.status == "accepted" && u.Name == user.organisation.Name);
                List<UserRequests> finish = finishedRequests.ToList();
                foreach (var request in finish)
                {
                    Console.WriteLine($"{request.Name} --- {request.Patient.FullName}\t");
                }
            }
        }
    }
}
