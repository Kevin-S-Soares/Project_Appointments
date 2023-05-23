using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project_Appointments.Migrations
{
    /// <inheritdoc />
    public partial class setup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Odontologists",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odontologists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    VerificationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetTokenExpires = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OdontologistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OdontologistId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDay = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndDay = table.Column<int>(type: "int", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Odontologists_OdontologistId",
                        column: x => x.OdontologistId,
                        principalTable: "Odontologists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleId = table.Column<long>(type: "bigint", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BreakTimes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDay = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndDay = table.Column<int>(type: "int", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreakTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BreakTimes_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Odontologists",
                columns: new[] { "Id", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1L, "john@blue.com", "John Blue", "(011) 91111-1111" },
                    { 2L, "maria@yellow.com", "Maria Yellow", "(011) 92222-2222" }
                });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "Id", "EndDay", "EndTime", "Name", "OdontologistId", "StartDay", "StartTime" },
                values: new object[,]
                {
                    { 1L, 1, new TimeSpan(0, 18, 0, 0, 0), "Monday", 1L, 1, new TimeSpan(0, 9, 0, 0, 0) },
                    { 2L, 2, new TimeSpan(0, 18, 0, 0, 0), "Tuesday", 1L, 2, new TimeSpan(0, 9, 0, 0, 0) },
                    { 3L, 4, new TimeSpan(0, 6, 0, 0, 0), "Wednesday-Thursday", 2L, 3, new TimeSpan(0, 21, 0, 0, 0) },
                    { 4L, 5, new TimeSpan(0, 6, 0, 0, 0), "Thursday-Friday", 2L, 4, new TimeSpan(0, 21, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "Description", "End", "PatientName", "ScheduleId", "Start" },
                values: new object[,]
                {
                    { 1L, "Cleaning", new DateTime(2023, 1, 2, 9, 15, 0, 0, DateTimeKind.Unspecified), "Bob Brown", 1L, new DateTime(2023, 1, 2, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2L, "Cleaning", new DateTime(2023, 1, 3, 9, 15, 0, 0, DateTimeKind.Unspecified), "Anna Red", 2L, new DateTime(2023, 1, 3, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3L, "Treatment", new DateTime(2023, 1, 4, 21, 15, 0, 0, DateTimeKind.Unspecified), "Peter Green", 3L, new DateTime(2023, 1, 4, 21, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4L, "Treatment", new DateTime(2023, 1, 5, 21, 15, 0, 0, DateTimeKind.Unspecified), "Lara White", 4L, new DateTime(2023, 1, 5, 21, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "BreakTimes",
                columns: new[] { "Id", "EndDay", "EndTime", "Name", "ScheduleId", "StartDay", "StartTime" },
                values: new object[,]
                {
                    { 1L, 1, new TimeSpan(0, 13, 0, 0, 0), "Lunch", 1L, 1, new TimeSpan(0, 12, 0, 0, 0) },
                    { 2L, 2, new TimeSpan(0, 13, 0, 0, 0), "Lunch", 2L, 2, new TimeSpan(0, 12, 0, 0, 0) },
                    { 3L, 4, new TimeSpan(0, 1, 0, 0, 0), "Dinner", 3L, 4, new TimeSpan(0, 0, 0, 0, 0) },
                    { 4L, 5, new TimeSpan(0, 1, 0, 0, 0), "Dinner", 4L, 5, new TimeSpan(0, 0, 0, 0, 0) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ScheduleId",
                table: "Appointments",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_BreakTimes_ScheduleId",
                table: "BreakTimes",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_OdontologistId",
                table: "Schedules",
                column: "OdontologistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "BreakTimes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Odontologists");
        }
    }
}
