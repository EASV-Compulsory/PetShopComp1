using System;
using Microsoft.VisualBasic;
using PetShop.Core.IServices;
using PetShop.Core.Models;

namespace PetShop.UI
{
    public class Menu : IMenu
    {
        private readonly IUtils _utils;
        private IPetService _petService;
        private IPetTypeService _petTypeService;
        

        public Menu(IUtils utils, IPetService petService, IPetTypeService petTypeService)
        {
            _utils = utils;
            _petService = petService;
            _petTypeService = petTypeService;
        }

        #region SWITCH for CRUD operations
        
        public void  StartUi()
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
                    
                        
                }
                ShowMainMenu();
                selection = GetOptionFromString();
            }
            Print("Bye bye");
        }
        

        #endregion
      
        
        private void Print(string value)
        {
            Console.WriteLine(value);
        }
        
        private void ShowGreetings()
        {
            Console.WriteLine(StringConstants.WelcomeGreetings);
        }


        #region Get Option from string
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
            Console.WriteLine(result ? $"Pet with id: {id} was deleted" : $"failed. Pet with id {id} wasnt deleted");
        }
        public Options GetOptionFromString()
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
        

        public void ShowMainMenu()
        {
            Console.WriteLine(StringConstants.PossibleOperations +"\n" + _utils.ConcatPossibleOptions());
        }

        #region CRUD helper classes
        
        private void GiveFiveCheapestPets()
        {
            var fiveCheapest = _petService.GetXCheapestPets(5);
            Print("five cheapest pets: ");
            foreach (var pet in fiveCheapest)
            {
                Print($"Pet :{pet.Id} {pet.Name}" +
                      $" {pet.Type?.Name} {pet.Color} Price: {pet.Price} " +
                      $" Birthdate: {pet.BirthDate} {pet.SoldDate}");
            }
        }
        
         private void SortPetsByPrice()
        {
            var sortedPets = _petService.SortPetsByPriceAsc();
            Print("pets sorted by price in ascending order");
            foreach (var pet in sortedPets)
            {
                Print($"Pet :{pet.Id} {pet.Name}" +
                      $" {pet.Type?.Name} {pet.Color} Price: {pet.Price} " +
                      $" Birthdate: {pet.BirthDate} {pet.SoldDate}");
            }

        }

        private void SearchPetsByType()
        {
            Print("Please insert query: ");
            _utils.GetMinimalStringInput(out var query, 1, StringConstants.ToShort);
            var foundPets = _petService.SearchPetsByType(query);
            if (foundPets.Count>0)
            {
                foreach (var pet in foundPets)
                {
                    Console.WriteLine( $"Pet with the following properties found: {pet.Id} {pet.Name}" +
                                       $"{pet.Type?.Name} {pet.Color} {pet.Price}" +
                                       $"{pet.BirthDate} {pet.SoldDate}");
                }
            }
            else
            {
                Console.WriteLine("No pet found");
            }
        }

        private void ShowAllPets()
        {
            Print(StringConstants.ShowAllPetsMessage);
            foreach (var pet in _petService.GetPets())
            {
                Print($"Pet :{pet.Id} {pet.Name}" +
                      $" {pet.Type?.Name} {pet.Color} {pet.Price}" +
                      $"{pet.BirthDate} {pet.SoldDate}");
            }
            Print(StringConstants.Line);
        }

        private void CreatePet()
        {
            GetDataForCreateOperation(out var pet);
            pet =  _petService.Create(pet);
                      
            Console.WriteLine(pet ==null ? "not saved properly. something crashed" :
                $"Pet with the following properties created: {pet.Id} {pet.Name}" +
                $"{pet.Type?.Name} {pet.Color} {pet.Price}" +
                $"{pet.BirthDate} {pet.SoldDate}");
        }

        public void GetDataForCreateOperation(out Pet pet)
        {
            //Name
            Console.WriteLine("Please give a name: ");
           _utils.GetMinimalStringInput(out var name,3, StringConstants.ToShort);
           
           //Type
           Console.WriteLine("Please give a type: ");
           //show possible types
           Console.WriteLine("Available pet types: ");
           foreach (var element in _petTypeService.GetPetTypes())
           {
               Print(element.Name);
           }
           _utils.GetMinimalStringInput(out var type,3, StringConstants.ToShort);
           //check if such type exists
           while (!_petTypeService.CheckIfPetTypeExists(type))
           {
               Console.WriteLine("Please give correct type: ");
               _utils.GetMinimalStringInput(out  type,3, StringConstants.ToShort);
           }

           _petTypeService.GetPetTypeByName(type, out var petType);
           
           //BirthDate
           Print("give birthdate");
           _utils.GetDateInput(out var birthdate);
           Print("give solddate");
           _utils.GetDateInput(out var soldDate);
           //later validate if birthdate > soldDate
           //Color && Price
           Print("Give color");
           _utils.GetMinimalStringInput(out var color, 3, StringConstants.ToShort);
           Print("give price");
           _utils.ReadDoubleFromString(out var price, StringConstants.NotDouble);
           //validate price. It should be bigger than zero

           pet = new Pet
           {
               Name = name, Type = petType, BirthDate = birthdate,
               SoldDate = soldDate, Color = color, Price = price
           };
        }
        

        #endregion
        
        
    }
}