using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Appointments.Migrations
{
    /// <inheritdoc />
    public partial class changedColumns2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Schedules_OdontologistId",
                table: "Schedules",
                column: "OdontologistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Odontologists_OdontologistId",
                table: "Schedules",
                column: "OdontologistId",
                principalTable: "Odontologists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Odontologists_OdontologistId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_OdontologistId",
                table: "Schedules");
        }
    }
}
