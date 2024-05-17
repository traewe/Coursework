using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
namespace Coursework
{
    public class SubjectsCollection : ICompositeElement
    {
        List<Subject> subjects = new List<Subject>();

        public Subject this[int index]
        {
            get
            {
                if (index >= 0 || index < subjects.Count)
                {
                    return subjects[index];
                }

                return null;
            }
            set
            {
                if (index >= 0 || index < subjects.Count)
                {
                    subjects[index] = value;
                }
            }
        }
        public void ShowWholeInternalStructure()
        {
            for (int i = 0; i < subjects.Count; i++)
            {
                if (Program.selectedSubject == subjects[i])
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.Write($"{i + 1} ");
                Console.ForegroundColor = ConsoleColor.Gray;
                subjects[i].ShowWholeInternalStructure();
            }
        }

        public void ShowOnlyChildren()
        {
            for (int i = 0; i < subjects.Count; i++)
            {
                Console.WriteLine($"{i + 1} {subjects[i].Name}");
            }
        }

        public ICompositeElement GetChildByName(string name)
        {
            for (int i = 0; i < subjects.Count(); i++)
            {
                if (subjects[i].Name == name)
                {
                    return subjects[i];
                }
            }

            return null;
        }

        public void Add(ICompositeElement subject)
        {
            if (subject is Subject)
            {
                subjects.Add((Subject)subject);
                Sort();
            }
        }

        public void Remove(ICompositeElement subject)
        {
            if (subject is Subject)
            {
                subjects.Remove((Subject)subject);
            }
        }

        public int Count()
        {
            return subjects.Count;
        }

        public void Clear()
        {
            subjects.Clear();
        }
        
        public void Sort()
        {
            subjects.Sort((x, y) => x.Name.CompareTo(y.Name));
        }
        
        public string GetStringForSaving()
        {
            string result = "";
            Sort();

            for (int i = 0; i < subjects.Count; i++)
            {
                result += subjects[i].Name;
                if (i != subjects.Count - 1)
                {
                    result += ";";
                }
            }

            return result;
        }

        public int IndexOf(ICompositeElement element)
        {
            if (element is Subject)
            {
                return subjects.IndexOf((Subject)element);
            }

            return -1;
        }

        public bool ContainsName(string name)
        {
            foreach (Subject subject in subjects)
            {
                if (subject.Name == name)
                {
                    return true;
                }
            }

            return false;
        }
    }

    public class Subject : ICompositeElement
    {
        protected List<Chapter> chapters = new List<Chapter>();

        public string Name { get; set; }

        public Subject(string name)
        {
            Name = name;
        }
        public Chapter this[int index]
        {
            get
            {
                if (index >= 0 || index < chapters.Count)
                {
                    return chapters[index];
                }

                return null;
            }
            set
            {
                if (index >= 0 || index < chapters.Count)
                {
                    chapters[index] = value;
                }
            }
        }
        public void ShowWholeInternalStructure()
        {
            if (Program.selectedSubject == this)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            Console.WriteLine(Name);
            Console.ForegroundColor = ConsoleColor.Gray;

            for (int i = 0; i < chapters.Count; i++)
            {
                if (Program.selectedChapter == chapters[i])
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.Write($" {i + 1} ");
                Console.ForegroundColor = ConsoleColor.Gray;

                chapters[i].ShowWholeInternalStructure();
            }
        }

        public void ShowOnlyChildren()
        {
            for (int i = 0; i < chapters.Count; i++)
            {
                Console.WriteLine($"{i + 1} {chapters[i].Name}");
            }
        }

        public ICompositeElement GetChildByName(string name)
        {
            for (int i = 0; i < chapters.Count(); i++)
            {
                if (chapters[i].Name == name)
                {
                    return chapters[i];
                }
            }

            return null;
        }

        public void Add(ICompositeElement chapter)
        {
            if (chapter is Chapter)
            {
                chapters.Add((Chapter)chapter);
                Sort();
            }
        }

        public void Remove(ICompositeElement chapter)
        {
            if (chapter is Chapter)
            {
                chapters.Remove((Chapter)chapter);
            }
        }

