using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    // extension classes must be static
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // employee table we want to seed in the db
            modelBuilder.Entity<Employee>().HasData(
                    new Employee
                    {
                        Id = 1,
                        Name = "Mary",
                        Department = Dept.IT,
                        Email = "Maryk@testing.com"
                    },
                    new Employee
                    {
                        Id = 2,
                        Name = "Mike",
                        Department = Dept.IT,
                        Email = "Mike@testing.com"
                    }
                );
        }
    }
}
