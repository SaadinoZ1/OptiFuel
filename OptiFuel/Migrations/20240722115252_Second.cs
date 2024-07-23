using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptiFuel.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BonDeLivraison",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanningId = table.Column<int>(type: "int", nullable: false),
                    BLFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantitésLivrée = table.Column<int>(type: "int", nullable: false),
                    DateValidation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonDeLivraison", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BonDeLivraison_Planings_PlanningId",
                        column: x => x.PlanningId,
                        principalTable: "Planings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Certificat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanningId = table.Column<int>(type: "int", nullable: false),
                    CertificatFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateUpload = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificat_Planings_PlanningId",
                        column: x => x.PlanningId,
                        principalTable: "Planings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BonDeLivraison_PlanningId",
                table: "BonDeLivraison",
                column: "PlanningId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Certificat_PlanningId",
                table: "Certificat",
                column: "PlanningId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BonDeLivraison");

            migrationBuilder.DropTable(
                name: "Certificat");
        }
    }
}
