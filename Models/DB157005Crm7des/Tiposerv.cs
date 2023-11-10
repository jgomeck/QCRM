using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("TIPOSERV", Schema = "dbo")]
    public partial class Tiposerv
    {
        [Key]
        [Required]
        public string TIPO { get; set; }

        public ICollection<Cuotas> Cuotas { get; set; }

        public ICollection<Oportunidades> Oportunidades { get; set; }

    }
}