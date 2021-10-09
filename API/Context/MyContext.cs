using Microsoft.EntityFrameworkCore;
using API.Models;
using System;
using API.Hash;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Onboard> Onboards { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ScheduleInterview> ScheduleInterviews { get; set; }
        public DbSet<DetailScheduleInterview> DetailScheduleInterviews { get; set; }
        public DbSet<ScheduleInterviewDateOption> ScheduleInterviewDateOptions { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<TypeStatus> TypeStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountRole>().HasKey(x => new { x.AccountId, x.RoleId });
            modelBuilder.Entity<Account>().HasMany(x => x.AccountRoles).WithOne(x => x.Account);
            modelBuilder.Entity<Role>().HasMany(x => x.AccountRoles).WithOne(x => x.Role);

            modelBuilder.Entity<Candidate>().HasMany(x => x.ScheduleInterviews).WithOne(x => x.Candidate);
            modelBuilder.Entity<Candidate>().HasMany(x => x.Onboards).WithOne(x => x.Candidate);

            modelBuilder.Entity<Company>().HasMany(x => x.ScheduleInterviews).WithOne(x => x.Company);
            modelBuilder.Entity<Company>().HasMany(x => x.Onboards).WithOne(x => x.Company);

            modelBuilder.Entity<TypeStatus>().HasMany(x => x.Status).WithOne(x => x.TypeStatus);

            modelBuilder.Entity<Status>().HasMany(x => x.Onboards).WithOne(x => x.Status);
            modelBuilder.Entity<Status>().HasMany(x => x.ScheduleInterviews).WithOne(x => x.Status);

            modelBuilder.Entity<ScheduleInterview>().HasMany(x => x.DetailScheduleInterviews).WithOne(x => x.ScheduleInterview);

            modelBuilder.Entity<ScheduleInterview>().HasMany(x => x.ScheduleInterviewDateOptions).WithOne(x => x.ScheduleInterview);

            //seed data
            modelBuilder.Entity<Role>().HasData(
                new Role { 
                    RoleId = "SP-ADM",
                    Name = "Super Administrator",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Role
                {
                    RoleId = "ADM",
                    Name = "Administrator",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            );

            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    AccountId = 1,
                    Name = "Super Administrator",
                    Email = "admin@mail.com",
                    Username = "Admin",
                    Password = Hashing.HashPassword("Admin123"),
                    AttemptCount = 0,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            );

            modelBuilder.Entity<AccountRole>().HasData(
                new AccountRole
                {
                    AccountId = 1,
                    RoleId = "SP-ADM",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new AccountRole
                {
                    AccountId = 1,
                    RoleId = "ADM",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            );

            modelBuilder.Entity<TypeStatus>().HasData(
                new TypeStatus {
                    TypeStatusId = "ITV",
                    Name = "Schedule Interview",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new TypeStatus {
                    TypeStatusId = "ONB",
                    Name = "Onboard Candidate",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            );

            modelBuilder.Entity<Status>().HasData(
                new Status {
                    StatusId = "ITV-WD",
                    Name = "Waiting Date",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Status
                {
                    StatusId = "ITV-OG",
                    Name = "On Going",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Status
                {
                    StatusId = "ITV-DN",
                    Name = "Done",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Status
                {
                    StatusId = "ITV-CN",
                    Name = "Cancel",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Status
                {
                    StatusId = "ONB-OG",
                    Name = "On Going",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Status
                {
                     StatusId = "ONB-DN",
                     Name = "Done",
                     CreatedAt = DateTime.Now,
                     UpdatedAt = DateTime.Now
                }
            );
        }
    }
}