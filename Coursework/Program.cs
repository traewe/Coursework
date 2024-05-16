using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Coursework
{
    public static class Program
    {
        public static string enteredPassword;
        public static string rightPassword;
        public static UserInterfaceAbstraction userInterface;
        public static SubjectsCollection subjectsCollection;
        public static IPasswordChecker passwordChecker;
        public static Subject selectedSubject;
        public static Chapter selectedChapter;
        public static LectureState selectedLecture;
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            int answer;
            string input;
            bool isProgramStopped = false;
            subjectsCollection = new SubjectsCollection();
            userInterface = new AdminCheckingUserInterface(subjectsCollection);
            passwordChecker = new PasswordLengthChecker();

            while (!isProgramStopped)
            {
                while (selectedSubject == null)
                {
                    userInterface.WriteOptionsFirstStage();

                    while (true)
                    {
                        input = Console.ReadLine();

                        if (int.TryParse(input, out answer))
                        {
                            if (answer >= 1 && answer <= 9)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Введена відповідь не відповідає вимогам. Спробуйте ще раз");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Введена відповідь не відповідає вимогам. Спробуйте ще раз");
                        }
                    }

                    switch (answer)
                    {
                        case 1:
                            subjectsCollection.ShowWholeInternalStructure();
                            break;
                        case 2:
                            subjectsCollection.ShowOnlyChildren();
                            break;
                        case 3:
                            subjectsCollection.ShowOnlyChildren();
                            Console.WriteLine("Введіть назву предмету, в який хочете перейти. Або натисніть \"Enter\"" +
                                " з пустою відповіддю для виходу назад");

                            do
                            {
                                input = Console.ReadLine();
                            } while (!string.IsNullOrWhiteSpace(input) && !subjectsCollection.ContainsName(input));

                            if (!string.IsNullOrWhiteSpace(input))
                            {
                                selectedSubject = (Subject)subjectsCollection.GetChildByName(input);
                            }

                            break;
                        case 4:
                            Console.WriteLine("Введіть назву предмету, якої ще немає в базі");

                            do
                            {
                                input = Console.ReadLine();
                            }
                            while (userInterface.subjectsCollection.ContainsName(input) || string.IsNullOrWhiteSpace(input));

                            userInterface.AddSubject(new Subject(input));
                            break;
                        case 5:
                            userInterface.subjectsCollection.ShowOnlyChildren();
                            Console.WriteLine("Введіть назву предмету, який хочете видалити. Або натисніть \"Enter\" з пустою відповіддю для виходу назад");

                            do
                            {
                                input = Console.ReadLine();
                            } while (!string.IsNullOrWhiteSpace(input) && !userInterface.subjectsCollection.ContainsName(input));

                            if (!string.IsNullOrWhiteSpace(input))
                            {
                                Subject subjectToRemove = (Subject)subjectsCollection.GetChildByName(input);
                                userInterface.RemoveSubject(subjectToRemove);
                            }

                            break;
                        case 6:
                            subjectsCollection.ShowOnlyChildren();
                            Console.WriteLine("Введіть назву предмету, якому хочете змінити назву. Або натисніть \"Enter\" з пустою відповіддю для виходу назад");

                            do
                            {
                                input = Console.ReadLine();
                            } while (!string.IsNullOrWhiteSpace(input) && !userInterface.subjectsCollection.ContainsName(input));

                            if (!string.IsNullOrWhiteSpace(input))
                            {
                                Console.WriteLine("Введіть нову назву предмету:");

                                string newSubjectName;
                                do
                                {
                                    newSubjectName = Console.ReadLine();
                                }
                                while (string.IsNullOrWhiteSpace(newSubjectName));

                                Subject subjectToChange = (Subject)subjectsCollection.GetChildByName(input);
                                if (subjectToChange != null)
                                {
                                    subjectToChange.Name = newSubjectName;
                                }
                                else
                                {
                                    Console.WriteLine("Предмет не знайдено.");
                                }
                            }
                            break;
                        case 7:
                            if (string.IsNullOrEmpty(rightPassword))
                            {
                                Console.WriteLine("Щоб мати можливість створювати, видаляти та змінювати об'єкти в цій базі, " +
                                "потрібно створити пароль при першому запуску. Його довжина має бути не менше 7 символів, " +
                                "має бути як мінімум одна маленька і одна велика літера, як мінімум один символ з цих: _.,%. Для виходу натисніть \"Enter\"");
                                do
                                {
                                    input = Console.ReadLine();
                                } while (!passwordChecker.CheckPassword(input) && !string.IsNullOrWhiteSpace(input));

                                if (passwordChecker.CheckPassword(input))
                                {
                                    rightPassword = input;
                                    enteredPassword = input;
                                }
                            }
                            else if (string.IsNullOrEmpty(enteredPassword))
                            {
                                Console.WriteLine("Будь ласка, введіть пароль, або натисніть \"Enter\" з пустою строкою. Для " +
                                    "скидання паролю видаліть всі дані");

                                do
                                {
                                    input = Console.ReadLine();
                                } while (!string.IsNullOrWhiteSpace(input) && !(rightPassword == enteredPassword));
                            }
                            else
                            {
                                Console.WriteLine("Ви вже увійшли як адмін. Пароль можна змінити лише після видалення всіх даних");
                            }

                            break;
                        case 8:
                            Console.WriteLine("Ви точно впевнені, що хочете видалити всі дані? Їх відновлення буде неможливим." +
                                " Напишіть \"Видалити все\" для підтвердження. Інша відповідь поверне в головне меню");
                            input = Console.ReadLine();

                            if (input == "Видалити все")
                            {
                                subjectsCollection.Clear();
                                enteredPassword = string.Empty;
                                rightPassword = string.Empty;
                            }

                            break;
                        case 9:
                            isProgramStopped = true;
                            break;
                    }
                }

                while (selectedSubject != null)
                {
                    userInterface.WriteOptionsSecondStage();

                    while (true)
                    {
                        input = Console.ReadLine();

                        if (int.TryParse(input, out answer))
                        {
                            if (answer >= 1 && answer <= 7)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Введена відповідь не відповідає вимогам. Спробуйте ще раз");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Введена відповідь не відповідає вимогам. Спробуйте ще раз");
                        }
                    }

                    switch (answer)
                    {
                        case 1:
                            subjectsCollection.ShowWholeInternalStructure();
                            break;
                        case 2:
                            subjectsCollection.ShowOnlyChildren();
                            break;
                        case 3:
                            subjectsCollection.ShowOnlyChildren();
                            Console.WriteLine("Введіть назву розділу, в який хочете перейти. Або натисніть \"Enter\"" +
                                " з пустою відповіддю для виходу назад");

                            do
                            {
                                input = Console.ReadLine();
                            } while (!string.IsNullOrWhiteSpace(input) && !selectedSubject.ContainsName(input));

                            if (!string.IsNullOrWhiteSpace(input))
                            {
                                selectedChapter = (Chapter)selectedSubject.GetChildByName(input);
                            }

                            break;
                        case 4:
                            Console.WriteLine("Введіть назву розділу, якої ще немає в цьому предметі");

                            do
                            {
                                input = Console.ReadLine();
                            }
                            while (selectedSubject.ContainsName(input) || string.IsNullOrWhiteSpace(input));

                            userInterface.AddSubject(new Subject(input));
                            break;
                        case 5:
                            subjectsCollection.ShowOnlyChildren();
                            Console.WriteLine("Введіть назву предмету, який хочете видалити. Або натисніть \"Enter\" з пустою відповіддю для виходу назад");

                            do
                            {
                                input = Console.ReadLine();
                            } while (!string.IsNullOrWhiteSpace(input) && !subjectsCollection.ContainsName(input));

                            if (!string.IsNullOrWhiteSpace(input))
                            {
                                Subject subjectToRemove = subjectsCollection.GetSubjectByName(input);
                                userInterface.RemoveSubject(subjectToRemove);
                            }

                            break;
                    }
            }
        }
    }
}
