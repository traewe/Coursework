using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
        public static ISavingAndLoadingStrategy savingAndLoadingStrategy;
        public static LectureCreator unfinishedLectureStateCreator = new UnfinishedLectureStateCreator();
        public static LectureCreator finishedLectureStateCreator = new FinishedLectureStateCreator();
        public static LectureCreator adminLectureStateCreator = new AdminLectureStateCreator();
        public static void SetStrategy(ISavingAndLoadingStrategy strategy)
        {
            savingAndLoadingStrategy = strategy;
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.Unicode;
            int answer;
            string input;
            bool isProgramStopped = false;
            subjectsCollection = new SubjectsCollection();
            userInterface = new AdminCheckingUserInterface(subjectsCollection);
            passwordChecker = new PasswordLengthChecker();

            if (File.Exists("LectureBase.txt"))
            {
                SetStrategy(new PasswordSavingAndLoading());
                savingAndLoadingStrategy.LoadData();
                SetStrategy(new SubjectsSavingAndLoading());
                savingAndLoadingStrategy.LoadData();
                SetStrategy(new ChaptersSavingAndLoading());
                savingAndLoadingStrategy.LoadData();
            }

            while (!isProgramStopped)
            {
                while (selectedSubject == null && !isProgramStopped)
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
                            while (userInterface.subjectsCollection.ContainsName(input) || string.IsNullOrWhiteSpace(input) || input.Contains('+') || input.Contains(';'));

                            userInterface.AddSubject(new Subject(input));

                            SetStrategy(new SubjectsSavingAndLoading());
                            savingAndLoadingStrategy.SaveData();
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

                            SetStrategy(new SubjectsSavingAndLoading());
                            savingAndLoadingStrategy.SaveData();
                            break;
                        case 6:
                            subjectsCollection.ShowOnlyChildren();
                            Console.WriteLine("Введіть назву предмету, якому хочете змінити назву. Або натисніть \"Enter\" з пустою " +
                                "відповіддю для виходу назад");

                            do
                            {
                                input = Console.ReadLine();
                            } while (!string.IsNullOrWhiteSpace(input) && !userInterface.subjectsCollection.ContainsName(input));

                            if (!string.IsNullOrWhiteSpace(input))
                            {
                                Console.WriteLine("Введіть нову назву предмету");

                                string newSubjectName;
                                do
                                {
                                    newSubjectName = Console.ReadLine();
                                }
                                while (userInterface.subjectsCollection.ContainsName(newSubjectName) || string.IsNullOrWhiteSpace(newSubjectName) || newSubjectName.Contains('+') || newSubjectName.Contains(';'));

                                Subject subjectToChange = (Subject)subjectsCollection.GetChildByName(input);

                                subjectToChange.Name = newSubjectName;
                            }

                            SetStrategy(new SubjectsSavingAndLoading());
                            savingAndLoadingStrategy.SaveData();
                            break;
                        case 7:
                            if (string.IsNullOrEmpty(rightPassword))
                            {
                                Console.WriteLine("Щоб мати можливість створювати, видаляти та змінювати об'єкти в цій базі, " +
                                "потрібно створити пароль при першому запуску. Його довжина має бути не менше 7 символів, " +
                                "має бути як мінімум одна маленька, одна велика літера і як мінімум одна цифра. Для виходу натисніть \"Enter\"");
                                do
                                {
                                    input = Console.ReadLine();
                                } while (!passwordChecker.CheckPassword(input) && !string.IsNullOrWhiteSpace(input));

                                if (passwordChecker.CheckPassword(input))
                                {
                                    rightPassword = input;
                                    enteredPassword = input;

                                    SetStrategy(new PasswordSavingAndLoading());
                                    savingAndLoadingStrategy.SaveData();
                                }
                            }
                            else if (string.IsNullOrEmpty(enteredPassword))
                            {
                                Console.WriteLine("Будь ласка, введіть пароль, або натисніть \"Enter\" з пустою строкою для повернення назад. Для " +
                                    "скидання паролю видаліть всі дані");

                                do
                                {
                                    enteredPassword = Console.ReadLine();
                                } while (!string.IsNullOrWhiteSpace(enteredPassword) && !(rightPassword == enteredPassword));
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
                                File.Delete("LectureBase.txt");
                            }

                            break;
                        case 9:
                            isProgramStopped = true;
                            break;
                    }
                }

                while (selectedSubject != null && selectedChapter == null)
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
                            selectedSubject.ShowOnlyChildren();
                            break;
                        case 3:
                            selectedSubject.ShowOnlyChildren();
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
                            Console.WriteLine("Введіть назву розділу, якого ще немає в цьому предметі");

                            do
                            {
                                input = Console.ReadLine();
                            }
                            while (selectedSubject.ContainsName(input) || string.IsNullOrWhiteSpace(input) || input.Contains('+') || input.Contains(';'));

                            userInterface.AddChapter(new Chapter(input));

                            SetStrategy(new ChaptersSavingAndLoading());
                            savingAndLoadingStrategy.SaveData();
                            break;
                        case 5:
                            selectedSubject.ShowOnlyChildren();
                            Console.WriteLine("Введіть назву розділу, який хочете видалити. Або натисніть \"Enter\" з пустою відповіддю для виходу назад");

                            do
                            {
                                input = Console.ReadLine();
                            } while (!string.IsNullOrWhiteSpace(input) && !selectedSubject.ContainsName(input));

                            if (!string.IsNullOrWhiteSpace(input))
                            {
                                Chapter chapterToRemove = (Chapter)selectedSubject.GetChildByName(input);
                                userInterface.RemoveChapter(chapterToRemove);
                            }

                            SetStrategy(new ChaptersSavingAndLoading());
                            savingAndLoadingStrategy.SaveData();
                            break;
                        case 6:
                            selectedSubject.ShowOnlyChildren();
                            Console.WriteLine("Введіть назву розділу, якому хочете змінити назву. Або натисніть \"Enter\" з пустою відповіддю для виходу назад");

                            do
                            {
                                input = Console.ReadLine();
                            } while (!string.IsNullOrWhiteSpace(input) && !selectedSubject.ContainsName(input));

                            if (!string.IsNullOrWhiteSpace(input))
                            {
                                Console.WriteLine("Введіть нову назву розділу");

                                string newChapterName;
                                do
                                {
                                    newChapterName = Console.ReadLine();
                                }
                                while (selectedSubject.ContainsName(newChapterName) || string.IsNullOrWhiteSpace(newChapterName) || newChapterName.Contains("+") || newChapterName.Contains(";"));

                                Chapter chapterToChange = (Chapter)selectedSubject.GetChildByName(input);

                                chapterToChange.Name = newChapterName;
                            }

                            SetStrategy(new ChaptersSavingAndLoading());
                            savingAndLoadingStrategy.SaveData();
                            break;
                        case 7:
                            selectedSubject = null;
                            break;
                    }
                }

                while (selectedChapter != null && selectedLecture == null)
                {
                    userInterface.WriteOptionsThirdStage();

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
                            subjectsCollection.ShowWholeInternalStructure();
                            break;
                        case 2:
                            selectedChapter.ShowOnlyChildren();
                            break;
                        case 3:
                            selectedChapter.ShowOnlyChildren();
                            Console.WriteLine("Введіть назву лекції, в яку хочете перейти. Або натисніть \"Enter\"" +
                                " з пустою відповіддю для виходу назад");

                            do
                            {
                                input = Console.ReadLine();
                            } while (!string.IsNullOrWhiteSpace(input) && !selectedChapter.ContainsName(input));

                            if (!string.IsNullOrWhiteSpace(input))
                            {
                                selectedLecture = (LectureState)selectedChapter.GetChildByName(input);
                            }

                            break;
                        case 4:
                            Console.WriteLine("Введіть назву лекції, якої ще немає в цьому розділі");

                            do
                            {
                                input = Console.ReadLine();
                            }
                            while (selectedChapter.ContainsName(input) || string.IsNullOrWhiteSpace(input) || input.Contains('+') || input.Contains(';'));

                            if (enteredPassword == rightPassword)
                            {
                                Console.WriteLine("Напишіть текст лекції. Потім натисніть \"Enter\" та напишіть посилання " +
                                    "на корисні ресурси через пробіл у формі повних посилань на сайти." +
                                    "Знов натисніть \"Enter\" і введіть повні шляхи розташування файлів лекції через пробіл");

                                selectedChapter.Add(unfinishedLectureStateCreator.CreateLecture(input, Console.ReadLine(), Console.ReadLine(), Console.ReadLine()));

                                SetStrategy(new LecturesSavingAndLoading());
                                savingAndLoadingStrategy.SaveData();
                            }
                            else
                            {
                                Console.WriteLine("У доступі відмовлено, увійдіть як адмін");
                            }

                            break;
                        case 5:
                            selectedChapter.ShowOnlyChildren();
                            Console.WriteLine("Введіть назву лекції, який хочете видалити. Або натисніть \"Enter\" з пустою відповіддю для виходу назад");

                            do
                            {
                                input = Console.ReadLine();
                            } while (!string.IsNullOrWhiteSpace(input) && !selectedChapter.ContainsName(input));

                            if (!string.IsNullOrWhiteSpace(input))
                            {
                                LectureState lectureToRemove = (LectureState)selectedChapter.GetChildByName(input);
                                userInterface.RemoveLecture(lectureToRemove);
                            }

                            SetStrategy(new LecturesSavingAndLoading());
                            savingAndLoadingStrategy.SaveData();
                            break;
                        case 6:
                            selectedChapter.ShowOnlyChildren();
                            Console.WriteLine("Введіть назву розділу, якому хочете змінити назву. Або натисніть \"Enter\" з пустою відповіддю для виходу назад");

                            do
                            {
                                input = Console.ReadLine();
                            } while (!string.IsNullOrWhiteSpace(input) && !selectedChapter.ContainsName(input));

                            if (!string.IsNullOrWhiteSpace(input))
                            {
                                if (selectedChapter.GetChildByName(input) is not UnfinishedLectureState)
                                {
                                    Console.WriteLine("Ця лекція не знаходиться в обробці, тому не можливо змінити її назву");
                                    break;
                                }

                                Console.WriteLine("Введіть нову назву розділу");

                                string newChapterName;
                                do
                                {
                                    newChapterName = Console.ReadLine();
                                }
                                while (selectedChapter.ContainsName(newChapterName) || string.IsNullOrWhiteSpace(newChapterName) || newChapterName.Contains("+") || newChapterName.Contains(";"));

                                LectureState lectureToChange = (LectureState)selectedChapter.GetChildByName(input);

                                lectureToChange.Name = newChapterName;
                            }

                            SetStrategy(new LecturesSavingAndLoading());
                            savingAndLoadingStrategy.SaveData();
                            break;
                        case 7:
                            if (rightPassword == enteredPassword)
                            {
                                selectedChapter.ShowOnlyChildren();

                                Console.WriteLine("Напишіть назву лекції, яку Ви хочете скопіювати. Або натисніть \"Enter\" з пустою відповіддю для виходу назад");

                                do
                                {
                                    input = Console.ReadLine();
                                } while (!string.IsNullOrWhiteSpace(input) && !selectedChapter.ContainsName(input));

                                if (!string.IsNullOrWhiteSpace(input))
                                {
                                    subjectsCollection.ShowWholeInternalStructure();

                                    Console.WriteLine("Напишіть назву розділу, в який бажаєте скопіювати обрану лекцію");

                                    string chapterToCopyLectureName;
                                    bool isChapterNameExists = false;

                                    do
                                    {
                                        chapterToCopyLectureName = Console.ReadLine();

                                        if (string.IsNullOrWhiteSpace(chapterToCopyLectureName))
                                        {
                                            break;
                                        }

                                        for (int i = 0; i < subjectsCollection.Count(); i++)
                                        {
                                            for (int j = 0; j < subjectsCollection[i].Count(); j++)
                                            {
                                                if (subjectsCollection[i][j].Name == chapterToCopyLectureName && !subjectsCollection[i][j].ContainsName(input))
                                                {
                                                    subjectsCollection[i][j].Add(((LectureState)selectedChapter.GetChildByName(input)).Clone());
                                                    isChapterNameExists = true;
                                                }
                                            }
                                        }
                                    } while (!isChapterNameExists);
                                }

                                SetStrategy(new LecturesSavingAndLoading());
                                savingAndLoadingStrategy.SaveData();
                            }
                            else
                            {
                                Console.WriteLine("У доступі відмовлено, увійдіть як адмін");
                            }

                            break;
                        case 8:
                            selectedChapter = null;
                            break;
                    }
                }

                while (selectedLecture != null)
                {
                    userInterface.WriteOptionsFourthStage();

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
                            selectedLecture.ShowText();
                            break;
                        case 3:
                            selectedLecture.OpenURLs();
                            break;
                        case 4:
                            selectedLecture.OpenFiles();
                            break;
                        case 5:
                            Console.WriteLine("Напишіть новий текст для цієї лекції");

                            selectedLecture.ChangeText(Console.ReadLine());
                            break;
                        case 6:
                            Console.WriteLine("Напишіть список нових посилань на сайти через пробіл");

                            selectedLecture.ChangeURLs(Console.ReadLine());
                            break;
                        case 7:
                            Console.WriteLine("Напишіть список нових посилань на файли через пробіл");

                            selectedLecture.ChangeFilesPaths(Console.ReadLine());
                            break;
                        case 9:
                            selectedLecture = null;
                            break;
                    }
                }
            }
        }
    }
}
