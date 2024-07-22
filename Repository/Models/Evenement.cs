using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymApplication.Repository.Models
{
    [Table("Evenement")]
    public partial class Evenement
    {
        public Evenement()
        {
            Plannings = new HashSet<Planning>();
        }

        [Key]
        [Column("id_evenement")]
        public int IdEvenement { get; set; }
        [Column("nom")]
        [StringLength(255)]
        public string Nom { get; set; } = null!;
        [Column("description")]
        public string? Description { get; set; }
        [Column("date", TypeName = "datetime")]
        public DateTime Date { get; set; }
        [Column("created_at", TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }

        [InverseProperty("IdEvenementNavigation")]
        public virtual ICollection<Planning> Plannings { get; set; }
    }
}
