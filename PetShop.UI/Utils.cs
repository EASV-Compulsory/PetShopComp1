using System;
using PetShop.Core.Models;

namespace PetShop.UI
{
    public class Utils :IUtils
    {
        public void ReadIntegerFromString(out int selectedNumber, string messageToUser)
        {
            var consoleInput = Console.ReadLine();
            int intValue; 
            while (!int.TryParse(consoleInput, out intValue))
            {
                Console.WriteLine(messageToUser);
                consoleInput = Console.ReadLine();
            }
            selectedNumber = intValue;
        }

        public void GetMinimalStringInput(out string userInput, int minLength, string repeatMessage)
        {
            var inp = Console.ReadLine();
            while (inp.Length < minLength)
            {
                Console.WriteLine(repeatMessage);
                inp = Console.ReadLine();
            }
            userInput = inp;
        }

        public void GetDateInput(out DateTime relaseDate)
        {
            var dateTime = Console.ReadLine();
            DateTime result;
            while (!DateTime.TryParse(dateTime, out result))
            {
                Console.WriteLine(StringConstants.IncorrectDateFormat);
                dateTime = Console.ReadLine();
            }
            relaseDate = result;
        }

        public void ReadDoubleFromString(out double price, string messageToUser)
        {
            var consoleInput = Console.ReadLine();
            double doubleValue; 
            while (!double.TryParse(consoleInput, out doubleValue))
            {
                Console.WriteLine(messageToUser);
                consoleInput = Console.ReadLine();
            }
            price = doubleValue;
        }

        public string ConcatPossibleOptions()
        {
            var output = "";
            var enums = Enum.GetValues(typeof(Options));
            foreach (var eEnum in enums)
            {
                var line = $"{(int) eEnum}. {eEnum} \n";
                output = string.Concat(output, line);
            }
            return output;
        }

       
    }
}