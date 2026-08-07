using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeoClinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeAppointmentRelationshipWithAvailability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Availabilities_AvailabilityId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_AvailabilityId",
                table: "Appointments");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AvailabilityId",
                table: "Appointments",
                column: "AvailabilityId",
                unique: true,
                filter: "[Status] <> 3 AND [Status] <> 4");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Availabilities_AvailabilityId",
                table: "Appointments",
                column: "AvailabilityId",
                principalTable: "Availabilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Availabilities_AvailabilityId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_AvailabilityId",
                table: "Appointments");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AvailabilityId",
                table: "Appointments",
                column: "AvailabilityId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Availabilities_AvailabilityId",
                table: "Appointments",
                column: "AvailabilityId",
                principalTable: "Availabilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
