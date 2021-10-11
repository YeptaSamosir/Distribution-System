using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class migrations2 : Migration
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

            migrationBuilder.InsertData(
                table: "tb_m_accounts",
                columns: new[] { "AccountId", "AttemptCount", "CreatedAt", "Email", "IsActive", "Name", "Password", "UpdatedAt", "Username" },
                values: new object[] { 1, 0, new DateTime(2021, 10, 10, 11, 34, 51, 349, DateTimeKind.Local).AddTicks(988), "admin@mail.com", true, "Super Administrator", "$2b$12$m3KPGs3rD25Qatbz81zAleOYkHea4ZdbweM5t3U8eG05tQkmxQC4m", new DateTime(2021, 10, 10, 11, 34, 51, 349, DateTimeKind.Local).AddTicks(1719), "Admin" });

            migrationBuilder.InsertData(
                table: "tb_m_roles",
                columns: new[] { "RoleId", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { "SP-ADM", new DateTime(2021, 10, 10, 11, 34, 50, 654, DateTimeKind.Local).AddTicks(6566), "Super Administrator", new DateTime(2021, 10, 10, 11, 34, 50, 656, DateTimeKind.Local).AddTicks(7960) },
                    { "ADM", new DateTime(2021, 10, 10, 11, 34, 50, 656, DateTimeKind.Local).AddTicks(8420), "Administrator", new DateTime(2021, 10, 10, 11, 34, 50, 656, DateTimeKind.Local).AddTicks(8426) }
                });

            migrationBuilder.InsertData(
                table: "tb_m_statuses",
                columns: new[] { "StatusId", "CreatedAt", "Name", "TypeStatusId", "UpdatedAt" },
                values: new object[,]
                {
                    { "ITV-WT", new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(2181), "Waiting", null, new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(2725) },
                    { "ITV-DN", new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(3533), "Done", null, new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(3540) },
                    { "ITV-CN", new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(3544), "Cancel", null, new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(3547) },
                    { "ONB-OG", new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(3550), "Cancel", null, new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(3552) },
                    { "ONB-DN", new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(3554), "Done", null, new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(3556) }
                });

            migrationBuilder.InsertData(
                table: "tb_m_type_statuses",
                columns: new[] { "TypeStatusId", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { "ITV", new DateTime(2021, 10, 10, 11, 34, 51, 349, DateTimeKind.Local).AddTicks(8869), "Schedule Interview", new DateTime(2021, 10, 10, 11, 34, 51, 349, DateTimeKind.Local).AddTicks(9462) },
                    { "ONB", new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(96), "Onboard Candidate", new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(104) }
                });

            migrationBuilder.InsertData(
                table: "tb_m_account_roles",
                columns: new[] { "AccountId", "RoleId", "CreatedAt", "UpdatedAt" },
                values: new object[] { 1, "SP-ADM", new DateTime(2021, 10, 10, 11, 34, 51, 349, DateTimeKind.Local).AddTicks(5173), new DateTime(2021, 10, 10, 11, 34, 51, 349, DateTimeKind.Local).AddTicks(5803) });

            migrationBuilder.InsertData(
                table: "tb_m_account_roles",
                columns: new[] { "AccountId", "RoleId", "CreatedAt", "UpdatedAt" },
                values: new object[] { 1, "ADM", new DateTime(2021, 10, 10, 11, 34, 51, 349, DateTimeKind.Local).AddTicks(6470), new DateTime(2021, 10, 10, 11, 34, 51, 349, DateTimeKind.Local).AddTicks(6479) });

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
                keyValue: "ITV-WT");

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
