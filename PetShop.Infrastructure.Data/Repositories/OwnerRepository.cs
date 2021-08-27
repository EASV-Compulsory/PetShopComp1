using System.Collections.Generic;
using PetShop.Core.Models;
using PetShop.Domain.IRepositories;

namespace PetShop.Infrastructure.Data.Repositories
{
    public class OwnerRepository: IOwnerRepository
    {
        public IEnumerable<Owner> ReadAll()
        {
            return FakeDb.GetOwners();
        }
    }
}