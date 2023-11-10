using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("ESTADOS", Schema = "dbo")]
    public partial class Estados
    {
        [Key]
        [Required]
        public string ESTADO { get; set; }

        public bool AUTOMATICO { get; set; }

        [Required]
        public int ORDEN { get; set; }

        public ICollection<Cuentas> Cuentas { get; set; }

    }
}