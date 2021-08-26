﻿using System.Collections.Generic;
using PetShop.Core.Models;
using PetShop.Domain.IRepositories;

namespace PetShop.Infrastructure.Data.Repositories
{
    public class PetRepository :IPetRepository
    {
        public IEnumerable<Pet> ReadPets()
        {
            return new List<Pet>(FakeDb.GetPets());
        }
    }
}