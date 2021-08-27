using System.Collections.Generic;
using System.Linq;
using PetShop.Core.IServices;
using PetShop.Core.Models;
using PetShop.Domain.IRepositories;

namespace PetShop.Domain.Services
{
    public class OwnerService :IOwnerService
    {
        private readonly IOwnerRepository _repository;

        public OwnerService(IOwnerRepository repository)
        {
            _repository = repository;
        }


        public List<Owner> GetOwners()
        {
            return _repository.ReadAll().ToList();
        }

        public Owner GetOwnerById(int id)
        {
            return GetOwners().Find(owner => owner.Id == id);
        }
    }
}