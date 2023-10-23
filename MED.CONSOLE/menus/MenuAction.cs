using MED.ATTACH.Objects;
using MED.CONTROL.Objects;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MED.CONSOLE.menus
{

    public class MenuAction
    {
        readonly string path = "";
        
        public MenuAction()
        {
            path = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        }
        public void GarbageClear()
        {
            requestRepo repo = new requestRepo(path);
            repo.GarbageDeleter();
        }
        public bool RegisterUser()
        {
            User user = new User();

            Console.WriteLine("LOGIN:");
            user.login = Console.ReadLine();

            Console.WriteLine("PASSWORD");
            user.password = Console.ReadLine();

            Console.WriteLine("PERMISSIONS");
            user.rights = Console.ReadLine();

            Console.WriteLine("ORGANISATION");
            Organisation  Organisation = new Organisation();
            user.organisation = Organisation.ConvertToOrganisation(Console.ReadLine());

            userRepos repo = new userRepos(path);
            
            return repo.CreateUser(user);
        }
        public User Auth()
        {
            userRepos repo = new userRepos(path);

            Console.WriteLine("LOGIN:");
            string login = Console.ReadLine();

            Console.WriteLine("PASSWORD");
            string password = Console.ReadLine();
            return repo.AuthUser(login, password);


        }
        public bool AddPatient()
        {

            patientRepos repo = new patientRepos(path);

            Patient patient = new Patient();

            Console.WriteLine("NAME:");
            patient.FullName = Console.ReadLine();
            Console.WriteLine("IIN:");
            patient.IIN = Console.ReadLine();
            return repo.CreatePatient(patient);
        }
        public void ShowPatients()
        {
            patientRepos repo = new patientRepos(path);
            repo.ShowPatients();
        }
        public Patient SeekPatients()
        {
            patientRepos repo = new patientRepos(path);

            Console.WriteLine("NAME:");
            string FullName = Console.ReadLine();

            Console.WriteLine("IIN:");
            string IIN = Console.ReadLine();

            return repo.FindPatients(FullName, IIN);
        }
        public void CreateRequest(User user, Patient patient)
        {
            requestRepo repo = new requestRepo(path);

            Console.WriteLine("y/n?");
            char choice = Convert.ToChar(Console.ReadLine());
            switch (choice)
            {
                case 'y':
                    {
                        if(user.rights != "admin")
                        {
                            UserRequests request = new UserRequests(user.organisation, patient);

                            request.status = "pending";
                            request.CreationDate = DateTime.Now;

                            repo.CreateRequest(request);
                        }
                        else
                        {
                            Organisation organisation= new Organisation();
                            Console.WriteLine("ВВЕДИТЕ ИМЯ ОРГАНИЗАЦИИ");
                            organisation.Name = Console.ReadLine();

                            UserRequests request = new UserRequests(organisation, patient);

                            request.status = "pending";
                            request.CreationDate = DateTime.Now;

                            repo.CreateRequest(request);
                            Console.WriteLine("Запрос создан!");
                        }
                        break;
                    }
                case 'n':
                    {
                        Console.WriteLine("возвращаемся в меню...");
                        break;
                    }
            }
        }
        public void ShowRequests(User user)
        {
            requestRepo repo = new requestRepo(path);
            repo.ShowRequestsPending(user);

        }
        public void acceptRequests()
        {
            requestRepo repo = new requestRepo(path);
            Console.WriteLine("y/n?");
            char choice = Convert.ToChar(Console.ReadLine());
            switch (choice)
            {
                case 'y':
                    {

                        Console.WriteLine("Имя организации желаемого запроса:");
                        string OrgName = Console.ReadLine();
                        Console.WriteLine("Имя пациента желаемого запроса");
                        string PatName = Console.ReadLine();

                        Console.WriteLine("Новый статус запроса:");
                        Console.WriteLine("accepted//declined");
                        string newStatus = Console.ReadLine();

                        repo.ViewRequests(newStatus, OrgName, PatName);
                        break;
                    }
                case 'n':
                    {
                        Console.WriteLine("возвращаемся в меню...");
                        break;
                    }
            }
        }
        public void finishedRequests(User user)
        {
            requestRepo repo = new requestRepo(path);
            repo.finishedRequests(user);
        }
    }
}
