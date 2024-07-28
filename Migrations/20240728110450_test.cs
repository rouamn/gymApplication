using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymApplication.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cours",
                columns: table => new
                {
                    id_cours = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    duree = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cours__7927EBB9D64C6A59", x => x.id_cours);
                });

            migrationBuilder.CreateTable(
                name: "Evenement",
                columns: table => new
                {
                    id_evenement = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Evenemen__F6BFCE778F3DA503", x => x.id_evenement);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateur",
                columns: table => new
                {
                    id_utilisateur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    prenom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    mot_de_passe = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    date_naissance = table.Column<DateTime>(type: "date", nullable: false),
                    adresse = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    telephone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetPasswordToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetPasswordExpiry = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Utilisat__1A4FA5B8F3C735FF", x => x.id_utilisateur);
                });

            migrationBuilder.CreateTable(
                name: "Planning",
                columns: table => new
                {
                    id_planning = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_evenement = table.Column<int>(type: "int", nullable: false),
                    jour = table.Column<DateTime>(type: "date", nullable: false),
                    heure_debut = table.Column<TimeSpan>(type: "time", nullable: false),
                    heure_fin = table.Column<TimeSpan>(type: "time", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
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

            migrationBuilder.CreateTable(
                name: "Abonnement",
                columns: table => new
                {
                    id_abonnement = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_utilisateur = table.Column<int>(type: "int", nullable: false),
                    date_debut = table.Column<DateTime>(type: "date", nullable: false),
                    date_fin = table.Column<DateTime>(type: "date", nullable: false),
                    prix = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    statut = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    type_abonnement = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Abonneme__395058AB06AC8C7D", x => x.id_abonnement);
                    table.ForeignKey(
                        name: "FK__Abonnemen__id_ut__3E52440B",
                        column: x => x.id_utilisateur,
                        principalTable: "Utilisateur",
                        principalColumn: "id_utilisateur");
                });

            migrationBuilder.CreateTable(
                name: "Paiement",
                columns: table => new
                {
                    id_paiement = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_utilisateur = table.Column<int>(type: "int", nullable: false),
                    montant = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    methode_paiement = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Paiement__72D44CFF086B29B8", x => x.id_paiement);
                    table.ForeignKey(
                        name: "FK__Paiement__id_uti__46E78A0C",
                        column: x => x.id_utilisateur,
                        principalTable: "Utilisateur",
                        principalColumn: "id_utilisateur");
                });

            migrationBuilder.CreateTable(
                name: "Profil",
                columns: table => new
                {
                    id_photo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_utilisateur = table.Column<int>(type: "int", nullable: false),
                    photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    biographie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Profil__599E10ACBD030CCE", x => x.id_photo);
                    table.ForeignKey(
                        name: "FK__Profil__id_utili__4D94879B",
                        column: x => x.id_utilisateur,
                        principalTable: "Utilisateur",
                        principalColumn: "id_utilisateur");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abonnement_id_utilisateur",
                table: "Abonnement",
                column: "id_utilisateur");

            migrationBuilder.CreateIndex(
                name: "IX_Paiement_id_utilisateur",
                table: "Paiement",
                column: "id_utilisateur");

            migrationBuilder.CreateIndex(
                name: "IX_Planning_id_evenement",
                table: "Planning",
                column: "id_evenement");

            migrationBuilder.CreateIndex(
                name: "IX_Profil_id_utilisateur",
                table: "Profil",
                column: "id_utilisateur");

            migrationBuilder.CreateIndex(
                name: "UQ__Utilisat__AB6E6164DFB2DE3B",
                table: "Utilisateur",
                column: "email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abonnement");

            migrationBuilder.DropTable(
                name: "Cours");

            migrationBuilder.DropTable(
                name: "Paiement");

            migrationBuilder.DropTable(
                name: "Planning");

            migrationBuilder.DropTable(
                name: "Profil");

            migrationBuilder.DropTable(
                name: "Evenement");

            migrationBuilder.DropTable(
                name: "Utilisateur");
        }
    }
}
