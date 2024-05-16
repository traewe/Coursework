using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    interface ICompositeElement
    {
        void ShowWholeInternalStructure();
        void ShowOnlyChildren();
    }
    interface ICloneableLecture
    {
        LectureState Clone();
    }
    interface ILectureBuilder
    {
        void ResetForUnfinishedState();
        void ResetForFinishedState();
        void ResetForAdminState();
        void SetName(string name);
        void SetText(string text);
        void SetURL(string url);
        void SetFilesPaths(string filePath);
        LectureState GetLecture();
    }
    public abstract class UserInterfaceAbstraction
    {
        public SubjectsCollection subjectsCollection;
        public abstract void AddSubject(Subject subject);
        public abstract void RemoveSubject(Subject subject);
        public abstract void WriteOptionsFirstStage();
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
        public void ShowWholeInternalStructure()
        {
            Console.WriteLine(Name);
        }

        public void ShowOnlyChildren() { }

        public LectureState Clone()
        {
            return (LectureState)MemberwiseClone();
        }
    }
    interface ISavingAndLoadingStrategy
    {
        void SaveData();
        void LoadData();
    }
    interface IPasswordChecker
    {
        bool CheckPassword(string password);
    }
}
