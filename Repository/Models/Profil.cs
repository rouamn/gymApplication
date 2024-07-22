using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymApplication.Repository.Models
{
    [Table("Profil")]
    public partial class Profil
    {
        [Key]
        [Column("id_photo")]
        public int IdPhoto { get; set; }
        [Column("id_utilisateur")]
        public int IdUtilisateur { get; set; }
        [Column("photo")]
        public byte[]? Photo { get; set; }
        [Column("biographie")]
        public string? Biographie { get; set; }
        [Column("created_at", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("IdUtilisateur")]
        [InverseProperty("Profils")]
        public virtual Utilisateur IdUtilisateurNavigation { get; set; } = null!;
    }
}
