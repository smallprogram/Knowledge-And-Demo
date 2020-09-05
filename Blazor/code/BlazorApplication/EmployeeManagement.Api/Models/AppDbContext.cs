using EmployeeManagement.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Api.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public DbSet<Employee> Employees { set; get; }
        public DbSet<Department> Departments { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed Departments Table
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = Guid.Parse("cbcebfdc-d176-4045-a680-75d5893fe185"), DepartmentName = "IT" });
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = Guid.Parse("fa87c5b3-0bbb-4ddb-a4ee-33a7a875cfe9"), DepartmentName = "HR" });
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = Guid.Parse("53049207-ed06-4411-b920-9ab7de6b5c0e"), DepartmentName = "Payroll" });
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = Guid.Parse("8347d0d5-5087-44a9-b3e6-1fcbb2d399dc"), DepartmentName = "Admin" });

            // Seed Employee Table
            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = Guid.Parse("e5122517-92e7-42da-a445-915de6ee9717"),
                FirstName = "John",
                LastName = "Hastings",
                Email = "David@pragimtech.com",
                DateOfBrith = new DateTime(1980, 10, 5),
                Gender = Gender.Male,
                DepartmentId = Guid.Parse("cbcebfdc-d176-4045-a680-75d5893fe185"),
                PhotoPath = "images/john.jpg"
            });

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = Guid.Parse("72b544d4-b703-4e43-baa9-25612d6dd7bf"),
                FirstName = "Sam",
                LastName = "Galloway",
                Email = "Sam@pragimtech.com",
                DateOfBrith = new DateTime(1981, 12, 22),
                Gender = Gender.Male,
                DepartmentId = Guid.Parse("fa87c5b3-0bbb-4ddb-a4ee-33a7a875cfe9"),
                PhotoPath = "images/sam.jpg"
            });

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = Guid.Parse("6a39fe40-8a48-492f-b5af-de38a2946b91"),
                FirstName = "Mary",
                LastName = "Smith",
                Email = "mary@pragimtech.com",
                DateOfBrith = new DateTime(1979, 11, 11),
                Gender = Gender.Female,
                DepartmentId = Guid.Parse("cbcebfdc-d176-4045-a680-75d5893fe185"),
                PhotoPath = "images/mary.jpg"
            });

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                EmployeeId = Guid.Parse("b15ace62-d94d-40fa-8e22-f048998ded80"),
                FirstName = "Sara",
                LastName = "Longway",
                Email = "sara@pragimtech.com",
                DateOfBrith = new DateTime(1982, 9, 23),
                Gender = Gender.Female,
                DepartmentId = Guid.Parse("53049207-ed06-4411-b920-9ab7de6b5c0e"),
                PhotoPath = "images/sara.jpg"
            });
        }
    }
}
