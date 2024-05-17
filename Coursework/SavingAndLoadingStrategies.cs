using System;
using System.IO;

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
                
            }
        }
    }

    public class SubjectsSavingAndLoading : ISavingAndLoadingStrategy
    {
        public void SaveData()
        {
            try
            {
                if (File.Exists("LectureBase.txt"))
                {
                    string[] lines = File.ReadAllLines("LectureBase.txt");

                    if (lines.Length > 1)
                    {
                        lines[1] = Program.subjectsCollection.GetStringForSaving();
                        File.WriteAllLines("LectureBase.txt", lines);
                    }
                    else
                    {
                        using (StreamWriter sw = File.AppendText("LectureBase.txt"))
                        {
                            sw.WriteLine("\n" + Program.subjectsCollection.GetStringForSaving());
                        }
                    }
                }
                else
                {
                    File.AppendAllText("LectureBase.txt", "");
                    SaveData();
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public void LoadData()
        {
            try
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
            catch (Exception ex)
            {
                
            }
        }
    }

    public class ChaptersSavingAndLoading : ISavingAndLoadingStrategy
    {
        public void SaveData()
        {
            try
            {
                string result = "";

                for (int i = 0; i < Program.subjectsCollection.Count(); i++)
                {
                    result += Program.subjectsCollection[i].GetStringForSaving();
                }

                if (result.Length > 0 && result[^1] == ';')
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
                    using (StreamWriter sw = File.AppendText("LectureBase.txt"))
                    {
                        sw.WriteLine(result);
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public void LoadData()
        {
            try
            {
                if (File.ReadAllLines("LectureBase.txt").Length > 2)
                {
                    foreach (string str in File.ReadAllLines("LectureBase.txt")[2].Split(";"))
                    {
                        Program.enteredPassword = Program.rightPassword;
                        Program.subjectsCollection[Convert.ToInt32(str.Split("|")[1])].Add(new Chapter(str.Split("|")[0]));
                        Program.enteredPassword = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }

    public class LecturesSavingAndLoading : ISavingAndLoadingStrategy
    {
        public void SaveData()
        {
            try
            {
                string result = "";

                for (int i = 0; i < Program.subjectsCollection.Count(); i++)
                {
                    for (int j = 0; j < Program.subjectsCollection[i].Count(); j++)
                    {
                        result += Program.subjectsCollection[i][j].GetStringForSaving(Program.subjectsCollection[i]);
                    }
                }

                if (result.Length > 0 && result[^1] == ';')
                {
                    result = result.Substring(0, result.Length - 1);
                }

                string[] lines = File.ReadAllLines("LectureBase.txt");

                if (lines.Length > 3)
                {
                    lines[3] = result;
                    File.WriteAllLines("LectureBase.txt", lines);
                }
                else
                {
                    using (StreamWriter sw = File.AppendText("LectureBase.txt"))
                    {
                        sw.WriteLine(result);
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public void LoadData()
        {
            try
            {
                if (File.ReadAllLines("LectureBase.txt").Length > 3)
                {
                    foreach (string str in File.ReadAllLines("LectureBase.txt")[3].Split(";"))
                    {
                        Program.enteredPassword = Program.rightPassword;
                        LectureCreator unfinishedLectureStateCreator = new UnfinishedLectureStateCreator();
                        LectureCreator finishedLectureStateCreator = new FinishedLectureStateCreator();
                        LectureCreator adminLectureStateCreator = new AdminLectureStateCreator();
                        LectureState lecture;

                        string lectureName = str.Split("|")[0];
                        string lectureText = str.Split("|")[1];
                        string lectureURLs = str.Split("|")[2];
                        string lectureFilesPaths = str.Split("|")[3];
                        int subjectIndex = Convert.ToInt32(str.Split("|")[5]);
                        int chapterIndex = Convert.ToInt32(str.Split("|")[4]);

                        if (str.Split("|")[6] == "un")
                        {
                            Program.subjectsCollection[subjectIndex][chapterIndex].Add(unfinishedLectureStateCreator.CreateLecture(lectureName,
                                lectureText, lectureURLs, lectureFilesPaths));
                        }
                        else if (str.Split("|")[6] == "fi")
                        {
                            Program.subjectsCollection[subjectIndex][chapterIndex].Add(finishedLectureStateCreator.CreateLecture(lectureName,
                                lectureText, lectureURLs, lectureFilesPaths));
                        }
                        else
                        {
                            Program.subjectsCollection[subjectIndex][chapterIndex].Add(adminLectureStateCreator.CreateLecture(lectureName,
                                lectureText, lectureURLs, lectureFilesPaths));
                        }

                        Program.enteredPassword = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
