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
        IPasswordChecker nextChecker = new PasswordDigitChecker();
        char[] smallLetters = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 
            'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'а', 'б', 'в', 'г', 'ґ', 'д', 'е', 'є', 'ж', 'з', 'и', 'і', 
            'ї', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ю', 'я'];
        char[] bigLetters = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R',
            'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'А', 'Б', 'В', 'Г', 'Ґ', 'Д', 'Е', 'Є', 'Ж', 'З', 'И', 'І', 'Ї', 'Й',
            'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ю', 'Я'];
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
    public class PasswordDigitChecker : IPasswordChecker
    {
        char[] specialSymbols = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];
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
