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
                name: "Contact",
                columns: table => new
                {
                    id_contact = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.id_contact);
                });

            migrationBuilder.CreateTable(
                name: "Cours",
                columns: table => new
                {
                    id_cours = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    duree = table.Column<TimeSpan>(type: "time", nullable: false),
                    instructor_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    course_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    heure_debut = table.Column<TimeSpan>(type: "time", nullable: false),
                    heure_fin = table.Column<TimeSpan>(type: "time", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Evenemen__F6BFCE778F3DA503", x => x.id_evenement);
                });

            migrationBuilder.CreateTable(
                name: "Paiement",
                columns: table => new
                {
                    id_paiement = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    operation_id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    full_Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    cin = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    type_abonnement = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    duree_abonnement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prix_abonnement = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Paiement__72D44CFF086B29B8", x => x.id_paiement);
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
                name: "Abonnement",
                columns: table => new
                {
                    id_abonnement = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prix = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    statut = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    type_abonnement = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    PaiementsIdPaiement = table.Column<int>(type: "int", nullable: true),
                    PaiementFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Abonneme__395058AB06AC8C7D", x => x.id_abonnement);
                    table.ForeignKey(
                        name: "FK_Abonnement_Paiement_PaiementsIdPaiement",
                        column: x => x.PaiementsIdPaiement,
                        principalTable: "Paiement",
                        principalColumn: "id_paiement");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abonnement_PaiementsIdPaiement",
                table: "Abonnement",
                column: "PaiementsIdPaiement");

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
                name: "Contact");

            migrationBuilder.DropTable(
                name: "Cours");

            migrationBuilder.DropTable(
                name: "Evenement");

            migrationBuilder.DropTable(
                name: "Utilisateur");

            migrationBuilder.DropTable(
                name: "Paiement");
        }
    }
}
