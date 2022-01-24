using BattleCards.Controllers;
using BattleCards.Services;
using SUS.MvcFramework;
using SUSHTTP;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleCards
{
    public class StartUp : IMvcAplication
    {
        public void Configure(List<Route> routeTable)
        {
           
            
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUserService, UserService>();
            serviceCollection.Add<ICardsService, CardsService>();
        }
    }
}
