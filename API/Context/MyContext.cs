using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
        public DbSet<Employee> Employees
        {
            get; set;
        }
        public DbSet<Account> Accounts
        {
            get; set;
        }
        public DbSet<Profiling> Profilings
        {
            get; set;

        }
        public DbSet<Education> Educations
        {
            get; set;
        }
        public DbSet<University> Universities
        {
            get; set;
        }
         public DbSet<AccountRole> AccountRoles
        {
            get; set;
        }
        public DbSet<Role> Roles
        {
            get; set;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* Relasi One to one*/
            /*dengan singkatan*/
            modelBuilder.Entity<Employee>()
            .HasOne(a => a.Account)
            .WithOne(e => e.Employee)
            .HasForeignKey<Account>(a => a.NIK);

            /* tanpa singkatan*/
            modelBuilder.Entity<Account>()
            .HasOne(Profiling => Profiling.Profiling)
            .WithOne(account => account.Account)
            .HasForeignKey<Profiling>(Profiling => Profiling.NIK);

            /*tambahkan akhiran s pada relasi many sesuai di icollection*/
            /* one to many relation */
            modelBuilder.Entity<Profiling>()
           .HasOne(e => e.Education)
           .WithMany(p => p.Profilings);

            /* many to one relation*/
            modelBuilder.Entity<University>()
             .HasMany(e => e.Educations)
             .WithOne(u => u.University);

              /* many to one relation */
            modelBuilder.Entity<Role>()
           .HasMany(ar => ar.AccountRoles)
           .WithOne(r => r.Role);

            /* many to one relation*/
            modelBuilder.Entity<Account>()
             .HasMany(ar => ar.AccountRoles)
             .WithOne(a => a.Account);


        }

    /*    enable lazyloading proxie*/
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }



    }
}