        public int Count()
        {
            return chapters.Count;
        }

        public void Clear()
        {
            chapters.Clear();
        }

        public void Sort()
        {
            chapters.Sort((x, y) => x.Name.CompareTo(y.Name));
        }

        public string GetStringForSaving()
        {
            string result = "";

            for (int i = 0; i < chapters.Count(); i++)
            {
                result += $"{chapters[i].Name}+{Program.subjectsCollection.IndexOf(this)};";
            }

            return result;
        }

        public int IndexOf(ICompositeElement element)
        {
            if (element is Chapter)
            {
                return chapters.IndexOf((Chapter)element);
            }

            return -1;
        }

        public bool ContainsName(string name)
        {
            foreach (Chapter chapter in chapters)
            {
                if (chapter.Name == name)
                {
                    return true;
                }
            }

            return false;
        }
    }

    public class Chapter : ICompositeElement
    {
        protected List<LectureState> lectures = new List<LectureState>();

        public string Name { get; set; }

        public Chapter(string name)
        {
            this.Name = name;
        }

        public void ShowWholeInternalStructure()
        {
            if (Program.selectedChapter == this)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            Console.WriteLine(Name);
            Console.ForegroundColor = ConsoleColor.Gray;

            for (int i = 0; i < lectures.Count; i++)
            {
                if (Program.selectedLecture == lectures[i])
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.Write($"  {i + 1} ");
                Console.ForegroundColor = ConsoleColor.Gray;
                
                lectures[i].ShowWholeInternalStructure();
            }
        }

        public void ShowOnlyChildren()
        {
            for (int i = 0; i < lectures.Count; i++)
            {
                Console.WriteLine($"{i + 1} {lectures[i].Name}");
            }
        }

        public ICompositeElement GetChildByName(string name)
        {
            for (int i = 0; i < lectures.Count(); i++)
            {
                if (lectures[i].Name == name)
                {
                    return lectures[i];
                }
            }

            return null;
        }

        public void Add(ICompositeElement lecture)
        {
            if (lecture is LectureState)
            {
                lectures.Add((LectureState)lecture);
                Sort();
            }
        }

        public void Remove(ICompositeElement lecture)
        {
            if (lecture is LectureState)
            {
                lectures.Remove((LectureState)lecture);
            }
        }

        public int Count()
        {
            return lectures.Count;
        }

        public void Clear()
        {
            lectures.Clear();
        }

        public void Sort()
        {
            lectures.Sort((x, y) => x.Name.CompareTo(y.Name));
        }

        public int IndexOf(ICompositeElement element)
        {
            if (element is LectureState)
            {
                return lectures.IndexOf((LectureState)element);
            }

            return -1;
        }

        public string GetStringForSaving()
        { 
            return string.Empty;
        }

