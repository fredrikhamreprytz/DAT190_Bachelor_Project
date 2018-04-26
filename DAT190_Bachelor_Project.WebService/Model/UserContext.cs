using System;
using DAT190_Bachelor_Project.Model;
using Microsoft.EntityFrameworkCore;

namespace DAT190_Bachelor_Project.WebService.Model
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
