using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    public class PasswordLengthChecker : IPasswordChecker
    {
        IPasswordChecker nextChecker = new PasswordLetterChecker();
        public bool CheckPassword(string password)
        {
            if (password.Length < 6)
            {
                return false;
            }
            else
            {
                return nextChecker.CheckPassword(password);
            }
        }
    }
    public class PasswordLetterChecker : IPasswordChecker
    {
        IPasswordChecker nextChecker = new PasswordSpecialSymbolChecker();
        char[] smallLetters = ['a', 'b', 'c'];
        char[] bigLetters = ['A', 'B', 'C'];
        bool doesContainSmallLetter = false;
        bool doesContainBigLetter = false;
        public bool CheckPassword(string password)
        {
            foreach (char letter in smallLetters)
            {
                if (password.Contains(letter))
                {
                    doesContainSmallLetter = true;
                }
            }

            foreach (char letter in bigLetters)
            {
                if (password.Contains(letter))
                {
                    doesContainBigLetter = true;
                }
            }

            if (!(doesContainSmallLetter && doesContainSmallLetter))
            {
                return false;
            }
            else
            {
                return nextChecker.CheckPassword(password);
            }
        }
    }
    public class PasswordSpecialSymbolChecker : IPasswordChecker
    {
        char[] specialSymbols = ['_', '.', ',', '%'];
        public bool CheckPassword(string password)
        {
            foreach (char symbol in specialSymbols)
            {
                if (password.Contains(symbol))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
