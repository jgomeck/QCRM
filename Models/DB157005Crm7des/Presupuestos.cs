using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("PRESUPUESTOS", Schema = "dbo")]
    public partial class Presupuestos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_PTO { get; set; }

        [Required]
        public string PROYECTO { get; set; }

        [Required]
        public string USUARIO { get; set; }

        [Required]
        public DateTime FECHA { get; set; }

        [Required]
        public string MONEDA { get; set; }

        [Required]
        public decimal MONTO { get; set; }

        public int? VERSION { get; set; }

        public DateTime? LASTUPDATED { get; set; }

        public string LASTUSER { get; set; }

        public Proyectos Proyectos { get; set; }

        public Usuarios Usuarios { get; set; }

    }
}