using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymApplication.Repository.Models
{
    [Table("Paiement")]
    public partial class Paiement
    {
        [Key]
        [Column("id_paiement")]
        public int IdPaiement { get; set; }

        [Column("email")]
        [StringLength(255)] 
        public string Email { get; set; } = null!;

        [Column("operation_id")]
        [StringLength(255)] 
        public string OperationId { get; set; } = null!;

        [Column("full_Name")]
        [StringLength(255)] 
        public string FullName { get; set; } = null!;

        [Column("cin")]
        [StringLength(20)] 
        public string Cin { get; set; } = null!;

     

        [Column("type_abonnement")]
        [StringLength(100)] 
        public string TypeAbonnement { get; set; } = null!;

        [Column("duree_abonnement")]
        public string DureeAbonnement { get; set; }

        [Column("prix_abonnement")]
        public decimal Prixabonnement { get; set; } 

        [Column("created_at", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at", TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }

        [Column("visibility")]
        public bool Visibility { get; set; } = true;

    }
}
