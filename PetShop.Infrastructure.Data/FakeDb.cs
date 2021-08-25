﻿using System;
using System.Collections.Generic;
using PetShop.Core.Models;

namespace PetShop.Infrastructure.Data
{
    /// <summary>
    /// Class represents fake SQL database
    /// </summary>
    public class FakeDb
    {
        private static List<Pet> _pets;
        private static List<PetType> _petTypes;
        private static int _petId =1;
        private static int _petTypeId =1;

        public FakeDb()
        {
            InitData();
        }

        #region get pets and petTypes

        public IList<Pet> GetPets()
        {
            return _pets;
        }
        
        public IList<PetType> GetPetTypes()
        {
            return _petTypes;
            //or new List ect
        }


        #endregion

        #region Init data
        private void InitData()
        {
            _petTypes = new List<PetType>();
            _pets = new List<Pet>();

            var cats = new PetType
            {
                Id = _petTypeId++, Name = "Cats"
            };
            var dogs = new PetType
            {
                Id = _petTypeId++, Name = "Dogs"
            };
            var birds = new PetType
            {
                Id = _petTypeId++, Name = "Goats"
            };
            _petTypes.Add(cats);
            _petTypes.Add(dogs);
            _petTypes.Add(birds);

            var lolka = new Pet
            {
                Id = _petId++,
                BirthDate = new DateTime(27 / 05 / 2009),
                Type = dogs,
                Color = "Black",
                Name = "Lolka", Price = 600.0,
                SoldDate = new DateTime(02 / 02 / 2010)
            };
            
            var nikita = new Pet
            {
                Id = _petId++,
                BirthDate = new DateTime(27 / 03 / 2014),
                Type = dogs,
                Color = "Black",
                Name = "Nikita", Price = 2000.0,
                SoldDate = new DateTime(27 / 05 / 2014)
            };
            _pets.Add(lolka);
            _pets.Add(nikita);

        }
        #endregion

    }
}