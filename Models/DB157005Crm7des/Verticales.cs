using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("VERTICALES", Schema = "dbo")]
    public partial class Verticales
    {
        [Key]
        [Required]
        public string VERTICAL { get; set; }

        public ICollection<Ejecutivos> Ejecutivos { get; set; }

        public ICollection<Ejecutivoscta> Ejecutivoscta { get; set; }

    }
}