using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class newMigrationSeedInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "tb_m_account_roles",
                keyColumns: new[] { "AccountId", "RoleId" },
                keyValues: new object[] { 1, "ADM" },
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(3521), new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(3527) });

            migrationBuilder.UpdateData(
                table: "tb_m_account_roles",
                keyColumns: new[] { "AccountId", "RoleId" },
                keyValues: new object[] { 1, "SP-ADM" },
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(2623), new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(3091) });

            migrationBuilder.UpdateData(
                table: "tb_m_accounts",
                keyColumn: "AccountId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 8, 42, 24, 443, DateTimeKind.Local).AddTicks(9538), "$2b$12$iwsge4opNQpo3oYrmSw6pe5dkNKZFDJvjLhTlbP4dPuwPIryHR6UC", new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(101) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "RoleId",
                keyValue: "ADM",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 8, 42, 23, 970, DateTimeKind.Local).AddTicks(5132), new DateTime(2021, 10, 13, 8, 42, 23, 970, DateTimeKind.Local).AddTicks(5140) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "RoleId",
                keyValue: "SP-ADM",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 8, 42, 23, 969, DateTimeKind.Local).AddTicks(2210), new DateTime(2021, 10, 13, 8, 42, 23, 970, DateTimeKind.Local).AddTicks(4368) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-AC",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9433), new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9435) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-CN",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9441), new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9443) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-DN",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9437), new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9439) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-OG",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9428), new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9430) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-WC",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9419), new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9425) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-WD",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(8449), new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(8942) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ONB-DN",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9450), new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9451) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ONB-OG",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9445), new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(9447) });

            migrationBuilder.UpdateData(
                table: "tb_m_type_statuses",
                keyColumn: "TypeStatusId",
                keyValue: "ITV",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(5296), new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(5804) });

            migrationBuilder.UpdateData(
                table: "tb_m_type_statuses",
                keyColumn: "TypeStatusId",
                keyValue: "ONB",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(6293), new DateTime(2021, 10, 13, 8, 42, 24, 444, DateTimeKind.Local).AddTicks(6300) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
