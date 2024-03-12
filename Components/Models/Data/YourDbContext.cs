

using Microsoft.EntityFrameworkCore;
using ExtentApplication_UserManagement.Components.Models;
using System.Collections.Generic;

namespace ExtentApplication_UserManagement.Components.Models.Data
{
    public class YourDbContext : DbContext
    {
        public YourDbContext(DbContextOptions<YourDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        
    }
}
