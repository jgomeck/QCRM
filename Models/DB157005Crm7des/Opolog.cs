using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("OPOLOG", Schema = "dbo")]
    public partial class Opolog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_OPOLOG { get; set; }

        [Required]
        public int ID_OPORTUNIDAD { get; set; }

        [Required]
        public string USUARIO { get; set; }

        [Required]
        public DateTime FECHA { get; set; }

        [Required]
        public string EVENTO { get; set; }

        public string NOMBRE { get; set; }

        public string USUARIOPO { get; set; }

        public DateTime? CIERRE { get; set; }

        public string FORECAST { get; set; }

        public string ETAPA { get; set; }

        public int? PROBABILIDAD { get; set; }

        public decimal? MONTOUSD { get; set; }

        public string MONEDA { get; set; }

        public decimal? TC { get; set; }

        public decimal? MONTOMN { get; set; }

        public int? LASTID { get; set; }

        public Oportunidades Oportunidades { get; set; }

        public Usuarios Usuarios { get; set; }

    }
}