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
    }
}