using System;
using System.Collections.Generic;
using PetShop.Core.Models;

namespace PetShop.Infrastructure.Data
{
    /// <summary>
    /// Class represents fake SQL database
    /// </summary>
    public static class FakeDb
    {
        private static List<Pet> _pets;
        private static List<PetType> _petTypes;
        private static int _petId =0;
        private static int _petTypeId =0;

        #region CRUD Pet
        public static IList<Pet> GetPets()
        {
            return new List<Pet>(_pets);
        }
        
        public static Pet CreatePet(Pet pet)
        {
            pet.Id = ++_petId;
            _pets.Add(pet);
            return pet;
        }

        /// <summary>
        /// delete operation is handed here not in Repository.
        /// We cant access private list from other place
        /// (getter returns only a copy)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeletePet(int id)
        {
            var pet = _pets.Find(p => p.Id == id);
            return _pets.Remove(pet);
        }
        

        #endregion

        #region CRUD petTypes
        
        public static IList<PetType> GetPetTypes()
        {
            return new List<PetType>(_petTypes);
        }


        #endregion

        #region Init data
        public static void InitData()
        {
            _petTypes = new List<PetType>();
            _pets = new List<Pet>();

            var cats = new PetType
            {
                Id = ++_petTypeId, Name = "Cats"
            };
            var dogs = new PetType
            {
                Id = ++_petTypeId, Name = "Dogs"
            };
            var birds = new PetType
            {
                Id = ++_petTypeId, Name = "Goats"
            };
            _petTypes.Add(cats);
            _petTypes.Add(dogs);
            _petTypes.Add(birds);

            var lolka = new Pet
            {
                Id = ++_petId,
                BirthDate = new DateTime(2020, 05, 06),
                Type = dogs,
                Color = "Black",
                Name = "Lolka", Price = 600.0,
                SoldDate = new DateTime(2022, 06, 05)
            };
            
            var nikita = new Pet
            {
                Id = ++_petId,
                BirthDate = new DateTime(2014,  03 ,  27),
                Type = dogs,
                Color = "Black",
                Name = "Nikita", Price = 2000.0,
                SoldDate = new DateTime(2015 ,  05 , 23)
            };
            
            var bPet = new Pet
            {
                Id = ++_petId,
                BirthDate = new DateTime(2014,  03 ,  27),
                Type = dogs,
                Color = "grey",
                Name = "Borys", Price = 200000.0,
                SoldDate = new DateTime(2017 ,  05 , 23)
            };
            
            var cPet = new Pet
            {
                Id = ++_petId,
                BirthDate = new DateTime(2014,  03 ,  27),
                Type = dogs,
                Color = "green",
                Name = "jack", Price = 400.0,
                SoldDate = new DateTime(2015 ,  05 , 23)
            };
            
            var dPet = new Pet
            {
                Id = ++_petId,
                BirthDate = new DateTime(2014,  03 ,  27),
                Type = dogs,
                Color = "white",
                Name = "Nana", Price = 3000.0,
                SoldDate = new DateTime(2015 ,  06 , 23)
            };
            
            var ePet = new Pet
            {
                Id = ++_petId,
                BirthDate = new DateTime(2014,  03 ,  27),
                Type = dogs,
                Color = "Black",
                Name = "origin", Price = 3000000.0,
                SoldDate = new DateTime(2015 ,  05 , 23)
            };
            _pets.Add(lolka);
            _pets.Add(nikita);
            _pets.Add(bPet);
            _pets.Add(cPet);
            _pets.Add(dPet);
            _pets.Add(ePet);

        }
        #endregion

    }
}