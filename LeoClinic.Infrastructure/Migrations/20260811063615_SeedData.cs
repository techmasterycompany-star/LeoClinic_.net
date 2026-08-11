using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeoClinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Address", "City", "CreatedAt", "Name", "Phone", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "15 Tahrir Street", "Cairo", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Main Clinic", "02-27951234", null },
                    { 2, "22 El Corniche Road", "Alexandria", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Branch Clinic", "03-34856789", null },
                    { 3, "8 Pyramids Street", "Giza", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Hospital Center", "02-37891234", null },
                    { 4, "10 El Gomhouria Street", "Mansoura", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Medical Center", "050-2345678", null },
                    { 5, "5 El Saada Street", "Tanta", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Health Center", "040-3456789", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "IsBlocked", "LastName", "PasswordHash", "Role", "UpdatedAt" },
                values: new object[,]
                {
                    { 9, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ahmed.ali@clinic.com", true, "Ahmed", false, "Ali", "$2a$11$wygh8/MdNtSSImU9VL2KsORPOKi9c6Imk4MAG6EzKD9PhZ8QPMMsu", 1, null },
                    { 10, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "mohamed.salem@clinic.com", true, "Mohamed", false, "Salem", "$2a$11$wygh8/MdNtSSImU9VL2KsORPOKi9c6Imk4MAG6EzKD9PhZ8QPMMsu", 1, null },
                    { 11, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "sara.khaled@clinic.com", true, "Sara", false, "Khaled", "$2a$11$wygh8/MdNtSSImU9VL2KsORPOKi9c6Imk4MAG6EzKD9PhZ8QPMMsu", 1, null },
                    { 12, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "fatma.hassan@clinic.com", true, "Fatma", false, "Hassan", "$2a$11$wygh8/MdNtSSImU9VL2KsORPOKi9c6Imk4MAG6EzKD9PhZ8QPMMsu", 1, null },
                    { 13, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "omar.youssef@clinic.com", true, "Omar", false, "Youssef", "$2a$11$wygh8/MdNtSSImU9VL2KsORPOKi9c6Imk4MAG6EzKD9PhZ8QPMMsu", 1, null },
                    { 14, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "ali.mahmoud@clinic.com", true, "Ali", false, "Mahmoud", "$2a$11$wygh8/MdNtSSImU9VL2KsORPOKi9c6Imk4MAG6EzKD9PhZ8QPMMsu", 2, null },
                    { 15, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "hassan.ibrahim@clinic.com", true, "Hassan", false, "Ibrahim", "$2a$11$wygh8/MdNtSSImU9VL2KsORPOKi9c6Imk4MAG6EzKD9PhZ8QPMMsu", 2, null },
                    { 16, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "nour.abdelrahman@clinic.com", true, "Nour", false, "Abdelrahman", "$2a$11$wygh8/MdNtSSImU9VL2KsORPOKi9c6Imk4MAG6EzKD9PhZ8QPMMsu", 2, null },
                    { 17, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "layla.mostafa@clinic.com", true, "Layla", false, "Mostafa", "$2a$11$wygh8/MdNtSSImU9VL2KsORPOKi9c6Imk4MAG6EzKD9PhZ8QPMMsu", 2, null },
                    { 18, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "youssef.adel@clinic.com", true, "Youssef", false, "Adel", "$2a$11$wygh8/MdNtSSImU9VL2KsORPOKi9c6Imk4MAG6EzKD9PhZ8QPMMsu", 2, null },
                    { 19, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin.seed@clinic.com", true, "Admin", false, "User", "$2a$11$wygh8/MdNtSSImU9VL2KsORPOKi9c6Imk4MAG6EzKD9PhZ8QPMMsu", 0, null }
                });

            migrationBuilder.InsertData(
                table: "DoctorProfiles",
                columns: new[] { "Id", "Bio", "ContactNumber", "CreatedAt", "IsApproved", "Price", "SpecialityId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 8, "Experienced general practitioner with 10 years of practice", "01012345678", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 200m, 1, null, 9 },
                    { 9, "Cardiologist specialized in interventional cardiology", "01123456789", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 500m, 2, null, 10 },
                    { 10, "Dermatologist with expertise in cosmetic dermatology", "01234567890", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 350m, 3, null, 11 },
                    { 11, "Pediatrician caring for children from birth to adolescence", "01098765432", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 250m, 4, null, 12 },
                    { 12, "Orthopedic surgeon specializing in sports injuries", "01187654321", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 400m, 5, null, 13 }
                });

            migrationBuilder.InsertData(
                table: "PatientProfiles",
                columns: new[] { "Id", "Address", "ContactNumber", "CreatedAt", "DateOfBirth", "IsApproved", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, "23 Mohamed Ali Street, Cairo", "01011112222", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 14 },
                    { 2, "10 Sheraton Street, Cairo", "01122223333", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1985, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 15 },
                    { 3, "5 La Mansión Street, Alexandria", "01233334444", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1995, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 16 },
                    { 4, "18 El-Thawra Street, Giza", "01044445555", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1992, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 17 },
                    { 5, "7 El-Nasr Street, Mansoura", "01155556666", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1988, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 18 }
                });

            migrationBuilder.InsertData(
                table: "Availabilities",
                columns: new[] { "Id", "CreatedAt", "Date", "DoctorId", "EndTime", "IsBooked", "LocationId", "StartTime", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, new TimeSpan(0, 10, 0, 0, 0), true, 1, new TimeSpan(0, 9, 0, 0, 0), null },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, new TimeSpan(0, 11, 0, 0, 0), true, 2, new TimeSpan(0, 10, 0, 0, 0), null },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, new TimeSpan(0, 12, 0, 0, 0), true, 3, new TimeSpan(0, 11, 0, 0, 0), null },
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, new TimeSpan(0, 14, 0, 0, 0), true, 4, new TimeSpan(0, 13, 0, 0, 0), null },
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, new TimeSpan(0, 15, 0, 0, 0), true, 5, new TimeSpan(0, 14, 0, 0, 0), null }
                });

            migrationBuilder.InsertData(
                table: "DoctorLocations",
                columns: new[] { "Id", "CreatedAt", "DoctorId", "LocationId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8, 1, null },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 9, 2, null },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10, 3, null },
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 11, 4, null },
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12, 5, null }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "CreatedAt", "DoctorId", "PatientId", "Rate", "Review", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 1, 5, "Very professional and thorough examination", null },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, 2, 4, "Excellent cardiologist, highly recommended", null },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, 3, 5, "Best dermatologist I have ever visited", null },
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, 4, 4, "Very gentle with children, my kid loved her", null },
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, 5, 5, "Solved my knee problem completely", null }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AvailabilityId", "CreatedAt", "DoctorId", "Notes", "PatientId", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 8, "Annual checkup", 1, 1, null },
                    { 2, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 9, "Chest pain consultation", 2, 1, null },
                    { 3, 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 10, "Skin rash examination", 3, 1, null },
                    { 4, 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 11, "Child vaccination", 4, 1, null },
                    { 5, 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 12, "Knee pain assessment", 5, 1, null }
                });

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "Id", "AppointmentId", "CreatedAt", "IsRead", "Message", "ReadAt", "RetryCount", "SentAt", "Status", "Type", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "Your appointment has been confirmed", null, 0, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 0, null, 14 },
                    { 2, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "Your appointment has been confirmed", null, 0, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 0, null, 15 },
                    { 3, 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "Your appointment has been confirmed", null, 0, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 1, null, 16 },
                    { 4, 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "Your appointment has been confirmed", null, 0, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 2, null, 17 },
                    { 5, 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, "Your appointment has been confirmed", null, 0, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 0, null, 18 }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "AppointmentId", "CreatedAt", "PatientId", "PaymentDate", "PaymentMethod", "Status", "TransactionReference", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 200m, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cash", 1, "TXN-001", null },
                    { 2, 500m, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Credit Card", 1, "TXN-002", null },
                    { 3, 350m, 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Vodafone Cash", 1, "TXN-003", null },
                    { 4, 250m, 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "InstaPay", 1, "TXN-004", null },
                    { 5, 400m, 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cash", 1, "TXN-005", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DoctorLocations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DoctorLocations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DoctorLocations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DoctorLocations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DoctorLocations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Availabilities",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PatientProfiles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PatientProfiles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PatientProfiles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PatientProfiles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PatientProfiles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DoctorProfiles",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "DoctorProfiles",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "DoctorProfiles",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "DoctorProfiles",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "DoctorProfiles",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 13);
        }
    }
}
