using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
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
                Console.Write($"{i + 1} ");
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

        public void ShowWholeInternalStructure()
        {
            Console.WriteLine(Name);
            for (int i = 0; i < chapters.Count; i++)
            {
                Console.Write($" {i + 1} ");
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
            Console.WriteLine(Name);
            for (int i = 0; i < lectures.Count; i++)
            {
                Console.Write($"  {i + 1} ");
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
            string URLs = "";
            string filesPaths = "";


            for (int i = 0; i < lectures.Count(); i++)
            {
                foreach (string URL in lectures[i].URLs)
                {
                    URLs += URL + " ";
                }

                foreach (string filesPath in lectures[i].FilesPaths)
                {
                    filesPaths += filesPath + " ";
                }

                result += $"{lectures[i].Name}\\{lectures[i].Text}\\{URLs}\\{filesPaths}\\{subject.IndexOf(this)}\\{Program.subjectsCollection.IndexOf(subject)};";
            }

            return string.Empty;
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
                    Process.Start(url);
                }
                catch
                {
                    Console.WriteLine("One of URLs can't be open, try to change it");
                }
            }
        }
        public override void OpenFiles()
        {
            foreach (string filePath in FilesPaths)
            {
                try
                {
                    Process.Start(filePath);
                }
                catch
                {
                    Console.WriteLine("One of files' paths can't be open, try to change it");
                }
            }
        }
        public override void ChangeText(string text)
        {
            Text = text;
        }
        public override void ChangeURLs(string urls)
        {
            URLs = urls.Split(" ");
        }
        public override void ChangeFilesPaths(string filesPaths)
        {
            FilesPaths = filesPaths.Split(" ");
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
                    Process.Start(url);
                }
                catch
                {
                    Console.WriteLine("One of URLs can't be open, try to change it");
                }
            }
        }
        public override void OpenFiles()
        {
            foreach (string filePath in FilesPaths)
            {
                try
                {
                    Process.Start(filePath);
                }
                catch
                {
                    Console.WriteLine("One of files' paths can't be open, try to change it");
                }
            }
        }
        public override void ChangeText(string text)
        {
            throw new Exception("You can't change finished lecture");
        }
        public override void ChangeURLs(string urls)
        {
            throw new Exception("You can't change finished lecture");
        }
        public override void ChangeFilesPaths(string filesPaths)
        {
            throw new Exception("You can't change finished lecture");
        }
    }
    public class AdminLectureState : LectureState
    {
        AdminCheckingUserInterface adminCheckingUserInterface = new AdminCheckingUserInterface(Program.subjectsCollection);
        public override void ShowText()
        {
            if (!adminCheckingUserInterface.CheckAccess())
            {
                return;
            }

            Console.WriteLine(Text);
        }
        public override void OpenURLs()
        {
            if (!adminCheckingUserInterface.CheckAccess())
            {
                return;
            }

            foreach (string url in URLs)
            {
                try
                {
                    Process.Start(url);
                }
                catch
                {
                    Console.WriteLine("One of URLs can't be open, try to change it");
                }
            }
        }
        public override void OpenFiles()
        {
            if (!adminCheckingUserInterface.CheckAccess())
            {
                return;
            }

            foreach (string filePath in FilesPaths)
            {
                try
                {
                    Process.Start(filePath);
                }
                catch
                {
                    Console.WriteLine("One of files' paths can't be open, try to change it");
                }
            }
        }
        public override void ChangeText(string text)
        {
            if (!adminCheckingUserInterface.CheckAccess())
            {
                return;
            }

            Text = text;
        }
        public override void ChangeURLs(string urls)
        {
            if (!adminCheckingUserInterface.CheckAccess())
            {
                return;
            }

            URLs = urls.Split(" ");
        }
        public override void ChangeFilesPaths(string filesPaths)
        {
            if (!adminCheckingUserInterface.CheckAccess())
            {
                return;
            }

            FilesPaths = filesPaths.Split(" ");
        }
    }
}
