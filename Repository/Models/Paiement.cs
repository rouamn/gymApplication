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

        [Column("id_utilisateur")]
        public int IdUtilisateur { get; set; }

        [Column("montant", TypeName = "decimal(10, 2)")]
        public decimal Montant { get; set; }

        [Column("date", TypeName = "datetime")]
        public DateTime Date { get; set; }

        [Column("methode_paiement")]
        [StringLength(50)]
        public string MethodePaiement { get; set; } = null!;

        [Column("created_at", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at", TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }

        [Column("stripe_payment_intent_id")]
        public string? StripePaymentIntentId { get; set; } // Store Stripe PaymentIntent ID

        [ForeignKey("IdUtilisateur")]
        [InverseProperty("Paiements")]
        public virtual Utilisateur IdUtilisateurNavigation { get; set; } = null!;

        [Column("fk_abonnement")]
        public int FkAbonnement { get; set; }

        public virtual Abonnement Abonnement { get; set; } = null!;
    }
}
