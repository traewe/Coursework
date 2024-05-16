using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    public class PasswordSavingAndLoading : ISavingAndLoadingStrategy
    {
        public void SaveData()
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при додаванні даних: {ex.Message}");
            }
        }
        public void LoadData()
        {
            try
            {
                Program.rightPassword = File.ReadAllLines("LectureBase.txt")[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при читанні даних: {ex.Message}");
            }
        }
    }
    public class SubjectsSavingAndLoading : ISavingAndLoadingStrategy
    {
        public void SaveData()
        {
            try 
            { 
                File.AppendText(Program.subjectsCollection.GetStringForSaving());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при додаванні даних: {ex.Message}");
            }
        }
        public void LoadData()
        {
            try
            {
                foreach (string str in File.ReadAllLines("LectureBase.txt")[1].Split(";"))
                {
                    Program.userInterface.AddSubject(new Subject(str));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при читанні даних: {ex.Message}");
            }
        }
    }
    public class ChaptersSavingAndLoading : ISavingAndLoadingStrategy
    {
        public void SaveData()
        {

        }
        public void LoadData()
        {

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