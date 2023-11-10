using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("EJECUTIVOSCTA", Schema = "dbo")]
    public partial class Ejecutivoscta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_EJECUTIVO_CUENTA { get; set; }

        [Required]
        public int ID_CUENTA { get; set; }

        [Required]
        public string FABRICANTE { get; set; }

        [Required]
        public int AM1 { get; set; }

        public int? AM2 { get; set; }

        public int? DIRECTOR { get; set; }

        public int? VP { get; set; }

        [Required]
        public string VERTICAL { get; set; }

        public DateTime? DESDE { get; set; }

        public string NOTAS { get; set; }

        public bool? ULTIMO { get; set; }

        public DateTime? LASTUPDATED { get; set; }

        public string LASTUSER { get; set; }

        public int? LASTID { get; set; }

        public Fabricantes Fabricantes { get; set; }

        public Cuentas Cuentas { get; set; }

        public Verticales Verticales { get; set; }

    }
}