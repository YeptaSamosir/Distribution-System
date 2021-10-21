using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class newMigrations : Migration
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
                values: new object[] { new DateTime(2021, 10, 15, 9, 2, 54, 404, DateTimeKind.Local).AddTicks(2271), new DateTime(2021, 10, 15, 9, 2, 54, 404, DateTimeKind.Local).AddTicks(2276) });

            migrationBuilder.UpdateData(
                table: "tb_m_account_roles",
                keyColumns: new[] { "AccountId", "RoleId" },
                keyValues: new object[] { 1, "SP-ADM" },
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 9, 2, 54, 404, DateTimeKind.Local).AddTicks(1289), new DateTime(2021, 10, 15, 9, 2, 54, 404, DateTimeKind.Local).AddTicks(1833) });

            migrationBuilder.UpdateData(
                table: "tb_m_accounts",
                keyColumn: "AccountId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 9, 2, 54, 403, DateTimeKind.Local).AddTicks(8113), "$2b$12$fgTUpQuDQ.mRhKlNRNvgpeBgITih1kcpvUcvtkA4xy3dcElNnK2pa", new DateTime(2021, 10, 15, 9, 2, 54, 403, DateTimeKind.Local).AddTicks(8638) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "RoleId",
                keyValue: "ADM",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 9, 2, 53, 846, DateTimeKind.Local).AddTicks(6302), new DateTime(2021, 10, 15, 9, 2, 53, 846, DateTimeKind.Local).AddTicks(6312) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "RoleId",
                keyValue: "SP-ADM",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 9, 2, 53, 845, DateTimeKind.Local).AddTicks(928), new DateTime(2021, 10, 15, 9, 2, 53, 846, DateTimeKind.Local).AddTicks(5575) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-AC",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 9, 2, 54, 404, DateTimeKind.Local).AddTicks(7475), new DateTime(2021, 10, 15, 9, 2, 54, 404, DateTimeKind.Local).AddTicks(7477) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-CN",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 9, 2, 54, 404, DateTimeKind.Local).AddTicks(7485), new DateTime(2021, 10, 15, 9, 2, 54, 404, DateTimeKind.Local).AddTicks(7487) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-DN",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 9, 2, 54, 404, DateTimeKind.Local).AddTicks(7479), new DateTime(2021, 10, 15, 9, 2, 54, 404, DateTimeKind.Local).AddTicks(7481) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-OG",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 9, 2, 54, 404, DateTimeKind.Local).AddTicks(7468), new DateTime(2021, 10, 15, 9, 2, 54, 404, DateTimeKind.Local).AddTicks(7470) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-WC",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 9, 2, 54, 404, DateTimeKind.Local).AddTicks(7461), new DateTime(2021, 10, 15, 9, 2, 54, 404, DateTimeKind.Local).AddTicks(7465) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-WD",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 9, 2, 54, 404, DateTimeKind.Local).AddTicks(6625), new DateTime(2021, 10, 15, 9, 2, 54, 404, DateTimeKind.Local).AddTicks(7051) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ONB-DN",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 9, 2, 54, 404, DateTimeKind.Local).AddTicks(7493), new DateTime(2021, 10, 15, 9, 2, 54, 404, DateTimeKind.Local).AddTicks(7495) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ONB-OG",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 9, 2, 54, 404, DateTimeKind.Local).AddTicks(7489), new DateTime(2021, 10, 15, 9, 2, 54, 404, DateTimeKind.Local).AddTicks(7491) });

            migrationBuilder.UpdateData(
                table: "tb_m_type_statuses",
                keyColumn: "TypeStatusId",
                keyValue: "ITV",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 9, 2, 54, 404, DateTimeKind.Local).AddTicks(3934), new DateTime(2021, 10, 15, 9, 2, 54, 404, DateTimeKind.Local).AddTicks(4365) });

            migrationBuilder.UpdateData(
                table: "tb_m_type_statuses",
                keyColumn: "TypeStatusId",
                keyValue: "ONB",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 15, 9, 2, 54, 404, DateTimeKind.Local).AddTicks(4769), new DateTime(2021, 10, 15, 9, 2, 54, 404, DateTimeKind.Local).AddTicks(4774) });
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
