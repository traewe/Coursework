using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    public static class Program
    {
        public static string enteredPassword;
        public static UserInterfaceAbstraction userInterface;
        public static SubjectsCollection subjectsCollection;
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            bool isProgramStopped = false;
            subjectsCollection = new SubjectsCollection();
            userInterface = new AdminCheckingUserInterface(subjectsCollection);

            while (!isProgramStopped)
            {
                userInterface.WriteOptionsFirstStage();
                int answer;
                string input;

                while (true)
                {
                    input = Console.ReadLine();

                    if (int.TryParse(input, out answer))
                    {
                        if (answer >= 1 && answer <= 8)
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
                        userInterface.subjectsCollection.ShowWholeInternalStructure();
                        break;
                    case 2:
                        userInterface.subjectsCollection.ShowOnlyChildren();
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
                            Subject subjectToRemove = subjectsCollection.GetSubjectByName(input);
                            if (subjectToRemove != null)
                            {
                                userInterface.RemoveSubject(subjectToRemove);
                            }
                            else
                            {
                                Console.WriteLine("Предмет не знайдено.");
                            }
                        }
                        break;
                    case 6:
                        userInterface.subjectsCollection.ShowOnlyChildren();
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

                            Subject subjectToChange = subjectsCollection.GetSubjectByName(input);
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

                        break;
                    case 8:
                        isProgramStopped = true;
                        break;
                }
            }
        }
    }
}