        public string GetStringForSaving(Subject subject)
        {
            string result = "";

            for (int i = 0; i < lectures.Count(); i++)
            {
                result += $"{lectures[i].Name}+{lectures[i].Text}+{string.Join(" ", lectures[i].URLs)}+{string.Join(" ", lectures[i].FilesPaths)}+{subject.IndexOf(this)}+{Program.subjectsCollection.IndexOf(subject)}+";

                if (lectures[i] is UnfinishedLectureState)
                {
                    result += "un;";
                }
                else if (lectures[i] is FinishedLectureState)
                {
                    result += "fi;";
                }
                else
                {
                    result += "ad;";
                }
            }

            return result;
        }
        public bool ContainsName(string name)
        {
            foreach (LectureState lecture in lectures)
            {
                if (lecture.Name == name)
                {
                    return true;
                }
            }

            return false;
        }
    }
    public class UnfinishedLectureState : LectureState
    {
        AdminCheckingUserInterface adminCheckingUserInterface = new AdminCheckingUserInterface(Program.subjectsCollection);
        public override void ShowText()
        {
            Console.WriteLine(Text);
        }
        public override void OpenURLs()
        {
            foreach (string url in URLs)
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    });
                }
                catch
                {
                    Console.WriteLine("Неможливо відкрити одне з посилань");
                }
            }
        }
        public override void OpenFiles()
        {
            foreach (string filePath in FilesPaths)
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    });
                }
                catch
                {
                    Console.WriteLine("Неможливо відкрити один з файлів");
                }
            }
        }
        public override void ChangeText(string text)
        {
            if (adminCheckingUserInterface.CheckAccess())
            {
                Text = text;
            }
        }
        public override void ChangeURLs(string urls)
        {
            if (adminCheckingUserInterface.CheckAccess())
            {
                URLs = urls.Split(" ");
            }
        }
        public override void ChangeFilesPaths(string filesPaths)
        {
            if (adminCheckingUserInterface.CheckAccess())
            {
                FilesPaths = filesPaths.Split(" ");
            }
        }

        public override string ShowStatus()
        {
            return "(в розробці)";
        }
    }
    public class FinishedLectureState : LectureState
    {
        public override void ShowText()
        {
            Console.WriteLine(Text);
        }
        public override void OpenURLs()
        {
            foreach (string url in URLs)
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    });
                }
                catch
                {
                    Console.WriteLine("Неможливо відкрити одне з посилань");
                }
            }
        }
        public override void OpenFiles()
        {
            foreach (string filePath in FilesPaths)
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    });
                }
                catch
                {
                    Console.WriteLine("Неможливо відкрити один з файлів");
                }
            }
        }

        public override string ShowStatus()
        {
            return "(закінчена)";
        }

        public override void ChangeText(string text)
        {
            Console.WriteLine("Не можна змінити закінчену лекцію");
        }
        public override void ChangeURLs(string urls)
        {
            Console.WriteLine("Не можна змінити закінчену лекцію");
        }
        public override void ChangeFilesPaths(string filesPaths)
        {
            Console.WriteLine("Не можна змінити закінчену лекцію");
        }
    }
    public class AdminLectureState : LectureState
    {
        AdminCheckingUserInterface adminCheckingUserInterface = new AdminCheckingUserInterface(Program.subjectsCollection);
        public override void ShowText()
        {
            if (adminCheckingUserInterface.CheckAccess())
            {
                Console.WriteLine(Text);
            }
            else
            {
                Console.WriteLine("Доступ відхилено, увійдіть як адмін");
            }
        }
        public override void OpenURLs()
        {
            if (adminCheckingUserInterface.CheckAccess())
            {
                foreach (string url in URLs)
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = url,
                            UseShellExecute = true
                        });
                    }
                    catch
                    {
                        Console.WriteLine("Неможливо відкрити одне з посилань");
                    }
                }
            }
            else
            {
                Console.WriteLine("Доступ відхилено, увійдіть як адмін");
            }
        }
        public override void OpenFiles()
        {
            if (adminCheckingUserInterface.CheckAccess())
            {
                foreach (string filePath in FilesPaths)
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = filePath,
                            UseShellExecute = true
                        });
                    }
                    catch
                    {
                        Console.WriteLine("Неможливо відкрити один з файлів");
                    }
                }
            }
            else
            {
                Console.WriteLine("Доступ відхилено, увійдіть як адмін");
            }
        }

        public override string ShowStatus()
        {
            return "(для адміна)";
        }

        public override void ChangeText(string text)
        {
            if (adminCheckingUserInterface.CheckAccess())
            {
                Text = text;
            }
            else
            {
                Console.WriteLine("Доступ відхилено, увійдіть як адмін");
            }
        }
        public override void ChangeURLs(string urls)
        {
            if (adminCheckingUserInterface.CheckAccess())
            {
                URLs = urls.Split(" ");
            }
            else
            {
                Console.WriteLine("Доступ відхилено, увійдіть як адмін");
            }
        }
        public override void ChangeFilesPaths(string filesPaths)
        {
            if (adminCheckingUserInterface.CheckAccess())
            {
                FilesPaths = filesPaths.Split(" ");
            }
            else
            {
                Console.WriteLine("Доступ відхилено, увійдіть як адмін");
            }
        }
    }
}
