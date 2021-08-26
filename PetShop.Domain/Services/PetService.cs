using System.Collections.Generic;
using System.Linq;
using PetShop.Core.IServices;
using PetShop.Core.Models;
using PetShop.Domain.IRepositories;

namespace PetShop.Domain.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _repository;

        public PetService(IPetRepository repository)
        {
            _repository = repository;
        }

        public List<Pet> GetPets()
        {
            return _repository.ReadPets().ToList();
        }

        public Pet Create(Pet pet)
        {
           return _repository.Create(pet);
        }

        public List<Pet> SearchPetsByType(string query)
        {
            return GetPets().
                FindAll(pet => pet.Type.Name.ToLower().Contains(query.ToLower()));
        }

        public List<Pet> SortPetsByPriceAsc()
        {
            return GetPets().OrderBy(pet => pet.Price).ToList();
        }

        public List<Pet> GetXCheapestPets(int i)
        {
            return GetPets().OrderBy(pet => pet.Price).Take(5).ToList();
        }
    }
}