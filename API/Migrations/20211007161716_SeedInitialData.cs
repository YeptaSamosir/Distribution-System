using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class SeedInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tb_m_accounts",
                columns: new[] { "AccountId", "AttemptCount", "CreatedAt", "Email", "IsActive", "Name", "Password", "UpdatedAt", "Username" },
                values: new object[] { 1, 0, new DateTime(2021, 10, 7, 23, 17, 15, 760, DateTimeKind.Local).AddTicks(4312), "admin@mail.com", true, "Super Administrator", "$2b$12$clsbfSGoIQc9iGyClefiT.wC8taXQBZ4JqlS46vAHACLTbF6/rBd6", new DateTime(2021, 10, 7, 23, 17, 15, 760, DateTimeKind.Local).AddTicks(4687), "Admin" });

            migrationBuilder.InsertData(
                table: "tb_m_roles",
                columns: new[] { "RoleId", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { "SP-ADM", new DateTime(2021, 10, 7, 23, 17, 15, 411, DateTimeKind.Local).AddTicks(1205), "Super Administrator", new DateTime(2021, 10, 7, 23, 17, 15, 412, DateTimeKind.Local).AddTicks(357) },
                    { "ADM", new DateTime(2021, 10, 7, 23, 17, 15, 412, DateTimeKind.Local).AddTicks(739), "Administrator", new DateTime(2021, 10, 7, 23, 17, 15, 412, DateTimeKind.Local).AddTicks(744) }
                });

            migrationBuilder.InsertData(
                table: "tb_m_statuses",
                columns: new[] { "StatusId", "CreatedAt", "Name", "TypeStatusId", "UpdatedAt" },
                values: new object[,]
                {
                    { "ITV-WT", new DateTime(2021, 10, 7, 23, 17, 15, 760, DateTimeKind.Local).AddTicks(9760), "Waiting", null, new DateTime(2021, 10, 7, 23, 17, 15, 761, DateTimeKind.Local).AddTicks(50) },
                    { "ITV-DN", new DateTime(2021, 10, 7, 23, 17, 15, 761, DateTimeKind.Local).AddTicks(424), "Done", null, new DateTime(2021, 10, 7, 23, 17, 15, 761, DateTimeKind.Local).AddTicks(429) },
                    { "ITV-CN", new DateTime(2021, 10, 7, 23, 17, 15, 761, DateTimeKind.Local).AddTicks(431), "Cancel", null, new DateTime(2021, 10, 7, 23, 17, 15, 761, DateTimeKind.Local).AddTicks(432) },
                    { "ONB-OG", new DateTime(2021, 10, 7, 23, 17, 15, 761, DateTimeKind.Local).AddTicks(434), "Cancel", null, new DateTime(2021, 10, 7, 23, 17, 15, 761, DateTimeKind.Local).AddTicks(435) },
                    { "ONB-DN", new DateTime(2021, 10, 7, 23, 17, 15, 761, DateTimeKind.Local).AddTicks(437), "Done", null, new DateTime(2021, 10, 7, 23, 17, 15, 761, DateTimeKind.Local).AddTicks(438) }
                });

            migrationBuilder.InsertData(
                table: "tb_m_type_statuses",
                columns: new[] { "TypeStatusId", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { "ITV", new DateTime(2021, 10, 7, 23, 17, 15, 760, DateTimeKind.Local).AddTicks(8145), "Schedule Interview", new DateTime(2021, 10, 7, 23, 17, 15, 760, DateTimeKind.Local).AddTicks(8439) },
                    { "ONB", new DateTime(2021, 10, 7, 23, 17, 15, 760, DateTimeKind.Local).AddTicks(8722), "Onboard Candidate", new DateTime(2021, 10, 7, 23, 17, 15, 760, DateTimeKind.Local).AddTicks(8726) }
                });

            migrationBuilder.InsertData(
                table: "tb_m_account_roles",
                columns: new[] { "AccountId", "RoleId", "CreatedAt", "UpdatedAt" },
                values: new object[] { 1, "SP-ADM", new DateTime(2021, 10, 7, 23, 17, 15, 760, DateTimeKind.Local).AddTicks(6496), new DateTime(2021, 10, 7, 23, 17, 15, 760, DateTimeKind.Local).AddTicks(6801) });

            migrationBuilder.InsertData(
                table: "tb_m_account_roles",
                columns: new[] { "AccountId", "RoleId", "CreatedAt", "UpdatedAt" },
                values: new object[] { 1, "ADM", new DateTime(2021, 10, 7, 23, 17, 15, 760, DateTimeKind.Local).AddTicks(7090), new DateTime(2021, 10, 7, 23, 17, 15, 760, DateTimeKind.Local).AddTicks(7095) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
