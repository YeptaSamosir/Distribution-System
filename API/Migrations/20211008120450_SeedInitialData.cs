using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class SeedInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_schedule_interviews_tb_m_statuses_StatusId",
                table: "tb_tr_schedule_interviews");

            migrationBuilder.AlterColumn<string>(
                name: "StatusId",
                table: "tb_tr_schedule_interviews",
                type: "nvarchar(16)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(16)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "tb_tr_schedule_interviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "JobTitle",
                table: "tb_tr_schedule_interviews",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                table: "tb_tr_schedule_interviews",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Grade",
                table: "tb_m_candidates",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "tb_m_candidates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "tb_tr_schedule_interview_date_options",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleInterviewId = table.Column<string>(type: "nvarchar(16)", nullable: true),
                    DateInterview = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_schedule_interview_date_options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_tr_schedule_interview_date_options_tb_tr_schedule_interviews_ScheduleInterviewId",
                        column: x => x.ScheduleInterviewId,
                        principalTable: "tb_tr_schedule_interviews",
                        principalColumn: "ScheduleInterviewId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "tb_m_accounts",
                columns: new[] { "AccountId", "AttemptCount", "CreatedAt", "Email", "IsActive", "Name", "Password", "UpdatedAt", "Username" },
                values: new object[] { 1, 0, new DateTime(2021, 10, 8, 19, 4, 49, 885, DateTimeKind.Local).AddTicks(2991), "admin@mail.com", true, "Super Administrator", "$2b$12$dgYX0be6hih0/TnWgKoN7.cBfNl0ZxlaNwgEthmffV.R1EzDsMCqe", new DateTime(2021, 10, 8, 19, 4, 49, 885, DateTimeKind.Local).AddTicks(3442), "Admin" });

            migrationBuilder.InsertData(
                table: "tb_m_roles",
                columns: new[] { "RoleId", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { "SP-ADM", new DateTime(2021, 10, 8, 19, 4, 49, 537, DateTimeKind.Local).AddTicks(3010), "Super Administrator", new DateTime(2021, 10, 8, 19, 4, 49, 538, DateTimeKind.Local).AddTicks(7998) },
                    { "ADM", new DateTime(2021, 10, 8, 19, 4, 49, 538, DateTimeKind.Local).AddTicks(8592), "Administrator", new DateTime(2021, 10, 8, 19, 4, 49, 538, DateTimeKind.Local).AddTicks(8599) }
                });

            migrationBuilder.InsertData(
                table: "tb_m_statuses",
                columns: new[] { "StatusId", "CreatedAt", "Name", "TypeStatusId", "UpdatedAt" },
                values: new object[,]
                {
                    { "ITV-WD", new DateTime(2021, 10, 8, 19, 4, 49, 885, DateTimeKind.Local).AddTicks(8819), "Waiting Date", null, new DateTime(2021, 10, 8, 19, 4, 49, 885, DateTimeKind.Local).AddTicks(9114) },
                    { "ITV-OG", new DateTime(2021, 10, 8, 19, 4, 49, 885, DateTimeKind.Local).AddTicks(9625), "On Going", null, new DateTime(2021, 10, 8, 19, 4, 49, 885, DateTimeKind.Local).AddTicks(9631) },
                    { "ITV-DN", new DateTime(2021, 10, 8, 19, 4, 49, 885, DateTimeKind.Local).AddTicks(9633), "Done", null, new DateTime(2021, 10, 8, 19, 4, 49, 885, DateTimeKind.Local).AddTicks(9634) },
                    { "ITV-CN", new DateTime(2021, 10, 8, 19, 4, 49, 885, DateTimeKind.Local).AddTicks(9636), "Cancel", null, new DateTime(2021, 10, 8, 19, 4, 49, 885, DateTimeKind.Local).AddTicks(9637) },
                    { "ONB-OG", new DateTime(2021, 10, 8, 19, 4, 49, 885, DateTimeKind.Local).AddTicks(9639), "On Going", null, new DateTime(2021, 10, 8, 19, 4, 49, 885, DateTimeKind.Local).AddTicks(9640) },
                    { "ONB-DN", new DateTime(2021, 10, 8, 19, 4, 49, 885, DateTimeKind.Local).AddTicks(9641), "Done", null, new DateTime(2021, 10, 8, 19, 4, 49, 885, DateTimeKind.Local).AddTicks(9642) }
                });

            migrationBuilder.InsertData(
                table: "tb_m_type_statuses",
                columns: new[] { "TypeStatusId", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { "ITV", new DateTime(2021, 10, 8, 19, 4, 49, 885, DateTimeKind.Local).AddTicks(7165), "Schedule Interview", new DateTime(2021, 10, 8, 19, 4, 49, 885, DateTimeKind.Local).AddTicks(7457) },
                    { "ONB", new DateTime(2021, 10, 8, 19, 4, 49, 885, DateTimeKind.Local).AddTicks(7744), "Onboard Candidate", new DateTime(2021, 10, 8, 19, 4, 49, 885, DateTimeKind.Local).AddTicks(7749) }
                });

            migrationBuilder.InsertData(
                table: "tb_m_account_roles",
                columns: new[] { "AccountId", "RoleId", "CreatedAt", "UpdatedAt" },
                values: new object[] { 1, "SP-ADM", new DateTime(2021, 10, 8, 19, 4, 49, 885, DateTimeKind.Local).AddTicks(5472), new DateTime(2021, 10, 8, 19, 4, 49, 885, DateTimeKind.Local).AddTicks(5773) });

            migrationBuilder.InsertData(
                table: "tb_m_account_roles",
                columns: new[] { "AccountId", "RoleId", "CreatedAt", "UpdatedAt" },
                values: new object[] { 1, "ADM", new DateTime(2021, 10, 8, 19, 4, 49, 885, DateTimeKind.Local).AddTicks(6060), new DateTime(2021, 10, 8, 19, 4, 49, 885, DateTimeKind.Local).AddTicks(6065) });

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_schedule_interview_date_options_ScheduleInterviewId",
                table: "tb_tr_schedule_interview_date_options",
                column: "ScheduleInterviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_schedule_interviews_tb_m_statuses_StatusId",
                table: "tb_tr_schedule_interviews",
                column: "StatusId",
                principalTable: "tb_m_statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_schedule_interviews_tb_m_statuses_StatusId",
                table: "tb_tr_schedule_interviews");

            migrationBuilder.DropTable(
                name: "tb_tr_schedule_interview_date_options");

            migrationBuilder.DeleteData(
                table: "tb_m_account_roles",
                keyColumns: new[] { "AccountId", "RoleId" },
                keyValues: new object[] { 1, "ADM" });

            migrationBuilder.DeleteData(
                table: "tb_m_account_roles",
                keyColumns: new[] { "AccountId", "RoleId" },
                keyValues: new object[] { 1, "SP-ADM" });

            migrationBuilder.DeleteData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-CN");

            migrationBuilder.DeleteData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-DN");

            migrationBuilder.DeleteData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-OG");

            migrationBuilder.DeleteData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-WD");

            migrationBuilder.DeleteData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ONB-DN");

            migrationBuilder.DeleteData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ONB-OG");

            migrationBuilder.DeleteData(
                table: "tb_m_type_statuses",
                keyColumn: "TypeStatusId",
                keyValue: "ITV");

            migrationBuilder.DeleteData(
                table: "tb_m_type_statuses",
                keyColumn: "TypeStatusId",
                keyValue: "ONB");

            migrationBuilder.DeleteData(
                table: "tb_m_accounts",
                keyColumn: "AccountId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "tb_m_roles",
                keyColumn: "RoleId",
                keyValue: "ADM");

            migrationBuilder.DeleteData(
                table: "tb_m_roles",
                keyColumn: "RoleId",
                keyValue: "SP-ADM");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "tb_m_candidates");

            migrationBuilder.AlterColumn<string>(
                name: "StatusId",
                table: "tb_tr_schedule_interviews",
                type: "nvarchar(16)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(16)");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "tb_tr_schedule_interviews",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "JobTitle",
                table: "tb_tr_schedule_interviews",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                table: "tb_tr_schedule_interviews",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "Grade",
                table: "tb_m_candidates",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_schedule_interviews_tb_m_statuses_StatusId",
                table: "tb_tr_schedule_interviews",
                column: "StatusId",
                principalTable: "tb_m_statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
