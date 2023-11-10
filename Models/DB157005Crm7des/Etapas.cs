using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("ETAPAS", Schema = "dbo")]
    public partial class Etapas
    {
        [Key]
        [Required]
        public string ETAPA { get; set; }

        public ICollection<Oportunidades> Oportunidades { get; set; }

    }
}