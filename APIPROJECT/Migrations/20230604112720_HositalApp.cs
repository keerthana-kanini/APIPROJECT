using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIPROJECT.Migrations
{
    /// <inheritdoc />
    public partial class HositalApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Admin_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "100, 1"),
                    Admin_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Admin_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Admin_Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Admin_Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Doctor_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "400, 1"),
                    Doctor_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doctor_No = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Doctor_Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Customer_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "200, 1"),
                    Customer_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer_Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Customer_Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Patient_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "300, 1"),
                    Patient_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disease = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patient_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doctor_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Patient_Id);
                    table.ForeignKey(
                        name: "FK_Patients_Doctors_Doctor_Id",
                        column: x => x.Doctor_Id,
                        principalTable: "Doctors",
                        principalColumn: "Doctor_Id");
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Appointment_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Patient_Id = table.Column<int>(type: "int", nullable: false),
                    Doctor_Id = table.Column<int>(type: "int", nullable: false),
                    Appointment_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Appointment_Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_Doctor_Id",
                        column: x => x.Doctor_Id,
                        principalTable: "Doctors",
                        principalColumn: "Doctor_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_Patient_Id",
                        column: x => x.Patient_Id,
                        principalTable: "Patients",
                        principalColumn: "Patient_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Doctor_Id",
                table: "Appointments",
                column: "Doctor_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Patient_Id",
                table: "Appointments",
                column: "Patient_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Doctor_Id",
                table: "Patients",
                column: "Doctor_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Doctors");
        }
    }
}
