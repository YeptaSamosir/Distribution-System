using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class migrations3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_detail_schedule_interviews_tb_tr_schedule_interviews_ScheduleInterviewId",
                table: "tb_m_detail_schedule_interviews");

            migrationBuilder.DeleteData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-WT");

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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "tb_m_account_roles",
                keyColumns: new[] { "AccountId", "RoleId" },
                keyValues: new object[] { 1, "ADM" },
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 11, 10, 10, 35, 398, DateTimeKind.Local).AddTicks(119), new DateTime(2021, 10, 11, 10, 10, 35, 398, DateTimeKind.Local).AddTicks(132) });

            migrationBuilder.UpdateData(
                table: "tb_m_account_roles",
                keyColumns: new[] { "AccountId", "RoleId" },
                keyValues: new object[] { 1, "SP-ADM" },
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 11, 10, 10, 35, 397, DateTimeKind.Local).AddTicks(8729), new DateTime(2021, 10, 11, 10, 10, 35, 397, DateTimeKind.Local).AddTicks(9397) });

            migrationBuilder.UpdateData(
                table: "tb_m_accounts",
                keyColumn: "AccountId",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Password", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 11, 10, 10, 35, 397, DateTimeKind.Local).AddTicks(4712), "$2b$12$D3dsiA8u2R82jsvuF.VSFemUN7446AvZZMOh5YqXVezo88Ms/cxFG", new DateTime(2021, 10, 11, 10, 10, 35, 397, DateTimeKind.Local).AddTicks(5363) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "RoleId",
                keyValue: "ADM",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 11, 10, 10, 34, 756, DateTimeKind.Local).AddTicks(9908), new DateTime(2021, 10, 11, 10, 10, 34, 756, DateTimeKind.Local).AddTicks(9915) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "RoleId",
                keyValue: "SP-ADM",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 11, 10, 10, 34, 755, DateTimeKind.Local).AddTicks(1740), new DateTime(2021, 10, 11, 10, 10, 34, 756, DateTimeKind.Local).AddTicks(9228) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-CN",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 11, 10, 10, 35, 398, DateTimeKind.Local).AddTicks(7088), new DateTime(2021, 10, 11, 10, 10, 35, 398, DateTimeKind.Local).AddTicks(7090) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-DN",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 11, 10, 10, 35, 398, DateTimeKind.Local).AddTicks(7083), new DateTime(2021, 10, 11, 10, 10, 35, 398, DateTimeKind.Local).AddTicks(7086) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ONB-DN",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 11, 10, 10, 35, 398, DateTimeKind.Local).AddTicks(7098), new DateTime(2021, 10, 11, 10, 10, 35, 398, DateTimeKind.Local).AddTicks(7100) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ONB-OG",
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 11, 10, 10, 35, 398, DateTimeKind.Local).AddTicks(7094), "On Going", new DateTime(2021, 10, 11, 10, 10, 35, 398, DateTimeKind.Local).AddTicks(7096) });

            migrationBuilder.InsertData(
                table: "tb_m_statuses",
                columns: new[] { "StatusId", "CreatedAt", "Name", "TypeStatusId", "UpdatedAt" },
                values: new object[,]
                {
                    { "ITV-WD", new DateTime(2021, 10, 11, 10, 10, 35, 398, DateTimeKind.Local).AddTicks(5816), "Waiting Date", null, new DateTime(2021, 10, 11, 10, 10, 35, 398, DateTimeKind.Local).AddTicks(6429) },
                    { "ITV-OG", new DateTime(2021, 10, 11, 10, 10, 35, 398, DateTimeKind.Local).AddTicks(7071), "On Going", null, new DateTime(2021, 10, 11, 10, 10, 35, 398, DateTimeKind.Local).AddTicks(7080) }
                });

            migrationBuilder.UpdateData(
                table: "tb_m_type_statuses",
                keyColumn: "TypeStatusId",
                keyValue: "ITV",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 11, 10, 10, 35, 398, DateTimeKind.Local).AddTicks(2358), new DateTime(2021, 10, 11, 10, 10, 35, 398, DateTimeKind.Local).AddTicks(2961) });

            migrationBuilder.UpdateData(
                table: "tb_m_type_statuses",
                keyColumn: "TypeStatusId",
                keyValue: "ONB",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 11, 10, 10, 35, 398, DateTimeKind.Local).AddTicks(3540), new DateTime(2021, 10, 11, 10, 10, 35, 398, DateTimeKind.Local).AddTicks(3549) });

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_schedule_interview_date_options_ScheduleInterviewId",
                table: "tb_tr_schedule_interview_date_options",
                column: "ScheduleInterviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_detail_schedule_interviews_tb_tr_schedule_interviews_ScheduleInterviewId",
                table: "tb_m_detail_schedule_interviews",
                column: "ScheduleInterviewId",
                principalTable: "tb_tr_schedule_interviews",
                principalColumn: "ScheduleInterviewId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_detail_schedule_interviews_tb_tr_schedule_interviews_ScheduleInterviewId",
                table: "tb_m_detail_schedule_interviews");

            migrationBuilder.DropTable(
                name: "tb_tr_schedule_interview_date_options");

            migrationBuilder.DeleteData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-OG");

            migrationBuilder.DeleteData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ITV-WD");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "tb_m_candidates");

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
                keyValue: "ONB-DN",
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(3554), new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(3556) });

            migrationBuilder.UpdateData(
                table: "tb_m_statuses",
                keyColumn: "StatusId",
                keyValue: "ONB-OG",
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(3550), "Cancel", new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(3552) });

            migrationBuilder.InsertData(
                table: "tb_m_statuses",
                columns: new[] { "StatusId", "CreatedAt", "Name", "TypeStatusId", "UpdatedAt" },
                values: new object[] { "ITV-WT", new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(2181), "Waiting", null, new DateTime(2021, 10, 10, 11, 34, 51, 350, DateTimeKind.Local).AddTicks(2725) });

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

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_detail_schedule_interviews_tb_tr_schedule_interviews_ScheduleInterviewId",
                table: "tb_m_detail_schedule_interviews",
                column: "ScheduleInterviewId",
                principalTable: "tb_tr_schedule_interviews",
                principalColumn: "ScheduleInterviewId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
