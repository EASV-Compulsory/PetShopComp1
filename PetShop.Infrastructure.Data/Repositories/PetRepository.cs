using System.Collections.Generic;
using PetShop.Core.Models;
using PetShop.Domain.IRepositories;

namespace PetShop.Infrastructure.Data.Repositories
{
    public class PetRepository :IPetRepository
    {
        private readonly FakeDb _fakeDb;
        public PetRepository()
        {
            _fakeDb = new FakeDb();
        }

        public IEnumerable<Pet> ReadPets()
        {
            return new List<Pet>(_fakeDb.GetPets());
        }
    }
}