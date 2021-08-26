using System;
using PetShop.Core.Models;

namespace PetShop.UI
{
    public interface IUtils
    {
        //for getting option 
        void ReadIntegerFromString(out int selectedNumber, string messageToUser);
        //Color & Name
        void GetMinimalStringInput(out string userInput, int minLength, string repeatMessage);
        // birthdate & selldate
        void GetDateInput(out DateTime date);
        //Price
        void ReadDoubleFromString(out double price, string messageToUser);
        //Options
        string ConcatPossibleOptions();
        

    }
}