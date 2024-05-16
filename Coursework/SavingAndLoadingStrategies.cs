using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    public class PasswordSavingAndLoading : ISavingAndLoadingStrategy
    {
        public void SaveData()
        {
            if (File.Exists("LectureBase.txt"))
            {
                string[] lines = File.ReadAllLines("LectureBase.txt");
                lines[0] = Program.rightPassword;
                File.WriteAllLines("LectureBase.txt", lines);
            }
            else
            {
                File.AppendAllText("LectureBase.txt", Program.rightPassword);
            }
        }
        public void LoadData()
        {
            Program.rightPassword = File.ReadAllLines("LectureBase.txt")[0];
        }
    }
    public class SubjectsSavingAndLoading : ISavingAndLoadingStrategy
    {
        public void SaveData()
        {
            string[] lines = File.ReadAllLines("LectureBase.txt");

            if (lines.Length > 1)
            {
                lines[1] = Program.subjectsCollection.GetStringForSaving();
                File.WriteAllLines("LectureBase.txt", lines);
            }
            else
            {
                StreamWriter sw = File.AppendText("LectureBase.txt");
                sw.WriteLine("\n" + Program.subjectsCollection.GetStringForSaving());
                sw.Close();
            }
        }
        public void LoadData()
        {
            if (File.ReadAllLines("LectureBase.txt").Length > 1)
            {
                foreach (string str in File.ReadAllLines("LectureBase.txt")[1].Split(";"))
                {
                    Program.enteredPassword = Program.rightPassword;
                    Program.userInterface.AddSubject(new Subject(str));
                    Program.enteredPassword = string.Empty;
                }
            }
        }
    }
    public class ChaptersSavingAndLoading : ISavingAndLoadingStrategy
    {
        public void SaveData()
        {
            string result = "";

            for (int i = 0; i < Program.subjectsCollection.Count(); i++)
            {
                result += Program.subjectsCollection[i].GetStringForSaving();
            }

            if (result[^1] == ';')
            {
                result = result.Substring(0, result.Length - 1);
            }

            string[] lines = File.ReadAllLines("LectureBase.txt");

            if (lines.Length > 2)
            {
                lines[2] = result;
                File.WriteAllLines("LectureBase.txt", lines);
            }
            else
            {
                StreamWriter sw = File.AppendText("LectureBase.txt");
                sw.WriteLine(result);
                sw.Close();
            }
        }
        public void LoadData()
        {
            if (File.ReadAllLines("LectureBase.txt").Length > 2)
            {
                foreach (string str in File.ReadAllLines("LectureBase.txt")[2].Split(";"))
                {
                    Program.enteredPassword = Program.rightPassword;
                    Program.subjectsCollection[Convert.ToInt32(str.Split("+")[1])].Add(new Chapter(str.Split("+")[0]));
                    Program.enteredPassword = string.Empty;
                }
            }
        }
    }
    public class LecturesSavingAndLoading : ISavingAndLoadingStrategy
    {
        public void SaveData()
        {

        }
        public void LoadData()
        {

        }
    }
}