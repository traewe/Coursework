using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Coursework
{
    public class SubjectsCollection : ICompositeElement
    {
        List<Subject> subjects = new List<Subject>();

        public void Add(Subject subject)
        {
            subjects.Add(subject);
        }

        public void Remove(Subject subject)
        {
            subjects.Remove(subject);
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
                Console.Write($"{i + 1} {subjects[i].Name}");
            }
        }
    }

    public class Subject : ICompositeElement
    {
        private string name;

        protected List<Chapter> chapters = new List<Chapter>();

        public string Name
        {
            get { return name; }
        }

        public Subject(string name)
        {
            this.name = name;
        }

        public void Add(Chapter chapter)
        {
            chapters.Add(chapter);
        }

        public void Remove(Chapter chapter)
        {
            chapters.Remove(chapter);
        }

        public void ShowWholeInternalStructure()
        {
            Console.WriteLine(name);
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
                Console.Write($"{i + 1} {chapters[i].Name}");
            }
        }
    }

    public class Chapter : ICompositeElement
    {
        string name;
        protected List<LectureState> lectures = new List<LectureState>();

        public string Name
        {
            get { return name; }
        }

        public Chapter(string name)
        {
            this.name = name;
        }

        public void Add(LectureState lecture)
        {
            lectures.Add(lecture);
        }

        public void Remove(LectureState lecture)
        {
            lectures.Remove(lecture);
        }

        public void ShowWholeInternalStructure()
        {
            Console.WriteLine(name);
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
                Console.Write($"{i + 1} {lectures[i].Name}");
            }
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
        AdminCheckingUserInterface adminCheckingUserInterface = new AdminCheckingUserInterface();
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
