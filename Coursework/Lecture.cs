using System;
using System.Collections.Generic;

namespace Coursework
{
    public interface ICompositeElement
    {
        void ShowInternalStructure();
    }
    interface ICloneableLecture
    {
        Lecture Clone();
    }
    public class SubjectsCollection : ICompositeElement
    {
        protected List<Subject> subjects = new List<Subject>();

        public void Add(ICompositeElement component)
        {
            if (component is Subject)
                subjects.Add((Subject)component);
        }

        public void Remove(ICompositeElement component)
        {
            if (component is Subject)
                subjects.Remove((Subject)component);
        }

        public void ShowInternalStructure()
        {
            for (int i = 0; i < subjects.Count; i++)
            {
                Console.Write($"{i + 1} ");
                subjects[i].ShowInternalStructure();
            }
        }
    }

    public class Subject : ICompositeElement
    {
        private string name;
        public string Name
        {
            get { return name; }
        }

        protected List<Chapter> chapters = new List<Chapter>();

        public Subject(string name)
        {
            this.name = name;
        }

        public void Add(ICompositeElement component)
        {
            if (component is Chapter)
                chapters.Add((Chapter)component);
        }

        public void Remove(ICompositeElement component)
        {
            if (component is Chapter)
                chapters.Remove((Chapter)component);
        }

        public void ShowInternalStructure()
        {
            Console.WriteLine(name);
            for (int i = 0; i < chapters.Count; i++)
            {
                Console.Write($" {i + 1} ");
                chapters[i].ShowInternalStructure();
            }
        }
    }

    public class Chapter : ICompositeElement
    {
        private string name;
        public string Name
        {
            get { return name; }
        }

        protected List<Lecture> lectures = new List<Lecture>();

        public Chapter(string name)
        {
            this.name = name;
        }

        public void Add(ICompositeElement component)
        {
            if (component is Lecture)
                lectures.Add((Lecture)component);
        }

        public void Remove(ICompositeElement component)
        {
            if (component is Lecture)
                lectures.Remove((Lecture)component);
        }

        public void ShowInternalStructure()
        {
            Console.WriteLine(name);
            for (int i = 0; i < lectures.Count; i++)
            {
                Console.Write($"  {i + 1} ");
                lectures[i].ShowInternalStructure();
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

        public void ShowInternalStructure()
        {
            Console.WriteLine(Name);
        }
        public Lecture Clone()
        {
            return (Lecture)MemberwiseClone();
        }
    }
}
