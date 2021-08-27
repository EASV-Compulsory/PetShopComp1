using System;
using Microsoft.Extensions.DependencyInjection;
using PetShop.Core.IServices;
using PetShop.Domain.IRepositories;
using PetShop.Domain.Services;
using PetShop.Infrastructure.Data;
using PetShop.Infrastructure.Data.Repositories;


namespace PetShop.UI
{
    class Program
    {
        private static void Main(string[] args)
        {
            FakeDb.InitData();
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IPetRepository, PetRepository>();
            serviceCollection.AddScoped<IPetTypeRepository, PetTypeRepository>();
            serviceCollection.AddScoped<IPetTypeService, PetTypeService>();
            serviceCollection.AddScoped<IPetService, PetService>();
            serviceCollection.AddScoped<IUtils, Utils>();
            serviceCollection.AddScoped<IMenu, Menu>();
            
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var menu = serviceProvider.GetRequiredService<IMenu>();
            menu.StartUi();



        }
    }
}