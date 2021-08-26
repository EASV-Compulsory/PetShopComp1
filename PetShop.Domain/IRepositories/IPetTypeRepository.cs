using System.Collections.Generic;
using PetShop.Core.Models;

namespace PetShop.Domain.IRepositories
{
    public interface IPetTypeRepository
    {
        IEnumerable<PetType> ReadPetsTypes();
    }
}