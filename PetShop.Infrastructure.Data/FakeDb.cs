using System;
using System.Collections.Generic;
using System.Linq;
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
        private static List<Owner> _owners;
        private static int _petId =0;
        private static int _petTypeId =0;
        private static int _ownerId = 0;

        #region CRUD Pet
        public static IEnumerable<Pet> GetPets()
        {
            return new List<Pet>(_pets);
        }
        
        public static Pet CreatePet(Pet pet)
        {
            pet.Id = ++_petId;
            _pets.Add(pet);
            return pet;
        }
        
        public static IEnumerable<Owner> GetOwners()
        {
            return new List<Owner>(_owners);
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
        
        public static IEnumerable<PetType> GetPetTypes()
        {
            return new List<PetType>(_petTypes);
        }


        #endregion

        #region Init data
        public static void InitData()
        {
            InitOwners();
            InitPetTypes();
            InitPets();
           

        }

      

        #endregion

        #region init pets
        private static void InitPets()
        {
            var lolka = new Pet
            {
                Id = ++_petId,
                BirthDate = new DateTime(2020, 05, 06),
                Type = _petTypes.ElementAt(0),
                Color = "Black",
                Name = "Lolka", Price = 600.0,
                SoldDate = new DateTime(2022, 06, 05),
                PreviousOwner = _owners.ElementAt(0)
            };
            
            var nikita = new Pet
            {
                Id = ++_petId,
                BirthDate = new DateTime(2014,  03 ,  27),
                Type = _petTypes.ElementAt(1),
                Color = "Black",
                Name = "Nikita", Price = 2000.0,
                SoldDate = new DateTime(2015 ,  05 , 23),
                PreviousOwner = _owners.ElementAt(1)
            };
            
            var bPet = new Pet
            {
                Id = ++_petId,
                BirthDate = new DateTime(2014,  03 ,  27),
                Type = _petTypes.ElementAt(2),
                Color = "grey",
                Name = "Borys", Price = 200000.0,
                SoldDate = new DateTime(2017 ,  05 , 23),
                PreviousOwner = _owners.ElementAt(0)
            };
            
            var cPet = new Pet
            {
                Id = ++_petId,
                BirthDate = new DateTime(2014,  03 ,  27),
                Type = _petTypes.ElementAt(0),
                Color = "green",
                Name = "jack", Price = 400.0,
                SoldDate = new DateTime(2015 ,  05 , 23),
                PreviousOwner = _owners.ElementAt(2)
            };
            
            var dPet = new Pet
            {
                Id = ++_petId,
                BirthDate = new DateTime(2014,  03 ,  27),
                Type = _petTypes.ElementAt(0),
                Color = "white",
                Name = "Nana", Price = 3000.0,
                SoldDate = new DateTime(2015 ,  06 , 23),
                PreviousOwner = _owners.ElementAt(1)
            };
            
            var ePet = new Pet
            {
                Id = ++_petId,
                BirthDate = new DateTime(2014,  03 ,  27),
                Type = _petTypes.ElementAt(2),
                Color = "Black",
                Name = "origin", Price = 3000000.0,
                SoldDate = new DateTime(2015 ,  05 , 23),
                PreviousOwner = _owners.ElementAt(2)
            };
            _pets = new List<Pet>(){lolka, nikita, bPet, cPet, dPet, ePet};
        }
        

        #endregion

        #region init pet types
        private static void InitPetTypes()
        {
            var cats = new PetType
            {
                Id = ++_petTypeId, Name = "Cats"
            };
            var dogs = new PetType
            {
                Id = ++_petTypeId, Name = "Dogs"
            };
            var goats = new PetType
            {
                Id = ++_petTypeId, Name = "Goats"
            };
            _petTypes = new List<PetType>(){cats, dogs, goats};
        }
        
        #endregion

        #region Init owners
        private static void InitOwners()
        {
            var o1 = new Owner
            {
                Address = "Esbjerg",
                Email = "o1@gmail.com",
                FirstName = "Tom",
                Id = ++_ownerId,
                LastName = "co",
                PhoneNumber = "800-900-100"
            };
            var o2 = new Owner
            {
                Address = "Aarhus",
                Email = "o2@gmail.com",
                FirstName = "Tim",
                Id = ++_ownerId,
                LastName = "cowy",
                PhoneNumber = "801-910-100"
            };
            var o3 = new Owner
            {
                Address = "Kolding",
                Email = "o3@gmail.com",
                FirstName = "Ania",
                Id = ++_ownerId,
                LastName = "buda",
                PhoneNumber = "811-911-101"
            };
            _owners = new List<Owner>() {o1, o2, o3};
        }
        

        #endregion
        
    }
}