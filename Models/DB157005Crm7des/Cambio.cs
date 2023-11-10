using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("CAMBIO", Schema = "dbo")]
    public partial class Cambio
    {
        [Key]
        [Required]
        public DateTime FECHA { get; set; }

        [Required]
        public decimal TC { get; set; }

    }
}