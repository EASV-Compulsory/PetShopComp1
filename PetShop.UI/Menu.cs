using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using PetShop.Core.IServices;
using PetShop.Core.Models;

namespace PetShop.UI
{
    public class Menu : IMenu
    {
        private readonly IUtils _utils;
        private readonly IPetService _petService;
        private readonly IPetTypeService _petTypeService;
        private readonly IOwnerService _ownerService;
        

        public Menu(IUtils utils, IPetService petService, IPetTypeService petTypeService, 
            IOwnerService ownerService)
        {
            _utils = utils;
            _petService = petService;
            _petTypeService = petTypeService;
            _ownerService = ownerService;
        }

        #region SWITCH for CRUD operations
        
        /// <summary>
        /// We could do better to follow open closed principle
        /// later we can rebuild it to factory and take advantage of polymorphism. 
        /// </summary>
        public void StartUi()
        {
            ShowGreetings();
            ShowMainMenu();
            var selection = GetOptionFromString();
            while (selection != Options.Exit)
            {
                switch (selection)
                {
                    case Options.CreatePet:
                    {
                        CreatePet();
                        break;
                    }
                    case Options.ShowAllPets:
                    {
                        ShowAllPets();
                        break;
                    }
                    case Options.SearchPetsByType:
                    {
                        SearchPetsByType();
                        break;
                    }
                    case Options.SortPetsByPrice:
                    {
                        SortPetsByPrice();
                        break;
                    }
                    case Options.GetFiveCheapestPets:
                    {
                        GiveFiveCheapestPets();
                        break;
                    }
                    case Options.DeletePet:
                    {
                        DeletePet();
                        break;
                    }
                    case Options.UpdatePet: 
                    {
                        UpdatePet();
                        break;
                    }
                    
                        
                }
                ShowMainMenu();
                selection = GetOptionFromString();
            }
            Print("Bye bye");
        }

        #endregion
      
        
        private static void Print(string value)
        {
            Console.WriteLine(value);
        }
        
        private static void ShowGreetings()
        {
            Console.WriteLine(StringConstants.WelcomeGreetings);
        }


        #region Get Option from string
        private Options GetOptionFromString()
        {
            _utils.ReadIntegerFromString( out var selection,StringConstants.OnlyNumbersAccepted );
            while (!Enum.IsDefined(typeof(Options), selection))
            {  
                Console.Clear();
                Print(StringConstants.NumberFromWrongRange);
                ShowMainMenu();
                _utils.ReadIntegerFromString( out selection,StringConstants.OnlyNumbersAccepted );
            }
            return (Options)selection;
        }

        #endregion
        

        private void ShowMainMenu()
        {
            Print(StringConstants.Line);
            Console.WriteLine(StringConstants.PossibleOperations +"\n" + _utils.ConcatPossibleOptions());
            Print(StringConstants.Line);
        }

        #region delete pet
        private void DeletePet()
        {
            Print("Please Insert id of the pet you wanna delete: ");
            _utils.ReadIntegerFromString(out var id, StringConstants.OnlyNumbersAccepted);
            //check if id even exists
            while (!_petService.CheckIfPetExistsById(id))
            {
                Print("Please Insert existing id of the pet you wanna delete: ");
                _utils.ReadIntegerFromString(out  id, StringConstants.OnlyNumbersAccepted);
            }

            var result = _petService.Delete(id);
            Console.WriteLine(result ? $"Pet with id: {id} was deleted" : $"failed. Pet with id {id} wasn't deleted");
        }
        

        #endregion

        #region give five cheapest pets
        private void GiveFiveCheapestPets()
        {
            var fiveCheapest = _petService.GetXCheapestPets(5);
            Print("five cheapest pets: ");
            foreach (var pet in fiveCheapest)
            {
                PrintPet(pet, "Pet: ");
            }
        }
        

        #endregion

        #region sort pets by price in ascrending order
        private void SortPetsByPrice()
        {
            var sortedPets = _petService.SortPetsByPriceAsc();
            Print("pets sorted by price in ascending order");
            foreach (var pet in sortedPets)
            {
                PrintPet(pet, "Pet: ");
            }

        }
        

        #endregion

        #region search pets by type
        private void SearchPetsByType()
        {
            ShowAvailableTypes("Please insert query. Below are available options");
            _utils.GetMinimalStringInput(out var query, 1, StringConstants.ToShort);
            while (!_petTypeService.CheckIfPetTypeExists(query))
            {
                ShowAvailableTypes("Please insert existing type. Below are available options");
                _utils.GetMinimalStringInput(out  query, 1, StringConstants.ToShort);
            }
            var foundPets = _petService.SearchPetsByType(query);
            ShowPetsQueriedByType(foundPets);
           
        }

        private void ShowAvailableTypes(string message)
        {
            Print(message);
            foreach (var element in _petTypeService.GetPetTypes())
            {
                Print(element.Name);
            }
        }

        private void ShowPetsQueriedByType(List<Pet> foundPets)
        {
            if (foundPets.Count>0)
            {
                foreach (var pet in foundPets)
                {
                    PrintPet(pet, "Pet with the following properties found: ");
                }
            }
            else
            {
                Console.WriteLine("No pet found");
            }
        }

        #endregion

        #region print infromation about pet

