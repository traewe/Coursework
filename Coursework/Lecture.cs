using System;
using System.Collections.Generic;

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
        protected List<Lecture> lectures = new List<Lecture>();

        public string Name
        {
            get { return name; }
        }

        public Chapter(string name)
        {
            this.name = name;
        }

        public void Add(Lecture lecture)
        {
            lectures.Add(lecture);
        }

        public void Remove(Lecture lecture)
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

    public class Lecture : ICompositeElement, ICloneableLecture
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string URL { get; set; }
        public string FilePath { get; set; }
        public Lecture() { }
        public Lecture(string name)
        {
            Name = name;
        }

        public void ShowWholeInternalStructure()
        {
            Console.WriteLine(Name);
        }
        
        public void ShowOnlyChildren() { }

        public Lecture Clone()
        {
            return (Lecture)MemberwiseClone();
        }
    }
}
