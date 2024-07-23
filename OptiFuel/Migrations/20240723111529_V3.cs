using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptiFuel.Migrations
{
    /// <inheritdoc />
    public partial class V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BonDeLivraison_Planings_PlanningId",
                table: "BonDeLivraison");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificat_Planings_PlanningId",
                table: "Certificat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Certificat",
                table: "Certificat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BonDeLivraison",
                table: "BonDeLivraison");

            migrationBuilder.DropColumn(
                name: "CertificatFilePath",
                table: "Certificat");

            migrationBuilder.DropColumn(
                name: "BLFilePath",
                table: "BonDeLivraison");

            migrationBuilder.RenameTable(
                name: "Certificat",
                newName: "certificats");

            migrationBuilder.RenameTable(
                name: "BonDeLivraison",
                newName: "bonDeLivraisons");

            migrationBuilder.RenameIndex(
                name: "IX_Certificat_PlanningId",
                table: "certificats",
                newName: "IX_certificats_PlanningId");

            migrationBuilder.RenameIndex(
                name: "IX_BonDeLivraison_PlanningId",
                table: "bonDeLivraisons",
                newName: "IX_bonDeLivraisons_PlanningId");

            migrationBuilder.AddColumn<byte[]>(
                name: "CertificatFile",
                table: "certificats",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "BLFile",
                table: "bonDeLivraisons",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddPrimaryKey(
                name: "PK_certificats",
                table: "certificats",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bonDeLivraisons",
                table: "bonDeLivraisons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_bonDeLivraisons_Planings_PlanningId",
                table: "bonDeLivraisons",
                column: "PlanningId",
                principalTable: "Planings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_certificats_Planings_PlanningId",
                table: "certificats",
                column: "PlanningId",
                principalTable: "Planings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bonDeLivraisons_Planings_PlanningId",
                table: "bonDeLivraisons");

            migrationBuilder.DropForeignKey(
                name: "FK_certificats_Planings_PlanningId",
                table: "certificats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_certificats",
                table: "certificats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bonDeLivraisons",
                table: "bonDeLivraisons");

            migrationBuilder.DropColumn(
                name: "CertificatFile",
                table: "certificats");

            migrationBuilder.DropColumn(
                name: "BLFile",
                table: "bonDeLivraisons");

            migrationBuilder.RenameTable(
                name: "certificats",
                newName: "Certificat");

            migrationBuilder.RenameTable(
                name: "bonDeLivraisons",
                newName: "BonDeLivraison");

            migrationBuilder.RenameIndex(
                name: "IX_certificats_PlanningId",
                table: "Certificat",
                newName: "IX_Certificat_PlanningId");

            migrationBuilder.RenameIndex(
                name: "IX_bonDeLivraisons_PlanningId",
                table: "BonDeLivraison",
                newName: "IX_BonDeLivraison_PlanningId");

            migrationBuilder.AddColumn<string>(
                name: "CertificatFilePath",
                table: "Certificat",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BLFilePath",
                table: "BonDeLivraison",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Certificat",
                table: "Certificat",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BonDeLivraison",
                table: "BonDeLivraison",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BonDeLivraison_Planings_PlanningId",
                table: "BonDeLivraison",
                column: "PlanningId",
                principalTable: "Planings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificat_Planings_PlanningId",
                table: "Certificat",
                column: "PlanningId",
                principalTable: "Planings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
