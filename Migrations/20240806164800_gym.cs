using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymApplication.Migrations
{
    public partial class gym : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paiement_Abonnement_AbonnemntFk",
                table: "Paiement");

            migrationBuilder.DropIndex(
                name: "IX_Paiement_AbonnemntFk",
                table: "Paiement");

            migrationBuilder.CreateIndex(
                name: "IX_Abonnement_PaiementFk",
                table: "Abonnement",
                column: "PaiementFk",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Abonnement_Paiement_PaiementFk",
                table: "Abonnement",
                column: "PaiementFk",
                principalTable: "Paiement",
                principalColumn: "id_paiement",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abonnement_Paiement_PaiementFk",
                table: "Abonnement");

            migrationBuilder.DropIndex(
                name: "IX_Abonnement_PaiementFk",
                table: "Abonnement");

            migrationBuilder.CreateIndex(
                name: "IX_Paiement_AbonnemntFk",
                table: "Paiement",
                column: "AbonnemntFk",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Paiement_Abonnement_AbonnemntFk",
                table: "Paiement",
                column: "AbonnemntFk",
                principalTable: "Abonnement",
                principalColumn: "id_abonnement",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
