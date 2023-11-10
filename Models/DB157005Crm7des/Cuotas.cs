using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("CUOTAS", Schema = "dbo")]
    public partial class Cuotas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string USUARIO { get; set; }

        [Required]
        public int ANIO { get; set; }

        [Required]
        public int QUARTER { get; set; }

        [Required]
        public string TIPO { get; set; }

        [Required]
        public decimal MONTOUSD { get; set; }

        [Required]
        public decimal MONTOMN { get; set; }

        public Tiposerv Tiposerv { get; set; }

    }
}