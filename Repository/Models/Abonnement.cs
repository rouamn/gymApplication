using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymApplication.Repository.Models
{
    [Table("Abonnement")]
    public partial class Abonnement
    {
        [Key]
        [Column("id_abonnement")]
        public int IdAbonnement { get; set; }
     
        [Column("date_debut", TypeName = "date")]
        public DateTime DateDebut { get; set; }
        [Column("date_fin", TypeName = "date")]
        public DateTime DateFin { get; set; }
        [Column("prix", TypeName = "decimal(10, 2)")]
        public decimal Prix { get; set; }
        [Column("statut")]
        [StringLength(50)]
        public string Statut { get; set; } = null!;
        [Column("type_abonnement")]
        [StringLength(50)]
        public string TypeAbonnement { get; set; } = null!;
        [Column("created_at", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }

   public Paiement? Paiements { get; set; }
        public int PaiementFk { get; set; }

    }
}
