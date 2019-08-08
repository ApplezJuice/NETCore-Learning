using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class AppDbContext : DbContext
    {
        // needs an isntance of the dbcontextoptions class
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        // for every type we have in our app, we create the cooresponding dbset prop, such as employee
        // using this prop to query and save to our db
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }

    }
}
