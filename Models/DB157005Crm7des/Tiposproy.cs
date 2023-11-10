using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("TIPOSPROY", Schema = "dbo")]
    public partial class Tiposproy
    {
        [Key]
        [Required]
        public string TIPO { get; set; }

        public ICollection<Proyectos> Proyectos { get; set; }

    }
}