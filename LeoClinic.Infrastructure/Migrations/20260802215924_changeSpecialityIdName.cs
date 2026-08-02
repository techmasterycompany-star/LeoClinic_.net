using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeoClinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeSpecialityIdName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorProfiles_Specialties_SpecialityId",
                table: "DoctorProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorProfiles_Specialties_SpecialtyId",
                table: "DoctorProfiles");

            migrationBuilder.DropIndex(
                name: "IX_DoctorProfiles_SpecialtyId",
                table: "DoctorProfiles");

            migrationBuilder.DropColumn(
                name: "SpecialtyId",
                table: "DoctorProfiles");

            migrationBuilder.AlterColumn<int>(
                name: "SpecialityId",
                table: "DoctorProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpecialityId1",
                table: "DoctorProfiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorProfiles_SpecialityId1",
                table: "DoctorProfiles",
                column: "SpecialityId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorProfiles_Specialties_SpecialityId",
                table: "DoctorProfiles",
                column: "SpecialityId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorProfiles_Specialties_SpecialityId1",
                table: "DoctorProfiles",
                column: "SpecialityId1",
                principalTable: "Specialties",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorProfiles_Specialties_SpecialityId",
                table: "DoctorProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorProfiles_Specialties_SpecialityId1",
                table: "DoctorProfiles");

            migrationBuilder.DropIndex(
                name: "IX_DoctorProfiles_SpecialityId1",
                table: "DoctorProfiles");

            migrationBuilder.DropColumn(
                name: "SpecialityId1",
                table: "DoctorProfiles");

            migrationBuilder.AlterColumn<int>(
                name: "SpecialityId",
                table: "DoctorProfiles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SpecialtyId",
                table: "DoctorProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorProfiles_SpecialtyId",
                table: "DoctorProfiles",
                column: "SpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorProfiles_Specialties_SpecialityId",
                table: "DoctorProfiles",
                column: "SpecialityId",
                principalTable: "Specialties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorProfiles_Specialties_SpecialtyId",
                table: "DoctorProfiles",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
