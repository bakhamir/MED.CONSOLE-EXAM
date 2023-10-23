using LiteDB;
using MED.ATTACH.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public void ShowRequestsPending(User user)
        {

                using (var db = new LiteDatabase(connectionString))
                {
                var requests = db.GetCollection<UserRequests>("UserRequests");
                if (user.rights != "admin")
                {
                    var LimitedRequests = requests.Find(u => u.Name == user.organisation.Name);
                    List<UserRequests> limitedReqs = LimitedRequests.ToList();
                    foreach (var request in limitedReqs)
                    {
                        if (request.status != "pending")
                        {
                            continue;
                        }
                        Console.WriteLine($"{request.Name}  {request.Patient.FullName} {request.status} подан - {request.CreationDate}\t");
                    }
                }
                else
                {
                    List<UserRequests> requestslist = requests.FindAll().ToList();
                    foreach (var request in requestslist)
                    {
                        if (request.status != "pending")
                        {
                            continue;
                        }
                        Console.WriteLine($"{request.Name}  {request.Patient.FullName} {request.status} подан - {request.CreationDate}\t");
                    }
                }

                }
        }
        public void ShowRequestsUser(string Name)
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var requests = db.GetCollection<UserRequests>("UserRequests");
            }
        }
        public void ViewRequests(string newStatus,string Name,string PatientName)
        {
            try
            {
                using (var db = new LiteDatabase(connectionString))
                {
                    var requests = db.GetCollection<UserRequests>("UserRequests");
                    var chosenReq = requests.FindOne(u => u.Name == Name && u.Patient.FullName == PatientName);
                    if (chosenReq != null)
                    {

                        chosenReq.status = newStatus;
                        chosenReq.ApprovalDate = DateTime.Now;


                        requests.Update(chosenReq);
                        Console.WriteLine("Статус запроса успешно обновлен.");
                    }
                    else
                    {
                        Console.WriteLine("Запрос не найден.");
                    }


                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void finishedRequests(User user)
        {
            try
            {
                using (var db = new LiteDatabase(connectionString))
                {
                    var requests = db.GetCollection<UserRequests>("UserRequests");
                    if (user.rights != "admin")
                    {
                        var finishedRequests = requests.Find(u => u.status == "accepted" && u.Name == user.organisation.Name);
                        List<UserRequests> finish = finishedRequests.ToList();
                        foreach (var request in finish)
                        {
                            Console.WriteLine($"{request.Name} --- {request.Patient.FullName} принят - {request.ApprovalDate}\t");
                        }
                    }
                    else
                    {
                        var finishedRequests = requests.Find(u => u.status == "accepted");
                        List<UserRequests> finish = finishedRequests.ToList();
                        foreach (var request in finish)
                        {
                            Console.WriteLine($"{request.Name} --- {request.Patient.FullName} принят - {request.ApprovalDate}\t");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public void GarbageDeleter()
        {
            using (var db = new LiteDatabase(connectionString))
            {
                var requests = db.GetCollection<UserRequests>("UserRequests");
                requests.DeleteMany(u => u.status == "declined");
            }
        }

    }
}
