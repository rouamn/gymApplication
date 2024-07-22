using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymApplication.Repository.Models
{
    [Table("Planning")]
    public partial class Planning
    {
        [Key]
        [Column("id_planning")]
        public int IdPlanning { get; set; }
        [Column("id_evenement")]
        public int IdEvenement { get; set; }
        [Column("jour", TypeName = "date")]
        public DateTime Jour { get; set; }
        [Column("heure_debut")]
        public TimeSpan HeureDebut { get; set; }
        [Column("heure_fin")]
        public TimeSpan HeureFin { get; set; }
        [Column("created_at", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("IdEvenement")]
        [InverseProperty("Plannings")]
        public virtual Evenement IdEvenementNavigation { get; set; } = null!;
    }
}
