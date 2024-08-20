using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GymApplication.Repository.Models
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        [Column("id_contact")]
        public int IdContact { get; set; }

        public String Nom { get; set; }
       
        public String Email { get; set; }

        public String Description { get; set; }
    }
}
