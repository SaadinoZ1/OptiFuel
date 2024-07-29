using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptiFuel.Migrations
{
    /// <inheritdoc />
    public partial class V4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bonDeLivraisons");

            migrationBuilder.DropTable(
                name: "certificats");

            migrationBuilder.AlterColumn<string>(
                name: "Center",
                table: "Planings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "e_created_on",
                table: "Planings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "e_updated_on",
                table: "Planings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "centres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    e_created_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    e_updated_on = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_centres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValidationBLId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    e_created_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    e_updated_on = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Télephone = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Poste = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    e_created_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    e_updated_on = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dechargements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValidationBLId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cuve = table.Column<int>(type: "int", nullable: false),
                    LevelStart = table.Column<int>(type: "int", nullable: false),
                    LevelEnd = table.Column<int>(type: "int", nullable: false),
                    DeliveryVolume = table.Column<int>(type: "int", nullable: false),
                    e_created_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    e_updated_on = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dechargements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "validationBLs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanningId = table.Column<int>(type: "int", nullable: false),
                    BLFile = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CertificatJumelageFile = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    QuantitésBL = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    e_created_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    e_updated_on = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_validationBLs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_validationBLs_Planings_PlanningId",
                        column: x => x.PlanningId,
                        principalTable: "Planings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_validationBLs_PlanningId",
                table: "validationBLs",
                column: "PlanningId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "centres");

            migrationBuilder.DropTable(
                name: "Comissions");

            migrationBuilder.DropTable(
                name: "contacts");

            migrationBuilder.DropTable(
                name: "Dechargements");

            migrationBuilder.DropTable(
                name: "validationBLs");

            migrationBuilder.DropColumn(
                name: "e_created_on",
                table: "Planings");

            migrationBuilder.DropColumn(
                name: "e_updated_on",
                table: "Planings");

            migrationBuilder.AlterColumn<string>(
                name: "Center",
                table: "Planings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "bonDeLivraisons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanningId = table.Column<int>(type: "int", nullable: false),
                    BLFile = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DateValidation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuantitésLivrée = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bonDeLivraisons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bonDeLivraisons_Planings_PlanningId",
                        column: x => x.PlanningId,
                        principalTable: "Planings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "certificats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanningId = table.Column<int>(type: "int", nullable: false),
                    CertificatFile = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DateUpload = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_certificats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_certificats_Planings_PlanningId",
                        column: x => x.PlanningId,
                        principalTable: "Planings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bonDeLivraisons_PlanningId",
                table: "bonDeLivraisons",
                column: "PlanningId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_certificats_PlanningId",
                table: "certificats",
                column: "PlanningId",
                unique: true);
        }
    }
}
