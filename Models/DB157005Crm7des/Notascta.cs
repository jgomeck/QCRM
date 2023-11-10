using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("NOTASCTA", Schema = "dbo")]
    public partial class Notascta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public int ID_CUENTA { get; set; }

        [Required]
        public DateTime FECHA { get; set; }

        [Required]
        public string USUARIO { get; set; }

        [Required]
        public string NOTA { get; set; }

        public int? LASTID { get; set; }

        public Cuentas Cuentas { get; set; }

        public Usuarios Usuarios { get; set; }

    }
}