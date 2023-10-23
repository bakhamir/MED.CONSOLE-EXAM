using MED.ATTACH.Objects;
using MED.CONSOLE.menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MED.CONSOLE
{
    public static class Menu
    {
        public static void MenuFirst()
        {
            MenuAction menuAction = new MenuAction();

            Console.WriteLine("Добро пожаловать в медицинский центр!");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("1 - авторизация");
            Console.WriteLine("2 - регистрация");
            Console.WriteLine("3 - выход");
            Console.WriteLine("--------------------------------------");
            int choice = Int32.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1: 
                    { 
                        Console.Clear();
                        User user = menuAction.Auth();
                        if (user == null)
                        {
                            Console.WriteLine("AUTH FAIL");
                        }
                        else
                        {
                            MenuSecond(user);
                        }
                        break;
                    }
                case 2:
                    {
                        Console.Clear();
                        if (menuAction.RegisterUser())
                        {
                            Console.Clear();
                            MenuFirst();
                        }
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("GOODBYE");
                        Environment.Exit(0);
                        break;
                    }
                default:
                        Environment.Exit(0);
                        break;
            }
        }
        public static void MenuSecond(User user)
        {
            MenuAction menuAction = new MenuAction();
            Console.Clear();
            Console.WriteLine($"Добро пожаловать {user.login}!");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("1 - внести нового пациента");
            Console.WriteLine("2 - поиск среди пациентов");
            Console.WriteLine("3 - просмотреть всех пациентов");
            Console.WriteLine("4 - просмотреть запросы на связку (требуются права администратора для полного просмотра)");
            Console.WriteLine("5 - просмотреть связанных пациентов (требуются права администратора для полного просмотра)");
            Console.WriteLine("6 - выход");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"ВАШ УРОВЕНЬ ДОСТУПА - {user.rights}");
            //menuAction.GetHashCode();
            int choice = Int32.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    {
                        Console.WriteLine();

                        if (menuAction.AddPatient())
                        {
                            Console.Clear();
                            MenuSecond(user);
                        }
                        break;
                    }
                    case 2:
                    {
                        Console.Clear();
                        Patient patient = menuAction.SeekPatients();
                        if(patient != null )
                        {
                            Console.WriteLine($"По запросу найден пациент: {patient.FullName} {patient.IIN}\t");
                            Console.WriteLine("Создать запрос на прикрепление ?");
                            menuAction.CreateRequest(user, patient);

                        }
                        else
                        {
                            Console.WriteLine("Пациент по данному запросу не найден");
                        }

                        Console.ReadKey();
                        Console.Clear();
                        MenuSecond(user);

                        break;
                    }
                    case 3: { 
                        Console.Clear();
                        menuAction.ShowPatients();

                        Console.ReadKey();
                        Console.Clear();
                        MenuSecond(user);

                        break;
                    }
                case 4:
                    {


                        Console.Clear();
                        menuAction.ShowRequests(user);

                        Console.WriteLine("Рассмотреть запросы?");
                        menuAction.acceptRequests();
                        menuAction.GarbageClear();


                        Console.ReadKey();
                        Console.Clear();
                        MenuSecond(user);

                        break;
                    }
                case 5:
                    {
                        Console.Clear();

                        menuAction.finishedRequests(user);
                        Console.ReadKey();
                        Console.Clear();
                        MenuSecond(user);
                        break;
                    }
                case 6:
                    {
                        Console.Clear();
                        break;
                    }
                    default:
                    {
                        Console.Clear();
                        Console.WriteLine("INCORRECT INPUT");
                        break;
                    }
            }
        }
    }
}
