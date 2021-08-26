using System.Collections.Generic;
using PetShop.Core.Models;

namespace PetShop.Core.IServices
{
    public interface IPetTypeService
    {
        bool CheckIfPetTypeExists(string type);
        List<PetType> GetPetTypes();
        void GetPetTypeByName(string userInput, out PetType petType);
    }
}