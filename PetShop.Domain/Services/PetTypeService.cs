using System.Collections.Generic;
using System.Linq;
using PetShop.Core.IServices;
using PetShop.Core.Models;
using PetShop.Domain.IRepositories;

namespace PetShop.Domain.Services
{
    public class PetTypeService: IPetTypeService
    {
        private readonly IPetTypeRepository _repository;

        public PetTypeService(IPetTypeRepository repository)
        {
            _repository = repository;
        }

        public bool CheckIfPetTypeExists(string type)
        {
            return _repository.ReadPetsTypes().Any(petType => petType.Name.ToLower().Equals(type.ToLower()));
        }

        public List<PetType> GetPetTypes()
        {
            return _repository.ReadPetsTypes().ToList();
        }

        public void GetPetTypeByName(string userInput, out PetType petType)
        {
            petType = _repository.ReadPetsTypes().
                ToList().Find(p => p.Name.ToLower().Equals(userInput.ToLower()));
        }
    }
}