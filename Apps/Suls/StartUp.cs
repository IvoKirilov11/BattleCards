using Microsoft.EntityFrameworkCore;
using Suls.Data;
using Suls.Services;
using SUS.MvcFramework;
using SUSHTTP;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suls
{
    public class StartUp : IMvcAplication
    {
        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<IProblemsService, ProblemsService>();
            serviceCollection.Add<ISubmissionsService, SubmissionsService>();
        }
    }
}
