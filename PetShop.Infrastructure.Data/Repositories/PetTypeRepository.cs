using System.Collections.Generic;
using PetShop.Core.Models;
using PetShop.Domain.IRepositories;

namespace PetShop.Infrastructure.Data.Repositories
{
    public class PetTypeRepository :IPetTypeRepository
    {
        public IEnumerable<PetType> ReadPetsTypes()
        {
            return new List<PetType>(FakeDb.GetPetTypes());
        }
    }
}