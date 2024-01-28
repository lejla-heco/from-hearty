using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPredictionResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PredictionResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsRemoved = table.Column<bool>(type: "INTEGER", nullable: false),
                    Cp = table.Column<float>(type: "REAL", nullable: false),
                    TrestBps = table.Column<float>(type: "REAL", nullable: false),
                    Chol = table.Column<float>(type: "REAL", nullable: false),
                    Fbs = table.Column<float>(type: "REAL", nullable: false),
                    RestEcg = table.Column<float>(type: "REAL", nullable: false),
                    Thalach = table.Column<float>(type: "REAL", nullable: false),
                    Exang = table.Column<float>(type: "REAL", nullable: false),
                    OldPeak = table.Column<float>(type: "REAL", nullable: false),
                    Slope = table.Column<float>(type: "REAL", nullable: false),
                    Ca = table.Column<float>(type: "REAL", nullable: false),
                    Thal = table.Column<float>(type: "REAL", nullable: false),
                    Label = table.Column<float>(type: "REAL", nullable: false),
                    CardiologistId = table.Column<Guid>(type: "TEXT", nullable: true),
                    HouseDoctorId = table.Column<Guid>(type: "TEXT", nullable: true),
                    PatientId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredictionResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PredictionResults_Cardiologists_CardiologistId",
                        column: x => x.CardiologistId,
                        principalTable: "Cardiologists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PredictionResults_HouseDoctors_HouseDoctorId",
                        column: x => x.HouseDoctorId,
                        principalTable: "HouseDoctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PredictionResults_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PredictionResults_CardiologistId",
                table: "PredictionResults",
                column: "CardiologistId");

            migrationBuilder.CreateIndex(
                name: "IX_PredictionResults_HouseDoctorId",
                table: "PredictionResults",
                column: "HouseDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_PredictionResults_PatientId",
                table: "PredictionResults",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PredictionResults");
        }
    }
}
