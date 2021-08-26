using System.Collections.Generic;
using PetShop.Core.Models;

namespace PetShop.Domain.IRepositories
{
    public interface IPetRepository
    {
        IEnumerable<Pet> ReadPets();
        Pet Create(Pet pet);
    }
}