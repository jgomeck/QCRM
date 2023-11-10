using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("TIPOSDOC", Schema = "dbo")]
    public partial class Tiposdoc
    {
        [Key]
        [Required]
        public string TIPO { get; set; }

        public ICollection<Documentos> Documentos { get; set; }

    }
}