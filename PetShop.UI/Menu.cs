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
                        GetDataForCreateOperation(out var pet);
                        pet =  _petService.Create(pet);
                      
                       Console.WriteLine(pet ==null ? "not saved properly. something crashed" :
                           $"Pet with the following properties created: {pet.Id} {pet.Name}" +
                             $"{pet.Type?.Name} {pet.Color} {pet.Price}" +
                             $"{pet.BirthDate} {pet.SoldDate}");
                       break;
                    }
                    case Options.ShowAllPets:
                    {
                        Print(StringConstants.ShowAllPetsMessage);
                        foreach (var pet in _petService.GetPets())
                        {
                            Print($"Pet :{pet.Id} {pet.Name}" +
                                  $" {pet.Type?.Name} {pet.Color} {pet.Price}" +
                                  $"{pet.BirthDate} {pet.SoldDate}");
                        }
                        Print(StringConstants.Line);
                        break;
                    }
                    case Options.SearchPetsByType:
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

        public void ShowMainMenu()
        {
            Console.WriteLine(StringConstants.PossibleOperations +"\n" + _utils.ConcatPossibleOptions());
        }

        #region CRUD helper classes
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