        private static void PrintPet(Pet pet, string messageToShowBeforehand, string infoCrash)
        {
            if (pet == null)
            {
                Print(infoCrash);
            }
            PrintPet(pet, messageToShowBeforehand);
        }

        private static void PrintPet(Pet pet, string messageToShowBeforehand)
        {
            Console.WriteLine(
                $"{messageToShowBeforehand} {pet.Id}, Name: {pet.Name}, Previews Owner: {pet.PreviousOwner?.FirstName}" +
                               $" Type: {pet.Type?.Name}, Color: {pet.Color}, Price: {pet.Price}, " +
                               $"BirthDate: {pet.BirthDate :d MMMM, yyyy}, SoldDate: {pet.SoldDate :d MMMM, yyyy}");
        }
        

        #endregion
        

        #region R from CRUD pets
        private void ShowAllPets()
        {
            Print(StringConstants.ShowAllPetsMessage);
            foreach (var pet in _petService.GetPets())
            {
                PrintPet(pet, "Pet: ");
            }
        }
        

        #endregion

        #region C from CRUD pets
        private void CreatePet()
        {
            GetDataForCreateUpdateOperation(out var pet);
            pet =  _petService.Create(pet);
            PrintPet(pet, "Pet with the following properties created:", 
                "not saved properly. something crashed");
            
        }
        

        #endregion

        #region U from CRUD pets
        private void UpdatePet()
        {
            Print("Updating a pet ---------");
            Print("Give id of the pet to update: ");
            _utils.ReadIntegerFromString(out var id, StringConstants.OnlyNumbersAccepted);
            while (!_petService.CheckIfPetExistsById(id))
            {
                Print("Give existing id of the pet to update: ");
                _utils.ReadIntegerFromString(out  id, StringConstants.OnlyNumbersAccepted);
            }
            GetDataForCreateUpdateOperation(out var pet);
            pet.Id = id;
            pet = _petService.Update(pet);
            
            PrintPet(pet, "Pet with the following properties updated:", "not updated properly. something crashed");
           
            Print(StringConstants.Line);
        }
        #endregion
        
        #region CRUD helper classes for Create and Update Pet
        
        private void GetDataForCreateUpdateOperation(out Pet pet)
        {
            pet = new Pet
           {
               Name = GetName(), PreviousOwner = GetPreviewsOwner(),  Type = GetPetType(), BirthDate = GetBirthdate(out var b),
               SoldDate = GetSoldDate(b), Color = GetColor(), Price = GetPrice()
           };
        }

        private Owner GetPreviewsOwner()
        {
           Print("Please insert id of previews owner. Available owners are shown below");
           ShowAvailableOwners();
           _utils.ReadIntegerFromString(out var id, StringConstants.OnlyNumbersAccepted);
           var owner = _ownerService.GetOwnerById(id);
           while (owner == null)
           {
               Print("plase insert id of existing owner: ");
               _utils.ReadIntegerFromString(out id, StringConstants.OnlyNumbersAccepted);
               owner = _ownerService.GetOwnerById(id);
           }

           return owner;
        }

        private void ShowAvailableOwners()
        {
            foreach (var owner in _ownerService.GetOwners()) // we have to create an Owner service. 
            {
                PrintOwner(owner);
            }
        }

        private void PrintOwner(Owner owner)
        {
            Print($"Owner: Id: {owner.Id}, Name: {owner.FirstName}," +
                              $" Second Name: {owner.LastName}, Email: {owner.Email}, Phone no: {owner.PhoneNumber}");
        }

        private double GetPrice()
        {
            Print("give price");
            _utils.ReadDoubleFromString(out var price, StringConstants.NotDouble);
            while (price <= 0)
            {
                Print(StringConstants.Price);
                _utils.ReadDoubleFromString(out price, StringConstants.NotDouble); 
            }

            return price;
        }

        private string GetColor()
        {
            Print("Give color");
            _utils.GetMinimalStringInput(out var color, 3, StringConstants.ToShort);
            return color;
        }

        private DateTime GetSoldDate( DateTime birthdate)
        {
            Print($"give sold date {StringConstants.CorrectDateFormat}");
            _utils.GetDateInput(out var soldDate);
            while (DateTime.Compare(soldDate, birthdate)<0)
            {
                Print($"give sold date that is later than birthdate {StringConstants.CorrectDateFormat}");
                _utils.GetDateInput(out  soldDate);
            }
            return soldDate;
        }

        private DateTime GetBirthdate(out DateTime birthdate1)
        {
            Print($"give birthdate {StringConstants.CorrectDateFormat}");
            _utils.GetDateInput(out var birthdate);
            birthdate1 = birthdate;
            return birthdate;
        }

        private PetType GetPetType()
        {
            ShowAvailableTypes("Please give pet type. Below are available pet types ");
            _utils.GetMinimalStringInput(out var type,3, StringConstants.ToShort);
            while (!_petTypeService.CheckIfPetTypeExists(type))
            {
                Console.WriteLine("Please give correct type: ");
                _utils.GetMinimalStringInput(out  type,3, StringConstants.ToShort);
            }

            _petTypeService.GetPetTypeByName(type, out var petType);
            return petType;
        }

        private string GetName()
        {
            Console.WriteLine("Please give a name: ");
            _utils.GetMinimalStringInput(out var name,3, StringConstants.ToShort);
            return name;
        }

        #endregion
        
        
    }
}