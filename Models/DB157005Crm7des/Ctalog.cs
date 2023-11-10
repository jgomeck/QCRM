using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("CTALOG", Schema = "dbo")]
    public partial class Ctalog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_CTALOG { get; set; }

        [Required]
        public int ID_CUENTA { get; set; }

        [Required]
        public string USUARIO { get; set; }

        [Required]
        public DateTime FECHA { get; set; }

        [Required]
        public string EVENTO { get; set; }

        public string USUARIOCTA { get; set; }

        public string ESTADO { get; set; }

        public string NOTAS { get; set; }

        public string NOTASCTA { get; set; }

        public int? LASTID { get; set; }

        public Cuentas Cuentas { get; set; }

        public Usuarios Usuarios { get; set; }

    }
}