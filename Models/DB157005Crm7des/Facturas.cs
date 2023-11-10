using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("FACTURAS", Schema = "dbo")]
    public partial class Facturas
    {
        [Key]
        [Required]
        public string ID_FACTURA { get; set; }

        public int? NUMEROF { get; set; }

        public string TIPO { get; set; }

        [Required]
        public DateTime FECHA { get; set; }

        public DateTime? FECHAC { get; set; }

        public string PROYECTO { get; set; }

        public string CLIENTE { get; set; }

        public string MONEDA { get; set; }

        public decimal? IMPORTEMN { get; set; }

        public decimal? RETENCIONMN { get; set; }

        public decimal? IVAMN { get; set; }

        public decimal? TOTALMN { get; set; }

        public decimal? SALDOMN { get; set; }

        public decimal? TC { get; set; }

        public decimal? IMPORTEUSD { get; set; }

        public decimal? RETENCIONUSD { get; set; }

        public decimal? IVAUSD { get; set; }

        public decimal? TOTALUSD { get; set; }

        public decimal? SALDOUSD { get; set; }

        public string CONCEPTO { get; set; }

        public DateTime? FECHACOBRO { get; set; }

        public decimal? IMPORTEC { get; set; }

        public DateTime? FECHARECIB { get; set; }

        public string NOMBREREC { get; set; }

        public string COMPLEMENTO { get; set; }

        public DateTime? FECHACOMPL { get; set; }

        public bool? MULTILINEA { get; set; }

        public DateTime? LASTUPDATED { get; set; }

        public string LASTUSER { get; set; }

        public bool? LINEAGEN { get; set; }

        public int? ID_CUENTA { get; set; }

        public string NOMCTA { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

        public Cuentas Cuentas { get; set; }

        public ICollection<Facturasl> Facturasl { get; set; }

    }
}