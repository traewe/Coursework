using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    public class LectureBuilder : ILectureBuilder
    {
        Lecture lecture;
        public void Reset()
        {
            lecture = new Lecture();
        }
        public void SetName(string name)
        {
            lecture.Name = name;
        }
        public void SetText(string text)
        {
            lecture.Text = text;
        }

        public void SetURL(string url)
        {
            lecture.URL = url;
        }
        public void SetFilePath(string filePath)
        {
            lecture.FilePath = filePath;
        }
        public Lecture GetLecture()
        {
            return lecture;
        }
    }
}
