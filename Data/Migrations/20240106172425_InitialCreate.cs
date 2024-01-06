using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cardiologists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsRemoved = table.Column<bool>(type: "INTEGER", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cardiologists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HouseDoctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsRemoved = table.Column<bool>(type: "INTEGER", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseDoctors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsRemoved = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateRegistered = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Gender = table.Column<bool>(type: "INTEGER", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HouseDoctorId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_HouseDoctors_HouseDoctorId",
                        column: x => x.HouseDoctorId,
                        principalTable: "HouseDoctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Findings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsRemoved = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FeatureA = table.Column<string>(type: "TEXT", nullable: false),
                    FeatureB = table.Column<string>(type: "TEXT", nullable: false),
                    IsSickPrediction = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsSick = table.Column<bool>(type: "INTEGER", nullable: true),
                    PatientId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CardiologistId = table.Column<Guid>(type: "TEXT", nullable: true),
                    HouseDoctorId = table.Column<Guid>(type: "TEXT", nullable: true),
                    HouseDoctorNote = table.Column<string>(type: "TEXT", nullable: true),
                    CardiologistNote = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Findings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Findings_Cardiologists_CardiologistId",
                        column: x => x.CardiologistId,
                        principalTable: "Cardiologists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Findings_HouseDoctors_HouseDoctorId",
                        column: x => x.HouseDoctorId,
                        principalTable: "HouseDoctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Findings_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Findings_CardiologistId",
                table: "Findings",
                column: "CardiologistId");

            migrationBuilder.CreateIndex(
                name: "IX_Findings_HouseDoctorId",
                table: "Findings",
                column: "HouseDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Findings_PatientId",
                table: "Findings",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_HouseDoctorId",
                table: "Patients",
                column: "HouseDoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Findings");

            migrationBuilder.DropTable(
                name: "Cardiologists");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "HouseDoctors");
        }
    }
}
