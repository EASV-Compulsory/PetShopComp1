using System.Collections.Generic;
using PetShop.Core.Models;

namespace PetShop.Core.IServices
{
    public interface IPetService
    {
        List<Pet> GetPets();
        Pet Create(Pet pet);
        List<Pet> SearchPetsByType(string userInput);
        
        List<Pet> SortPetsByPriceAsc();
        List<Pet> GetXCheapestPets(int i);
    }
}