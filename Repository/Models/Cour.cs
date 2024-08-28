using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymApplication.Repository.Models
{
    public partial class Cour
    {
        [Key]
        [Column("id_cours")]
        public int IdCours { get; set; }
        [Column("nom")]
        [StringLength(255)]
        public string Nom { get; set; } = null!;
        [Column("description")]
        public string? Description { get; set; }
        [Column("duree")]
        public TimeSpan Duree { get; set; }

        [Column("instructor_name")]
        [StringLength(255)]
        public string? InstructorName { get; set; }

        [Column("course_date")]
        public DateTime CourseDate { get; set; }
        public string? ImagePath { get; set; }
    }
}
