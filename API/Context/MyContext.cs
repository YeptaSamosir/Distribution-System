using Microsoft.EntityFrameworkCore;
using API.Models;
using System;

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
        }
    }
}