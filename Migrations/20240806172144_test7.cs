using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymApplication.Migrations
{
    public partial class test7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paiement_Abonnement_AbonnemntFk",
                table: "Paiement");

            migrationBuilder.RenameColumn(
                name: "AbonnemntFk",
                table: "Paiement",
                newName: "fk_abonnement");

            migrationBuilder.RenameIndex(
                name: "IX_Paiement_AbonnemntFk",
                table: "Paiement",
                newName: "IX_Paiement_fk_abonnement");

            migrationBuilder.AddForeignKey(
                name: "FK__Paiement__id_abo__4E88ABD4",
                table: "Paiement",
                column: "fk_abonnement",
                principalTable: "Abonnement",
                principalColumn: "id_abonnement");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Paiement__id_abo__4E88ABD4",
                table: "Paiement");

            migrationBuilder.RenameColumn(
                name: "fk_abonnement",
                table: "Paiement",
                newName: "AbonnemntFk");

            migrationBuilder.RenameIndex(
                name: "IX_Paiement_fk_abonnement",
                table: "Paiement",
                newName: "IX_Paiement_AbonnemntFk");

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
