using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymApplication.Repository.Models
{
    [Table("Utilisateur")]
    [Index("Email", Name = "UQ__Utilisat__AB6E6164DFB2DE3B", IsUnique = true)]
    public partial class Utilisateur
    {
        public Utilisateur()
        {
        
            Paiements = new HashSet<Paiement>();
            Profils = new HashSet<Profil>();
        }

        [Key]
        [Column("id_utilisateur")]
        public int IdUtilisateur { get; set; }
        [Column("nom")]
        [StringLength(100)]
        public string Nom { get; set; } = null!;
        [Column("prenom")]
        [StringLength(100)]
        public string Prenom { get; set; } = null!;
        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; } = null!;
        [Column("mot_de_passe")]
        [StringLength(255)]
        public string MotDePasse { get; set; } = null!;
        [Column("date_naissance", TypeName = "date")]
        public DateTime DateNaissance { get; set; }
        [Column("adresse")]
        [StringLength(255)]
        public string? Adresse { get; set; }
        [Column("telephone")]
        [StringLength(20)]
        public string? Telephone { get; set; }
        [Column("role")]
        [StringLength(50)]
        public string Role { get; set; } = null!;
        [Column("created_at", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }

        [InverseProperty("IdUtilisateurNavigation")]
        public virtual ICollection<Paiement> Paiements { get; set; }
        [InverseProperty("IdUtilisateurNavigation")]
        public virtual ICollection<Profil> Profils { get; set; }

        public string? Token { get; set; }

        public string? ResetPasswordToken { get; set; }
        public DateTime ResetPasswordExpiry { get; set; }
    }
}
