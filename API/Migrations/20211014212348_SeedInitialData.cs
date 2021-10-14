using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class SeedInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FeedbackMessage",
                table: "tb_tr_schedule_interviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "JobTitle",
                table: "tb_tr_onboards",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "tb_m_account_roles",
                keyColumns: new[] { "AccountId", "RoleId" },
                keyValues: new object[] { 1, "ADM" },
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 4, 23, 47, 231, DateTimeKind.Local).AddTicks(664), new DateTime(2021, 10, 15, 4, 23, 47, 231, DateTimeKind.Local).AddTicks(669) });

            migrationBuilder.UpdateData(
                table: "tb_m_account_roles",
                keyColumns: new[] { "AccountId", "RoleId" },
                keyValues: new object[] { 1, "SP-ADM" },
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 4, 23, 47, 231, DateTimeKind.Local).AddTicks(113), new DateTime(2021, 10, 15, 4, 23, 47, 231, DateTimeKind.Local).AddTicks(395) });

            migrationBuilder.UpdateData(
                table: "tb_m_accounts",
                keyColumn: "AccountId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 4, 23, 47, 230, DateTimeKind.Local).AddTicks(7916), "$2b$12$yOIG5Dd//i5x7YFymLUZHu3pMeZWNTKd.JZB2ooGM/qjo/ENz3hxK", new DateTime(2021, 10, 15, 4, 23, 47, 230, DateTimeKind.Local).AddTicks(8295) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "RoleId",
                keyValue: "ADM",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 4, 23, 46, 883, DateTimeKind.Local).AddTicks(5072), new DateTime(2021, 10, 15, 4, 23, 46, 883, DateTimeKind.Local).AddTicks(5077) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "RoleId",
                keyValue: "SP-ADM",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 4, 23, 46, 882, DateTimeKind.Local).AddTicks(5379), new DateTime(2021, 10, 15, 4, 23, 46, 883, DateTimeKind.Local).AddTicks(4654) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-AC",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 4, 23, 47, 231, DateTimeKind.Local).AddTicks(4068), new DateTime(2021, 10, 15, 4, 23, 47, 231, DateTimeKind.Local).AddTicks(4069) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-CN",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 4, 23, 47, 231, DateTimeKind.Local).AddTicks(4075), new DateTime(2021, 10, 15, 4, 23, 47, 231, DateTimeKind.Local).AddTicks(4076) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-DN",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 4, 23, 47, 231, DateTimeKind.Local).AddTicks(4071), new DateTime(2021, 10, 15, 4, 23, 47, 231, DateTimeKind.Local).AddTicks(4072) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-OG",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 4, 23, 47, 231, DateTimeKind.Local).AddTicks(4065), new DateTime(2021, 10, 15, 4, 23, 47, 231, DateTimeKind.Local).AddTicks(4066) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-WC",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 4, 23, 47, 231, DateTimeKind.Local).AddTicks(4058), new DateTime(2021, 10, 15, 4, 23, 47, 231, DateTimeKind.Local).AddTicks(4063) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-WD",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 4, 23, 47, 231, DateTimeKind.Local).AddTicks(3520), new DateTime(2021, 10, 15, 4, 23, 47, 231, DateTimeKind.Local).AddTicks(3792) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ONB-DN",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 4, 23, 47, 231, DateTimeKind.Local).AddTicks(4081), new DateTime(2021, 10, 15, 4, 23, 47, 231, DateTimeKind.Local).AddTicks(4082) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ONB-OG",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 4, 23, 47, 231, DateTimeKind.Local).AddTicks(4078), new DateTime(2021, 10, 15, 4, 23, 47, 231, DateTimeKind.Local).AddTicks(4079) });

            migrationBuilder.UpdateData(
                table: "tb_m_type_statuses",
                keyColumn: "TypeStatusId",
                keyValue: "ITV",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 4, 23, 47, 231, DateTimeKind.Local).AddTicks(1695), new DateTime(2021, 10, 15, 4, 23, 47, 231, DateTimeKind.Local).AddTicks(1972) });

            migrationBuilder.UpdateData(
                table: "tb_m_type_statuses",
                keyColumn: "TypeStatusId",
                keyValue: "ONB",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 4, 23, 47, 231, DateTimeKind.Local).AddTicks(2240), new DateTime(2021, 10, 15, 4, 23, 47, 231, DateTimeKind.Local).AddTicks(2244) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeedbackMessage",
                table: "tb_tr_schedule_interviews");

            migrationBuilder.AlterColumn<string>(
                name: "JobTitle",
                table: "tb_tr_onboards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "tb_m_account_roles",
                keyColumns: new[] { "AccountId", "RoleId" },
                keyValues: new object[] { 1, "ADM" },
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 5, 11, 23, 836, DateTimeKind.Local).AddTicks(820), new DateTime(2021, 10, 13, 5, 11, 23, 836, DateTimeKind.Local).AddTicks(824) });

            migrationBuilder.UpdateData(
                table: "tb_m_account_roles",
                keyColumns: new[] { "AccountId", "RoleId" },
                keyValues: new object[] { 1, "SP-ADM" },
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 5, 11, 23, 836, DateTimeKind.Local).AddTicks(214), new DateTime(2021, 10, 13, 5, 11, 23, 836, DateTimeKind.Local).AddTicks(547) });

            migrationBuilder.UpdateData(
                table: "tb_m_accounts",
                keyColumn: "AccountId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 5, 11, 23, 835, DateTimeKind.Local).AddTicks(8001), "$2b$12$Ray2VSHraU9HmxUxAB4yBuehMwbySP05lHE5914jtCRIDl/qoZV1O", new DateTime(2021, 10, 13, 5, 11, 23, 835, DateTimeKind.Local).AddTicks(8364) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "RoleId",
                keyValue: "ADM",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 5, 11, 23, 464, DateTimeKind.Local).AddTicks(1207), new DateTime(2021, 10, 13, 5, 11, 23, 464, DateTimeKind.Local).AddTicks(1213) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "RoleId",
                keyValue: "SP-ADM",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 5, 11, 23, 462, DateTimeKind.Local).AddTicks(2993), new DateTime(2021, 10, 13, 5, 11, 23, 464, DateTimeKind.Local).AddTicks(793) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-AC",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 5, 11, 23, 836, DateTimeKind.Local).AddTicks(4211), new DateTime(2021, 10, 13, 5, 11, 23, 836, DateTimeKind.Local).AddTicks(4212) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-CN",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 5, 11, 23, 836, DateTimeKind.Local).AddTicks(4216), new DateTime(2021, 10, 13, 5, 11, 23, 836, DateTimeKind.Local).AddTicks(4217) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-DN",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 5, 11, 23, 836, DateTimeKind.Local).AddTicks(4213), new DateTime(2021, 10, 13, 5, 11, 23, 836, DateTimeKind.Local).AddTicks(4214) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-OG",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 5, 11, 23, 836, DateTimeKind.Local).AddTicks(4208), new DateTime(2021, 10, 13, 5, 11, 23, 836, DateTimeKind.Local).AddTicks(4209) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-WC",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 5, 11, 23, 836, DateTimeKind.Local).AddTicks(4202), new DateTime(2021, 10, 13, 5, 11, 23, 836, DateTimeKind.Local).AddTicks(4206) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-WD",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 5, 11, 23, 836, DateTimeKind.Local).AddTicks(3603), new DateTime(2021, 10, 13, 5, 11, 23, 836, DateTimeKind.Local).AddTicks(3930) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ONB-DN",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 5, 11, 23, 836, DateTimeKind.Local).AddTicks(4221), new DateTime(2021, 10, 13, 5, 11, 23, 836, DateTimeKind.Local).AddTicks(4222) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ONB-OG",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 5, 11, 23, 836, DateTimeKind.Local).AddTicks(4219), new DateTime(2021, 10, 13, 5, 11, 23, 836, DateTimeKind.Local).AddTicks(4220) });

            migrationBuilder.UpdateData(
                table: "tb_m_type_statuses",
                keyColumn: "TypeStatusId",
                keyValue: "ITV",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 5, 11, 23, 836, DateTimeKind.Local).AddTicks(1839), new DateTime(2021, 10, 13, 5, 11, 23, 836, DateTimeKind.Local).AddTicks(2111) });

            migrationBuilder.UpdateData(
                table: "tb_m_type_statuses",
                keyColumn: "TypeStatusId",
                keyValue: "ONB",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 5, 11, 23, 836, DateTimeKind.Local).AddTicks(2376), new DateTime(2021, 10, 13, 5, 11, 23, 836, DateTimeKind.Local).AddTicks(2381) });
        }
    }
}
