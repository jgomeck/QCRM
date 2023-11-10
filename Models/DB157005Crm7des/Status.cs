using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("STATUS", Schema = "dbo")]
    public partial class Status
    {
        [Key]
        [Required]
        public string FORECAST { get; set; }

        [Required]
        public string GRUPO { get; set; }

        public ICollection<Oportunidades> Oportunidades { get; set; }

    }
}