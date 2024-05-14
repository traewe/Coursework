using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    public class UserInterface : IUserInterface
    {
        public SubjectsCollection subjectsCollection;

        public void AddSubject(Subject subject)
        {
            subjectsCollection.Add(subject);
        }

        public void WriteOptionsFirstStage()
        {
            Console.WriteLine("==============================");
            Console.WriteLine("1 - Переглянути всю структуру (предмети - розділи - лекції)");
            Console.WriteLine("2 - Переглянути список всіх предметів");
            Console.WriteLine("3 - Перейти у певний предмет");
            Console.WriteLine("4 - Додати предмет");
            Console.WriteLine("5 - Увійти як адмін (або встановити пароль, якщо його ще немає)");
            Console.WriteLine("6 - Вийти");
            Console.WriteLine("==============================");
        }
    }

    public class AdminCheckingUserInterface : IUserInterface
    {
        UserInterface userInterface;
        public SubjectsCollection subjectsCollection;
        string password;

        public AdminCheckingUserInterface()
        {
            userInterface = new UserInterface();
            subjectsCollection = new SubjectsCollection();
        }

        public void SetPassword(string password)
        {
            if (this.password == null)
            {
                this.password = password;
            }
        }

        public void AddSubject(Subject subject)
        {
            if (CheckAccess())
            {
                userInterface.AddSubject(subject);
            }
            else
            {
                Console.WriteLine("Access denied. Please login as admin.");
            }
        }

        public void WriteOptionsFirstStage()
        {
            userInterface.WriteOptionsFirstStage();
        }

        public bool CheckAccess()
        {
            return password == Program.enteredPassword;
        }
    }
}
