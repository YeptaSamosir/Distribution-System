using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class migrations3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Grade",
                table: "tb_m_candidates",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.UpdateData(
                table: "tb_m_account_roles",
                keyColumns: new[] { "AccountId", "RoleId" },
                keyValues: new object[] { 1, "ADM" },
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 10, 14, 36, 26, 240, DateTimeKind.Local).AddTicks(4245), new DateTime(2021, 10, 10, 14, 36, 26, 240, DateTimeKind.Local).AddTicks(4255) });

            migrationBuilder.UpdateData(
                table: "tb_m_account_roles",
                keyColumns: new[] { "AccountId", "RoleId" },
                keyValues: new object[] { 1, "SP-ADM" },
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 10, 14, 36, 26, 240, DateTimeKind.Local).AddTicks(2854), new DateTime(2021, 10, 10, 14, 36, 26, 240, DateTimeKind.Local).AddTicks(3563) });

            migrationBuilder.UpdateData(
                table: "tb_m_accounts",
                keyColumn: "AccountId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 10, 14, 36, 26, 239, DateTimeKind.Local).AddTicks(8422), "$2b$12$zSGRoszXExgF5O6Y652k2uHi1ZkgUBHWwnS.0RlzLzdaqPeQogTla", new DateTime(2021, 10, 10, 14, 36, 26, 239, DateTimeKind.Local).AddTicks(9226) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "RoleId",
                keyValue: "ADM",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 10, 14, 36, 25, 612, DateTimeKind.Local).AddTicks(7139), new DateTime(2021, 10, 10, 14, 36, 25, 612, DateTimeKind.Local).AddTicks(7146) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "RoleId",
                keyValue: "SP-ADM",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 10, 14, 36, 25, 611, DateTimeKind.Local).AddTicks(5902), new DateTime(2021, 10, 10, 14, 36, 25, 612, DateTimeKind.Local).AddTicks(6534) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-CN",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 10, 14, 36, 26, 241, DateTimeKind.Local).AddTicks(2171), new DateTime(2021, 10, 10, 14, 36, 26, 241, DateTimeKind.Local).AddTicks(2174) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-DN",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 10, 14, 36, 26, 241, DateTimeKind.Local).AddTicks(2158), new DateTime(2021, 10, 10, 14, 36, 26, 241, DateTimeKind.Local).AddTicks(2167) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-WT",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 10, 14, 36, 26, 241, DateTimeKind.Local).AddTicks(750), new DateTime(2021, 10, 10, 14, 36, 26, 241, DateTimeKind.Local).AddTicks(1393) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ONB-DN",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 10, 14, 36, 26, 241, DateTimeKind.Local).AddTicks(2182), new DateTime(2021, 10, 10, 14, 36, 26, 241, DateTimeKind.Local).AddTicks(2184) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ONB-OG",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 10, 14, 36, 26, 241, DateTimeKind.Local).AddTicks(2177), new DateTime(2021, 10, 10, 14, 36, 26, 241, DateTimeKind.Local).AddTicks(2179) });

            migrationBuilder.UpdateData(
                table: "tb_m_type_statuses",
                keyColumn: "TypeStatusId",
                keyValue: "ITV",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 10, 14, 36, 26, 240, DateTimeKind.Local).AddTicks(6844), new DateTime(2021, 10, 10, 14, 36, 26, 240, DateTimeKind.Local).AddTicks(7512) });

            migrationBuilder.UpdateData(
                table: "tb_m_type_statuses",
                keyColumn: "TypeStatusId",
                keyValue: "ONB",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 10, 14, 36, 26, 240, DateTimeKind.Local).AddTicks(8358), new DateTime(2021, 10, 10, 14, 36, 26, 240, DateTimeKind.Local).AddTicks(8367) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Grade",
                table: "tb_m_candidates",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);

            migrationBuilder.UpdateData(
                table: "tb_m_account_roles",
                keyColumns: new[] { "AccountId", "RoleId" },
                keyValues: new object[] { 1, "ADM" },
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 10, 11, 34, 51, 349, DateTimeKind.Local).AddTicks(6470), new DateTime(2021, 10, 10, 11, 34, 51, 349, DateTimeKind.Local).AddTicks(6479) });

            migrationBuilder.UpdateData(
                table: "tb_m_account_roles",
                keyColumns: new[] { "AccountId", "RoleId" },
                keyValues: new object[] { 1, "SP-ADM" },
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 10, 11, 34, 51, 349, DateTimeKind.Local).AddTicks(5173), new DateTime(2021, 10, 10, 11, 34, 51, 349, DateTimeKind.Local).AddTicks(5803) });

            migrationBuilder.UpdateData(
                table: "tb_m_accounts",
                keyColumn: "AccountId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 10, 11, 34, 51, 349, DateTimeKind.Local).AddTicks(988), "$2b$12$m3KPGs3rD25Qatbz81zAleOYkHea4ZdbweM5t3U8eG05tQkmxQC4m", new DateTime(2021, 10, 10, 11, 34, 51, 349, DateTimeKind.Local).AddTicks(1719) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "RoleId",
                keyValue: "ADM",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 10, 11, 34, 50, 656, DateTimeKind.Local).AddTicks(8420), new DateTime(2021, 10, 10, 11, 34, 50, 656, DateTimeKind.Local).AddTicks(8426) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "RoleId",
                keyValue: "SP-ADM",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 10, 11, 34, 50, 654, DateTimeKind.Local).AddTicks(6566), new DateTime(2021, 10, 10, 11, 34, 50, 656, DateTimeKind.Local).AddTicks(7960) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-CN",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(3544), new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(3547) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-DN",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(3533), new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(3540) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-WT",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(2181), new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(2725) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ONB-DN",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(3554), new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(3556) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ONB-OG",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(3550), new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(3552) });

            migrationBuilder.UpdateData(
                table: "tb_m_type_statuses",
                keyColumn: "TypeStatusId",
                keyValue: "ITV",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 10, 11, 34, 51, 349, DateTimeKind.Local).AddTicks(8869), new DateTime(2021, 10, 10, 11, 34, 51, 349, DateTimeKind.Local).AddTicks(9462) });

            migrationBuilder.UpdateData(
                table: "tb_m_type_statuses",
                keyColumn: "TypeStatusId",
                keyValue: "ONB",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(96), new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(104) });
        }
    }
}
