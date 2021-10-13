﻿// <auto-generated />
using System;
using API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20211013014225_newMigrationSeedInitialData")]
    partial class newMigrationSeedInitialData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("API.Models.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AttemptCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("AccountId");

                    b.ToTable("tb_m_accounts");

                    b.HasData(
                        new
                        {
                            AccountId = 1,
                            AttemptCount = 0,
                            CreatedAt = new DateTime(2021, 10, 13, 8, 42, 24, 443, DateTimeKind.Local).AddTicks(9538),
                            Email = "admin@mail.com",
                            IsActive = true,
                            Name = "Super Administrator",
                            Password = "$2b$12$iwsge4opNQpo3oYrmSw6pe5dkNKZFDJvjLhTlbP4dPuwPIryHR6UC",
                            UpdatedAt = new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(101),
                            Username = "Admin"
                        });
                });

            modelBuilder.Entity("API.Models.AccountRole", b =>
                {
                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("AccountId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("tb_m_account_roles");

                    b.HasData(
                        new
                        {
                            AccountId = 1,
                            RoleId = "SP-ADM",
                            CreatedAt = new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(2623),
                            UpdatedAt = new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(3091)
                        },
                        new
                        {
                            AccountId = 1,
                            RoleId = "ADM",
                            CreatedAt = new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(3521),
                            UpdatedAt = new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(3527)
                        });
                });

            modelBuilder.Entity("API.Models.Candidate", b =>
                {
                    b.Property<int>("CandidateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Grade")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("CandidateId");

                    b.ToTable("tb_m_candidates");
                });

            modelBuilder.Entity("API.Models.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("CompanyId");

                    b.ToTable("tb_m_companies");
                });

            modelBuilder.Entity("API.Models.DetailScheduleInterview", b =>
                {
                    b.Property<int>("DetailScheduleInterviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailCandidate")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("EmailCustomer")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("ScheduleInterviewId")
                        .HasColumnType("nvarchar(16)");

                    b.Property<int>("TypeLocation")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("DetailScheduleInterviewId");

                    b.HasIndex("ScheduleInterviewId");

                    b.ToTable("tb_m_detail_schedule_interviews");
                });

            modelBuilder.Entity("API.Models.Onboard", b =>
                {
                    b.Property<int>("OnboardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CandidateId")
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusId")
                        .HasColumnType("nvarchar(16)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("OnboardId");

                    b.HasIndex("CandidateId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("StatusId");

                    b.ToTable("tb_tr_onboards");
                });

            modelBuilder.Entity("API.Models.Role", b =>
                {
                    b.Property<string>("RoleId")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("RoleId");

                    b.ToTable("tb_m_roles");

                    b.HasData(
                        new
                        {
                            RoleId = "SP-ADM",
                            CreatedAt = new DateTime(2021, 10, 13, 8, 42, 23, 969, DateTimeKind.Local).AddTicks(2210),
                            Name = "Super Administrator",
                            UpdatedAt = new DateTime(2021, 10, 13, 8, 42, 23, 970, DateTimeKind.Local).AddTicks(4368)
                        },
                        new
                        {
                            RoleId = "ADM",
                            CreatedAt = new DateTime(2021, 10, 13, 8, 42, 23, 970, DateTimeKind.Local).AddTicks(5132),
                            Name = "Administrator",
                            UpdatedAt = new DateTime(2021, 10, 13, 8, 42, 23, 970, DateTimeKind.Local).AddTicks(5140)
                        });
                });

            modelBuilder.Entity("API.Models.ScheduleInterview", b =>
                {
                    b.Property<string>("ScheduleInterviewId")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<int>("CandidateId")
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime>("EndInterview")
                        .HasColumnType("datetime2");

                    b.Property<string>("FollowingBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartInterview")
                        .HasColumnType("datetime2");

                    b.Property<string>("StatusId")
                        .IsRequired()
                        .HasColumnType("nvarchar(16)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ScheduleInterviewId");

                    b.HasIndex("CandidateId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("StatusId");

                    b.ToTable("tb_tr_schedule_interviews");
                });

            modelBuilder.Entity("API.Models.ScheduleInterviewDateOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateInterview")
                        .HasColumnType("datetime2");

                    b.Property<string>("ScheduleInterviewId")
                        .HasColumnType("nvarchar(16)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleInterviewId");

                    b.ToTable("tb_tr_schedule_interview_date_options");
                });

            modelBuilder.Entity("API.Models.Status", b =>
                {
                    b.Property<string>("StatusId")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("TypeStatusId")
                        .HasColumnType("nvarchar(16)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("StatusId");

                    b.HasIndex("TypeStatusId");

                    b.ToTable("tb_m_statuses");

                    b.HasData(
                        new
                        {
                            StatusId = "ITV-WD",
                            CreatedAt = new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(8449),
                            Name = "Waiting Schedule",
                            TypeStatusId = "ITV",
                            UpdatedAt = new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(8942)
                        },
                        new
                        {
                            StatusId = "ITV-WC",
                            CreatedAt = new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9419),
                            Name = "Waiting Confirmation",
                            TypeStatusId = "ITV",
                            UpdatedAt = new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9425)
                        },
                        new
                        {
                            StatusId = "ITV-OG",
                            CreatedAt = new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9428),
                            Name = "Interview in progress",
                            TypeStatusId = "ITV",
                            UpdatedAt = new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9430)
                        },
                        new
                        {
                            StatusId = "ITV-AC",
                            CreatedAt = new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9433),
                            Name = "Candidate Accepted",
                            TypeStatusId = "ITV",
                            UpdatedAt = new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9435)
                        },
                        new
                        {
                            StatusId = "ITV-DN",
                            CreatedAt = new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9437),
                            Name = "Done",
                            TypeStatusId = "ITV",
                            UpdatedAt = new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9439)
                        },
                        new
                        {
                            StatusId = "ITV-CN",
                            CreatedAt = new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9441),
                            Name = "Candidate canceled",
                            TypeStatusId = "ITV",
                            UpdatedAt = new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9443)
                        },
                        new
                        {
                            StatusId = "ONB-OG",
                            CreatedAt = new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9445),
                            Name = "Onboard in progress",
                            TypeStatusId = "ONB",
                            UpdatedAt = new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9447)
                        },
                        new
                        {
                            StatusId = "ONB-DN",
                            CreatedAt = new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9450),
                            Name = "Done",
                            TypeStatusId = "ONB",
                            UpdatedAt = new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9451)
                        });
                });

            modelBuilder.Entity("API.Models.TypeStatus", b =>
                {
                    b.Property<string>("TypeStatusId")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("TypeStatusId");

                    b.ToTable("tb_m_type_statuses");

                    b.HasData(
                        new
                        {
                            TypeStatusId = "ITV",
                            CreatedAt = new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(5296),
                            Name = "Schedule Interview",
                            UpdatedAt = new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(5804)
                        },
                        new
                        {
                            TypeStatusId = "ONB",
                            CreatedAt = new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(6293),
                            Name = "Onboard Candidate",
                            UpdatedAt = new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(6300)
                        });
                });

            modelBuilder.Entity("API.Models.AccountRole", b =>
                {
                    b.HasOne("API.Models.Account", "Account")
                        .WithMany("AccountRoles")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Role", "Role")
                        .WithMany("AccountRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("API.Models.DetailScheduleInterview", b =>
                {
                    b.HasOne("API.Models.ScheduleInterview", "ScheduleInterview")
                        .WithMany("DetailScheduleInterviews")
                        .HasForeignKey("ScheduleInterviewId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("ScheduleInterview");
                });

            modelBuilder.Entity("API.Models.Onboard", b =>
                {
                    b.HasOne("API.Models.Candidate", "Candidate")
                        .WithMany("Onboards")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Company", "Company")
                        .WithMany("Onboards")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Status", "Status")
                        .WithMany("Onboards")
                        .HasForeignKey("StatusId");

                    b.Navigation("Candidate");

                    b.Navigation("Company");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("API.Models.ScheduleInterview", b =>
                {
                    b.HasOne("API.Models.Candidate", "Candidate")
                        .WithMany("ScheduleInterviews")
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Company", "Company")
                        .WithMany("ScheduleInterviews")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Status", "Status")
                        .WithMany("ScheduleInterviews")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Candidate");

                    b.Navigation("Company");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("API.Models.ScheduleInterviewDateOption", b =>
                {
                    b.HasOne("API.Models.ScheduleInterview", "ScheduleInterview")
                        .WithMany("ScheduleInterviewDateOptions")
                        .HasForeignKey("ScheduleInterviewId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("ScheduleInterview");
                });

            modelBuilder.Entity("API.Models.Status", b =>
                {
                    b.HasOne("API.Models.TypeStatus", "TypeStatus")
                        .WithMany("Status")
                        .HasForeignKey("TypeStatusId");

                    b.Navigation("TypeStatus");
                });

            modelBuilder.Entity("API.Models.Account", b =>
                {
                    b.Navigation("AccountRoles");
                });

            modelBuilder.Entity("API.Models.Candidate", b =>
                {
                    b.Navigation("Onboards");

                    b.Navigation("ScheduleInterviews");
                });

            modelBuilder.Entity("API.Models.Company", b =>
                {
                    b.Navigation("Onboards");

                    b.Navigation("ScheduleInterviews");
                });

            modelBuilder.Entity("API.Models.Role", b =>
                {
                    b.Navigation("AccountRoles");
                });

            modelBuilder.Entity("API.Models.ScheduleInterview", b =>
                {
                    b.Navigation("DetailScheduleInterviews");

                    b.Navigation("ScheduleInterviewDateOptions");
                });

            modelBuilder.Entity("API.Models.Status", b =>
                {
                    b.Navigation("Onboards");

                    b.Navigation("ScheduleInterviews");
                });

            modelBuilder.Entity("API.Models.TypeStatus", b =>
                {
                    b.Navigation("Status");
                });
#pragma warning restore 612, 618
        }
    }
}
