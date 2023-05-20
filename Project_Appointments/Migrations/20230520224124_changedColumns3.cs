using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Appointments.Migrations
{
    /// <inheritdoc />
    public partial class changedColumns3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BreakTimes_ScheduleId",
                table: "BreakTimes",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ScheduleId",
                table: "Appointments",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Schedules_ScheduleId",
                table: "Appointments",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BreakTimes_Schedules_ScheduleId",
                table: "BreakTimes",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Schedules_ScheduleId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_BreakTimes_Schedules_ScheduleId",
                table: "BreakTimes");

            migrationBuilder.DropIndex(
                name: "IX_BreakTimes_ScheduleId",
                table: "BreakTimes");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ScheduleId",
                table: "Appointments");
        }
    }
}
