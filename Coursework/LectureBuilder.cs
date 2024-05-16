using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    public class LectureStateBuilder : ILectureBuilder
    {
        LectureState lecture;
        public void ResetForUnfinishedState()
        {
            lecture = new UnfinishedLectureState();
        }
        public void ResetForFinishedState()
        {
            lecture = new FinishedLectureState();
        }
        public void ResetForAdminState()
        {
            lecture = new AdminLectureState();
        }
        public void SetName(string name)
        {
            lecture.Name = name;
        }
        public void SetText(string text)
        {
            lecture.Text = text;
        }

        public void SetURLs(string url)
        {
            lecture.URLs = url.Split(" ");
        }
        public void SetFilesPaths(string filePath)
        {
            lecture.FilesPaths = filePath.Split(" ");
        }
        public LectureState GetLecture()
        {
            return lecture;
        }
    }
}
