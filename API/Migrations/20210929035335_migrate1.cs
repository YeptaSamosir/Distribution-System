using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class migrate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_accounts", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_candidates",
                columns: table => new
                {
                    CandidateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_candidates", x => x.CandidateId);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_companies",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_roles",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_type_statuses",
                columns: table => new
                {
                    TypeStatusId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_type_statuses", x => x.TypeStatusId);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_account_roles",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_account_roles", x => new { x.AccountId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_tb_m_account_roles_tb_m_accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "tb_m_accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_m_account_roles_tb_m_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tb_m_roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_statuses",
                columns: table => new
                {
                    StatusId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeStatusId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_statuses", x => x.StatusId);
                    table.ForeignKey(
                        name: "FK_tb_m_statuses_tb_m_type_statuses_TypeStatusId",
                        column: x => x.TypeStatusId,
                        principalTable: "tb_m_type_statuses",
                        principalColumn: "TypeStatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_onboards",
                columns: table => new
                {
                    OnboardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_onboards", x => x.OnboardId);
                    table.ForeignKey(
                        name: "FK_tb_m_onboards_tb_m_candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "tb_m_candidates",
                        principalColumn: "CandidateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_m_onboards_tb_m_companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tb_m_companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_m_onboards_tb_m_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "tb_m_statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_schedule_interviews",
                columns: table => new
                {
                    ScheduleInterviewId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CandidateId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartInterview = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndInterview = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_schedule_interviews", x => x.ScheduleInterviewId);
                    table.ForeignKey(
                        name: "FK_tb_m_schedule_interviews_tb_m_candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "tb_m_candidates",
                        principalColumn: "CandidateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_m_schedule_interviews_tb_m_companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tb_m_companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_m_schedule_interviews_tb_m_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "tb_m_statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_detail_schedule_interviews",
                columns: table => new
                {
                    DetailScheduleInterviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleInterviewId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EmailCandidate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailCustomer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeLocation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_detail_schedule_interviews", x => x.DetailScheduleInterviewId);
                    table.ForeignKey(
                        name: "FK_tb_m_detail_schedule_interviews_tb_m_schedule_interviews_ScheduleInterviewId",
                        column: x => x.ScheduleInterviewId,
                        principalTable: "tb_m_schedule_interviews",
                        principalColumn: "ScheduleInterviewId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_account_roles_RoleId",
                table: "tb_m_account_roles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_detail_schedule_interviews_ScheduleInterviewId",
                table: "tb_m_detail_schedule_interviews",
                column: "ScheduleInterviewId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_onboards_CandidateId",
                table: "tb_m_onboards",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_onboards_CompanyId",
                table: "tb_m_onboards",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_onboards_StatusId",
                table: "tb_m_onboards",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_schedule_interviews_CandidateId",
                table: "tb_m_schedule_interviews",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_schedule_interviews_CompanyId",
                table: "tb_m_schedule_interviews",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_schedule_interviews_StatusId",
                table: "tb_m_schedule_interviews",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_statuses_TypeStatusId",
                table: "tb_m_statuses",
                column: "TypeStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_m_account_roles");

            migrationBuilder.DropTable(
                name: "tb_m_detail_schedule_interviews");

            migrationBuilder.DropTable(
                name: "tb_m_onboards");

            migrationBuilder.DropTable(
                name: "tb_m_accounts");

            migrationBuilder.DropTable(
                name: "tb_m_roles");

            migrationBuilder.DropTable(
                name: "tb_m_schedule_interviews");

            migrationBuilder.DropTable(
                name: "tb_m_candidates");

            migrationBuilder.DropTable(
                name: "tb_m_companies");

            migrationBuilder.DropTable(
                name: "tb_m_statuses");

            migrationBuilder.DropTable(
                name: "tb_m_type_statuses");
        }
    }
}
