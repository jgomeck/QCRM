using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("INDUSTRIAS", Schema = "dbo")]
    public partial class Industrias
    {
        [Key]
        [Required]
        public string INDUSTRIA { get; set; }

        public ICollection<Cuentas> Cuentas { get; set; }

    }
}