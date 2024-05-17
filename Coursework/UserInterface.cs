using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    public class UserInterface : UserInterfaceAbstraction
    {
        public UserInterface(SubjectsCollection subjectsCollection)
        {
            this.subjectsCollection = subjectsCollection;
        }

        public override void AddSubject(Subject subject)
        {
            subjectsCollection.Add(subject);
        }

        public override void RemoveSubject(Subject subject)
        {
            subjectsCollection.Remove(subject);
        }

        public override void AddChapter(Chapter chapter)
        {
            subjectsCollection.GetChildByName(Program.selectedSubject.Name).Add(chapter);
        }

        public override void RemoveChapter(Chapter chapter)
        {
            subjectsCollection.GetChildByName(Program.selectedSubject.Name).Remove(chapter);
        }

        public override void RemoveLecture(LectureState lecture)
        {
            subjectsCollection.GetChildByName(Program.selectedSubject.Name).GetChildByName(Program.selectedChapter.Name).Remove(lecture);
        }

        public override void WriteOptionsFirstStage()
        {
            Console.WriteLine("==============================");
            Console.WriteLine("1 - Переглянути всю структуру (предмети - розділи - лекції)");
            Console.WriteLine("2 - Переглянути список всіх предметів");
            Console.WriteLine("3 - Перейти у певний предмет");
            Console.WriteLine("4 - Додати предмет");
            Console.WriteLine("5 - Видалити предмет");
            Console.WriteLine("6 - Змінити назву вже наявного предмету");
            Console.WriteLine("7 - Увійти як адмін (або встановити пароль, якщо його ще немає)");
            Console.WriteLine("8 - Очистити всі дані");
            Console.WriteLine("9 - Вийти");
            Console.WriteLine("==============================");
        }
        public override void WriteOptionsSecondStage()
        {
            Console.WriteLine("==============================");
            Console.WriteLine("1 - Переглянути всю структуру (предмети - розділи - лекції)");
            Console.WriteLine("2 - Переглянути список всіх розділів у цьому предметі");
            Console.WriteLine("3 - Перейти у певний розділ");
            Console.WriteLine("4 - Додати розділ");
            Console.WriteLine("5 - Видалити розділ");
            Console.WriteLine("6 - Змінити назву вже наявного розділу");
            Console.WriteLine("7 - Вийти у попереднє меню");
            Console.WriteLine("==============================");
        }
        public override void WriteOptionsThirdStage()
        {
            Console.WriteLine("==============================");
            Console.WriteLine("1 - Переглянути всю структуру (предмети - розділи - лекції)");
            Console.WriteLine("2 - Переглянути список всіх лекцій у цьому розділі");
            Console.WriteLine("3 - Перейти у певну лекцію");
            Console.WriteLine("4 - Додати лекцію");
            Console.WriteLine("5 - Видалити лекцію");
            Console.WriteLine("6 - Змінити назву вже наявної лекції");
            Console.WriteLine("7 - Скопіювати лекцію в інший розділ");
            Console.WriteLine("8 - Вийти у попереднє меню");
            Console.WriteLine("==============================");
        }
        public override void WriteOptionsFourthStage()
        {
            Console.WriteLine("==============================");
            Console.WriteLine("1 - Переглянути всю структуру (предмети - розділи - лекції)");
            Console.WriteLine("2 - Переглянути текст лекції");
            Console.WriteLine("3 - Відкрити посилання лекції");
            Console.WriteLine("4 - Відкрити файли лекції");
            Console.WriteLine("5 - Змінити текст лекції");
            Console.WriteLine("6 - Змінити посилання лекції");
            Console.WriteLine("7 - Змінити файли лекції");
            Console.WriteLine("8 - Змінити статус лекції");
            Console.WriteLine("9 - Вийти у попереднє меню");
            Console.WriteLine("==============================");
        }
    }

    public class AdminCheckingUserInterface : UserInterfaceAbstraction
    {
        UserInterface userInterface;
        string password;

        public AdminCheckingUserInterface(SubjectsCollection subjectsCollection)
        {
            this.subjectsCollection = subjectsCollection;
            userInterface = new UserInterface(subjectsCollection);
        }

        public override void AddSubject(Subject subject)
        {
            if (CheckAccess())
            {
                userInterface.AddSubject(subject);
            }
            else
            {
                Console.WriteLine("У доступі відмовлено, увійдіть як адмін");
            }
        }

        public override void RemoveSubject(Subject subject)
        {
            if (CheckAccess())
            {
                userInterface.RemoveSubject(subject);
            }
            else
            {
                Console.WriteLine("У доступі відмовлено, увійдіть як адмін");
            }
        }

        public override void WriteOptionsFirstStage()
        {
            userInterface.WriteOptionsFirstStage();
        }

        public override void WriteOptionsSecondStage()
        {
            userInterface.WriteOptionsSecondStage();
        }

        public override void WriteOptionsThirdStage()
        {
            userInterface.WriteOptionsThirdStage();
        }

        public override void WriteOptionsFourthStage()
        {
            userInterface.WriteOptionsFourthStage();
        }

        public override void AddChapter(Chapter chapter)
        {
            if (CheckAccess())
            {
                subjectsCollection.GetChildByName(Program.selectedSubject.Name).Add(chapter);
            }
            else
            {
                Console.WriteLine("У доступі відмовлено, увійдіть як адмін");
            }
        }

        public override void RemoveChapter(Chapter chapter)
        {
            if (CheckAccess())
            {
                subjectsCollection.GetChildByName(Program.selectedSubject.Name).Remove(chapter);
            }
            else
            {
                Console.WriteLine("У доступі відмовлено, увійдіть як адмін");
            }
        }

        public override void RemoveLecture(LectureState lecture)
        {
            if (CheckAccess())
            {
                subjectsCollection.GetChildByName(Program.selectedSubject.Name).GetChildByName(Program.selectedChapter.Name).Remove(lecture);
            }
            else
            {
                Console.WriteLine("У доступі відмовлено, увійдіть як адмін");
            }
        }
        public bool CheckAccess()
        {
            return Program.rightPassword == Program.enteredPassword;
        }
    }
}
