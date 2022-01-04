using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Education> Educations { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Profilling> Profillings { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet <Role> Roles { get; set; }
        public DbSet <AccountRole> AccountRoles{ get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<University>()
                .HasMany(education => education.Educations)
                .WithOne(university => university.University);

            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Account)
                .WithOne(e => e.Employee)
                .HasForeignKey<Account>(a => a.NIK);

            modelBuilder.Entity<Profilling>()
                 .HasOne(e => e.Education)
                 .WithMany(p => p.Profillings);

            modelBuilder.Entity<Account>()
                .HasOne(p => p.Profilling)
                .WithOne(a => a.Account)
                .HasForeignKey<Profilling>(p => p.NIK);

            modelBuilder.Entity<AccountRole>()
                .HasOne(ar => ar.Account)
                .WithMany(e => e.AccountRole);

            modelBuilder.Entity<Role>()
                .HasMany(r => r.AccountRole)
                .WithOne(e => e.Role);

        }

    }
}
