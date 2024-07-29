using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptiFuel.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_validationBLs_Planings_PlanningId",
                table: "validationBLs");

            migrationBuilder.DropTable(
                name: "Comissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Planings",
                table: "Planings");

            migrationBuilder.RenameTable(
                name: "Planings",
                newName: "Plannings");


            migrationBuilder.DropIndex(
                 name: "IX_validationBLs_PlanningId",
                 table: "validationBLs");

            migrationBuilder.DropColumn(
                name: "PlanningId",
                table: "validationBLs");

            migrationBuilder.AddColumn<Guid>(
                name: "PlanningId",
                table: "validationBLs",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.AlterColumn<double>(
                name: "LevelStart",
                table: "Dechargements",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "LevelEnd",
                table: "Dechargements",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "DeliveryVolume",
                table: "Dechargements",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Cuve",
                table: "Dechargements",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            // Drop and recreate Id in Plannings table
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Plannings");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Plannings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Plannings",
                table: "Plannings",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Commissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValidationBLId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodeG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    e_created_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    e_updated_on = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commissions_contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commissions_validationBLs_ValidationBLId",
                        column: x => x.ValidationBLId,
                        principalTable: "validationBLs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_validationBLs_PlanningId",
                table: "validationBLs",
                column: "PlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_Dechargements_ValidationBLId",
                table: "Dechargements",
                column: "ValidationBLId");

            migrationBuilder.CreateIndex(
                name: "IX_Commissions_ContactId",
                table: "Commissions",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Commissions_ValidationBLId",
                table: "Commissions",
                column: "ValidationBLId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dechargements_validationBLs_ValidationBLId",
                table: "Dechargements",
                column: "ValidationBLId",
                principalTable: "validationBLs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_validationBLs_Plannings_PlanningId",
                table: "validationBLs",
                column: "PlanningId",
                principalTable: "Plannings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dechargements_validationBLs_ValidationBLId",
                table: "Dechargements");

            migrationBuilder.DropForeignKey(
                name: "FK_validationBLs_Plannings_PlanningId",
                table: "validationBLs");

            migrationBuilder.DropTable(
                name: "Commissions");

            migrationBuilder.DropIndex(
                name: "IX_validationBLs_PlanningId",
                table: "validationBLs");

            migrationBuilder.DropIndex(
                name: "IX_Dechargements_ValidationBLId",
                table: "Dechargements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Plannings",
                table: "Plannings");

            migrationBuilder.DropColumn(
                name: "PlanningId",
                table: "validationBLs");

            migrationBuilder.AddColumn<int>(
                name: "PlanningId",
                table: "validationBLs",
                type: "int",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "LevelStart",
                table: "Dechargements",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "LevelEnd",
                table: "Dechargements",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryVolume",
                table: "Dechargements",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "Cuve",
                table: "Dechargements",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Planings",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Planings",
                table: "Planings",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Comissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValidationBLId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    e_created_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    e_updated_on = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comissions", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_validationBLs_Planings_PlanningId",
                table: "validationBLs",
                column: "PlanningId",
                principalTable: "Planings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

    }
}
