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
        Lecture Clone();
    }
    interface ILectureBuilder
    {
        void Reset();
        void SetName(string name);
        void SetText(string text);
        void SetURL(string url);
        void SetFilePath(string filePath);
    }
    interface IUserInterface
    {
        void AddSubject(Subject subject);
        void WriteOptionsFirstStage();
    }
}
