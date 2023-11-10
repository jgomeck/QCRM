using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("CIUDADES", Schema = "dbo")]
    public partial class Ciudades
    {
        [Key]
        [Required]
        public string CIUDAD { get; set; }

        public int? ORDEN { get; set; }

        public ICollection<Contactos> Contactos { get; set; }

        public ICollection<Cuentas> Cuentas { get; set; }

        public ICollection<Ejecutivos> Ejecutivos { get; set; }

    }
}