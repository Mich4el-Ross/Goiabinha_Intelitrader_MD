using Microsoft.EntityFrameworkCore;
using Goiabinha_Intelitrader_API.Models;

namespace Goiabinha_Intelitrader_API.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }


    }
}
