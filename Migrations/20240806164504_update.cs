using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymApplication.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Abonnemen__id_ut__3E52440B",
                table: "Abonnement");

            migrationBuilder.DropTable(
                name: "Planning");

            migrationBuilder.DropIndex(
                name: "IX_Abonnement_id_utilisateur",
                table: "Abonnement");

            migrationBuilder.RenameColumn(
                name: "id_utilisateur",
                table: "Abonnement",
                newName: "PaiementFk");

            migrationBuilder.AddColumn<int>(
                name: "AbonnemntFk",
                table: "Paiement",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "heure_debut",
                table: "Evenement",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "heure_fin",
                table: "Evenement",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paiement_Abonnement_AbonnemntFk",
                table: "Paiement");

            migrationBuilder.DropIndex(
                name: "IX_Paiement_AbonnemntFk",
                table: "Paiement");

            migrationBuilder.DropColumn(
                name: "AbonnemntFk",
                table: "Paiement");

            migrationBuilder.DropColumn(
                name: "heure_debut",
                table: "Evenement");

            migrationBuilder.DropColumn(
                name: "heure_fin",
                table: "Evenement");

            migrationBuilder.RenameColumn(
                name: "PaiementFk",
                table: "Abonnement",
                newName: "id_utilisateur");

            migrationBuilder.CreateTable(
                name: "Planning",
                columns: table => new
                {
                    id_planning = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_evenement = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    heure_debut = table.Column<TimeSpan>(type: "time", nullable: false),
                    heure_fin = table.Column<TimeSpan>(type: "time", nullable: false),
                    jour = table.Column<DateTime>(type: "date", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Planning__0119D9CC46D31DDD", x => x.id_planning);
                    table.ForeignKey(
                        name: "FK__Planning__id_eve__52593CB8",
                        column: x => x.id_evenement,
                        principalTable: "Evenement",
                        principalColumn: "id_evenement");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abonnement_id_utilisateur",
                table: "Abonnement",
                column: "id_utilisateur");

            migrationBuilder.CreateIndex(
                name: "IX_Planning_id_evenement",
                table: "Planning",
                column: "id_evenement");

            migrationBuilder.AddForeignKey(
                name: "FK__Abonnemen__id_ut__3E52440B",
                table: "Abonnement",
                column: "id_utilisateur",
                principalTable: "Utilisateur",
                principalColumn: "id_utilisateur");
        }
    }
}
