using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    public static class Program
    {
        public static string enteredPassword = "defaultPassword";
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            IUserInterface userInterface = new AdminCheckingUserInterface();

            while (true)
            {
                userInterface.WriteOptionsFirstStage();
                int answer = Convert.ToInt32(Console.ReadLine());

                switch (answer)
                {
                    case 1:
                        break;
                    case 4:

                        break;
                }
            }
        }
    }
}
