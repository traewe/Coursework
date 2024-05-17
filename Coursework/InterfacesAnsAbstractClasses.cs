using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    public interface ICompositeElement
    {
        void ShowWholeInternalStructure();
        void ShowOnlyChildren();
        int Count();
        bool ContainsName(string name);
        void Clear();
        void Add(ICompositeElement component);
        void Remove(ICompositeElement component);
        void Sort();
        string GetStringForSaving();
        int IndexOf(ICompositeElement element);
        ICompositeElement GetChildByName(string name);
    }
    interface ICloneableLecture
    {
        LectureState Clone();
    }
    public abstract class LectureCreator
    {
        public abstract LectureState CreateLecture(string name, string text, string urls, string filesPaths);
    }
    public abstract class UserInterfaceAbstraction
    {
        public SubjectsCollection subjectsCollection;
        public abstract void AddSubject(Subject subject);
        public abstract void RemoveSubject(Subject subject);
        public abstract void AddChapter(Chapter chapter);
        public abstract void RemoveChapter(Chapter chapter);
        public abstract void RemoveLecture(LectureState lecture);
        public abstract void WriteOptionsFirstStage();
        public abstract void WriteOptionsSecondStage();
        public abstract void WriteOptionsThirdStage();
        public abstract void WriteOptionsFourthStage();
    }
    public abstract class LectureState : ICloneableLecture, ICompositeElement
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string[] URLs { get; set; }
        public string[] FilesPaths { get; set; }
        public abstract void ShowText();
        public abstract void OpenURLs();
        public abstract void OpenFiles();
        public abstract void ChangeText(string text);
        public abstract void ChangeURLs(string urls);
        public abstract void ChangeFilesPaths(string filesPaths);
        public abstract string ShowStatus();
        public void ShowWholeInternalStructure()
        {
            Console.WriteLine($"{Name} {ShowStatus()}");
        }

        public void ShowOnlyChildren() { }
        public int Count() {  return 0; }
        public bool ContainsName(string name) { return false; }
        public void Clear() { }
        public void Add(ICompositeElement component) { }
        public void Remove(ICompositeElement component) { }
        public void Sort() { }
        public string GetStringForSaving() { return ""; }
        public int IndexOf(ICompositeElement component) { return 0; }
        public ICompositeElement GetChildByName(string name) { return this; }
        public LectureState Clone()
        {
            return (LectureState)MemberwiseClone();
        }
    }
    public interface ISavingAndLoadingStrategy
    {
        void SaveData();
        void LoadData();
    }
    public interface IPasswordChecker
    {
        bool CheckPassword(string password);
    }
}
