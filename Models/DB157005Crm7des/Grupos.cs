using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("GRUPOS", Schema = "dbo")]
    public partial class Grupos
    {
        [Key]
        [Required]
        public string GRUPO { get; set; }

        public string DESCRIPCION { get; set; }

        public int? ORDEN { get; set; }

        public DateTime? LASTUPDATED { get; set; }

        public string LASTUSER { get; set; }

        public ICollection<Cuentas> Cuentas { get; set; }

    }
}