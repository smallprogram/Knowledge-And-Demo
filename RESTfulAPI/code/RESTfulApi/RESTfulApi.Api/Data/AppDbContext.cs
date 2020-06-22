using Microsoft.EntityFrameworkCore;
using RESTfulApi.Api.Entities;
using System;

namespace RESTfulApi.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            // 数据库相关主外键，字段长度设置
            modelBuilder.Entity<Company>().Property(x => x.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Company>().Property(x => x.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Employee>().Property(x => x.EmployeeNo).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Employee>().Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>().Property(x => x.LastName).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Company)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);  // DeleteBehavior.Restrict 不级联删除   DeleteBehavior.Cascade 级联删除

            // 种子数据
            modelBuilder.Entity<Company>().HasData(
                new Company 
                { 
                    Id = Guid.Parse("e2f039ad-237c-4efe-97e9-15deccda6691"),
                    Name = "Microsoft",
                    Introduction = "Great Company"
                },
                new Company
                {
                    Id = Guid.Parse("e2b07120-ce3d-4e30-8fa8-a4fb76c663e5"),
                    Name = "Google",
                    Introduction = "Don't be evil"
                },
                new Company
                {
                    Id = Guid.Parse("a2f92442-73dc-4091-ad4d-4398b39f4d47"),
                    Name = "Alibaba",
                    Introduction = "FuBao Company"
                });


            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = Guid.Parse("4b501cb3-d168-4cc0-b375-48fb33f318a4"),
                    CompanyId = Guid.Parse("e2f039ad-237c-4efe-97e9-15deccda6691"),
                    DateOfBirth = new DateTime(1976, 1, 2),
                    EmployeeNo = "MSFT231",
                    FirstName = "Nick",
                    LastName = "Carter",
                    Gender = Gender.男
                },
                new Employee
                {
                    Id = Guid.Parse("7eaa532c-1be5-472c-a738-94fd26e5fad6"),
                    CompanyId = Guid.Parse("e2f039ad-237c-4efe-97e9-15deccda6691"),
                    DateOfBirth = new DateTime(1981, 12, 5),
                    EmployeeNo = "MSFT245",
                    FirstName = "Vince",
                    LastName = "Carter",
                    Gender = Gender.男
                },
                new Employee
                {
                    Id = Guid.Parse("72457e73-ea34-4e02-b575-8d384e82a481"),
                    CompanyId = Guid.Parse("e2b07120-ce3d-4e30-8fa8-a4fb76c663e5"),
                    DateOfBirth = new DateTime(1986, 11, 4),
                    EmployeeNo = "G003",
                    FirstName = "Mary",
                    LastName = "King",
                    Gender = Gender.女
                },
                new Employee
                {
                    Id = Guid.Parse("7644b71d-d74e-43e2-ac32-8cbadd7b1c3a"),
                    CompanyId = Guid.Parse("e2b07120-ce3d-4e30-8fa8-a4fb76c663e5"),
                    DateOfBirth = new DateTime(1977, 4, 6),
                    EmployeeNo = "G097",
                    FirstName = "Kevin",
                    LastName = "Richardson",
                    Gender = Gender.男
                },
                new Employee
                {
                    Id = Guid.Parse("679dfd33-32e4-4393-b061-f7abb8956f53"),
                    CompanyId = Guid.Parse("a2f92442-73dc-4091-ad4d-4398b39f4d47"),
                    DateOfBirth = new DateTime(1967, 1, 24),
                    EmployeeNo = "A009",
                    FirstName = "卡",
                    LastName = "里",
                    Gender = Gender.女
                },
                new Employee
                {
                    Id = Guid.Parse("1861341e-b42b-410c-ae21-cf11f36fc574"),
                    CompanyId = Guid.Parse("a2f92442-73dc-4091-ad4d-4398b39f4d47"),
                    DateOfBirth = new DateTime(1957, 3, 8),
                    EmployeeNo = "A404",
                    FirstName = "Not",
                    LastName = "Man",
                    Gender = Gender.男
                });
        }
    }
}
