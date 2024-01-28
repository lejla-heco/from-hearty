using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsRemoved = table.Column<bool>(type: "INTEGER", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsRemoved = table.Column<bool>(type: "INTEGER", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    UserRoleId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cardiologists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsRemoved = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cardiologists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cardiologists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HouseDoctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsRemoved = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseDoctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HouseDoctors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsRemoved = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Patients_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsRemoved = table.Column<bool>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Start = table.Column<DateTime>(type: "TEXT", nullable: false),
                    End = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AllDay = table.Column<bool>(type: "INTEGER", nullable: false),
                    CardiologistId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PatientId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Cardiologists_CardiologistId",
                        column: x => x.CardiologistId,
                        principalTable: "Cardiologists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
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

            migrationBuilder.CreateTable(
                name: "FindingsBase",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsRemoved = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BMI = table.Column<float>(type: "REAL", nullable: false),
                    Smoking = table.Column<bool>(type: "INTEGER", nullable: false),
                    AlcoholDrinking = table.Column<string>(type: "TEXT", nullable: false),
                    Stroke = table.Column<bool>(type: "INTEGER", nullable: false),
                    PhysicalHealth = table.Column<float>(type: "REAL", nullable: false),
                    MentalHealth = table.Column<float>(type: "REAL", nullable: false),
                    DiffWalking = table.Column<bool>(type: "INTEGER", nullable: false),
                    Sex = table.Column<string>(type: "TEXT", nullable: false),
                    AgeCategory = table.Column<string>(type: "TEXT", nullable: false),
                    Diabetic = table.Column<bool>(type: "INTEGER", nullable: false),
                    PhysicalActivity = table.Column<bool>(type: "INTEGER", nullable: false),
                    GenHealth = table.Column<string>(type: "TEXT", nullable: false),
                    SleepTime = table.Column<float>(type: "REAL", nullable: false),
                    Asthma = table.Column<bool>(type: "INTEGER", nullable: false),
                    KidneyDisease = table.Column<bool>(type: "INTEGER", nullable: false),
                    SkinCancer = table.Column<bool>(type: "INTEGER", nullable: false),
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
                    table.PrimaryKey("PK_FindingsBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FindingsBase_Cardiologists_CardiologistId",
                        column: x => x.CardiologistId,
                        principalTable: "Cardiologists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FindingsBase_HouseDoctors_HouseDoctorId",
                        column: x => x.HouseDoctorId,
                        principalTable: "HouseDoctors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FindingsBase_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CardiologistId",
                table: "Appointments",
                column: "CardiologistId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Cardiologists_UserId",
                table: "Cardiologists",
                column: "UserId");

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
                name: "IX_FindingsBase_CardiologistId",
                table: "FindingsBase",
                column: "CardiologistId");

            migrationBuilder.CreateIndex(
                name: "IX_FindingsBase_HouseDoctorId",
                table: "FindingsBase",
                column: "HouseDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_FindingsBase_PatientId",
                table: "FindingsBase",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseDoctors_UserId",
                table: "HouseDoctors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_HouseDoctorId",
                table: "Patients",
                column: "HouseDoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UserId",
                table: "Patients",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserRoleId",
                table: "Users",
                column: "UserRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Findings");

            migrationBuilder.DropTable(
                name: "FindingsBase");

            migrationBuilder.DropTable(
                name: "Cardiologists");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "HouseDoctors");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}
