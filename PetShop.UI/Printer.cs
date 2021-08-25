using System;
using PetShop.Core.IServices;

namespace PetShop.UI
{
    public class Printer : IPrinter
    {
        private readonly IPetService _service;

        public Printer(IPetService service)
        {
            _service = service;
        }
        
        private void Print(string value)
        {
            Console.WriteLine(value);
        }
        
     

        #region CRUD

        //read
        //make it private again after that
        private void PrintAllPets()
        {
            Console.WriteLine(Constants.ShowingAllPets);
            foreach (var pet in _service.GetPets())
            {
                Console.WriteLine($"Pet: {pet.Name}, {pet.Type}, {pet.Price}, {pet.BirthDate}, {pet.Color}, {pet.SoldDate}");
            }
        }
        #endregion

        public void StartUi()
        {
            PrintAllPets();
        }
    }
}