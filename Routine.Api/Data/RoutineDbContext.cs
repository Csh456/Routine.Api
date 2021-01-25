using Microsoft.EntityFrameworkCore;
using Routine.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Data
{
    public class RoutineDbContext:DbContext
    {
        public RoutineDbContext(DbContextOptions<RoutineDbContext> options)
            :base(options)
        {

        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .Property(x => x.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Company>()
                .Property(x => x.Introduction).IsRequired().HasMaxLength(500);

            modelBuilder.Entity<Employee>()
                .Property(x => x.EmployeeNo).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Employee>()
                .Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>()
                .Property(x => x.LastName).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Company)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = Guid.Parse("6f5d5115-5f80-54bc-8c0f-8c52b28707d2"),
                    CompanyId = Guid.Parse("3a496508-384f-2fb2-5d1e-4e19328df49a"),
                    DateOfBirth = new DateTime(1986, 11, 4),
                    EmployeeNo = "G003",
                    FirstName = "Mary",
                    LastName = "King",
                    Gender = Gender.女
                },
                new Employee
                {
                    Id = Guid.Parse("191bab05-ba96-e019-c86f-2a5b78ae12d2"),
                    CompanyId = Guid.Parse("c195d4f5-d469-c2b7-97bf-97cc8ff34b5f"),
                    DateOfBirth = new DateTime(1977, 4, 6),
                    EmployeeNo = "G097",
                    FirstName = "Kevin",
                    LastName = "Richardson",
                    Gender = Gender.男
                },
                 new Employee
                 {
                     Id = Guid.Parse("85e19631-e8f0-2460-fe82-fed36d8905ef"),
                     CompanyId = Guid.Parse("09802ddf-f462-d23f-88b5-b6991e7cd6c9"),
                     DateOfBirth = new DateTime(1977, 4, 6),
                     EmployeeNo = "G098",
                     FirstName = "Kevin",
                     LastName = "Richardson",
                     Gender = Gender.女
                 }
                );
            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = Guid.Parse("3a496508-384f-2fb2-5d1e-4e19328df49a"),
                    Name = "Microsoft",
                    Introduction = "Great Company"
                },
                new Company
                {
                    Id = Guid.Parse("c195d4f5-d469-c2b7-97bf-97cc8ff34b5f"),
                    Name = "Google",
                    Introduction = "Don't be evil"
                },
                new Company
                {
                    Id = Guid.Parse("09802ddf-f462-d23f-88b5-b6991e7cd6c9"),
                    Name = "Alipapa",
                    Introduction = "Fubao Company"
                }); 
        }
    }
}
