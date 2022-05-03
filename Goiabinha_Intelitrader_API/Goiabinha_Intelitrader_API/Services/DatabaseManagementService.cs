using Goiabinha_Intelitrader_API.Models;
using Goiabinha_Intelitrader_API.Data;
using Microsoft.EntityFrameworkCore;

namespace Goiabinha_Intelitrader_API.Services;

public static class DatabaseManagementService
{
    public static void MigrationInitialisation(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            var serviceDb  = serviceScope.ServiceProvider
                             .GetService<AppDataContext>();
                             
            System.Console.WriteLine("Appling Migrations...");
            serviceDb.Database.Migrate();
        }
    }
}