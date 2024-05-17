using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    public class UnfinishedLectureStateCreator : LectureCreator
    {
        public override LectureState CreateLecture(string name, string text, string urls, string filesPaths)
        {
            UnfinishedLectureState lecture = new UnfinishedLectureState();
            lecture.Name = name;
            lecture.Text = text;
            lecture.URLs = urls.Split(" ");
            lecture.FilesPaths = filesPaths.Split(" ");
            return lecture;
        }
    }
    public class FinishedLectureStateCreator : LectureCreator
    {
        public override LectureState CreateLecture(string name, string text, string urls, string filesPaths)
        {
            FinishedLectureState lecture = new FinishedLectureState();
            lecture.Name = name;
            lecture.Text = text;
            lecture.URLs = urls.Split(" ");
            lecture.FilesPaths = filesPaths.Split(" ");
            return lecture;
        }
    }
    public class AdminLectureStateCreator : LectureCreator
    {
        public override LectureState CreateLecture(string name, string text, string urls, string filesPaths)
        {
            AdminLectureState lecture = new AdminLectureState();
            lecture.Name = name;
            lecture.Text = text;
            lecture.URLs = urls.Split(" ");
            lecture.FilesPaths = filesPaths.Split(" ");
            return lecture;
        }
    }
}
