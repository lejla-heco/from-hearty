using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPercetageColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PredictionResults_Cardiologists_CardiologistId",
                table: "PredictionResults");

            migrationBuilder.DropIndex(
                name: "IX_PredictionResults_CardiologistId",
                table: "PredictionResults");

            migrationBuilder.DropColumn(
                name: "CardiologistId",
                table: "PredictionResults");

            migrationBuilder.AddColumn<float>(
                name: "Percetage",
                table: "PredictionResults",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Percetage",
                table: "PredictionResults");

            migrationBuilder.AddColumn<Guid>(
                name: "CardiologistId",
                table: "PredictionResults",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PredictionResults_CardiologistId",
                table: "PredictionResults",
                column: "CardiologistId");

            migrationBuilder.AddForeignKey(
                name: "FK_PredictionResults_Cardiologists_CardiologistId",
                table: "PredictionResults",
                column: "CardiologistId",
                principalTable: "Cardiologists",
                principalColumn: "Id");
        }
    }
}
