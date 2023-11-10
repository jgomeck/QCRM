using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("FACTURASL", Schema = "dbo")]
    public partial class Facturasl
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_LINEA { get; set; }

        public string ID_FACTURA { get; set; }

        public int? LINEA { get; set; }

        public string PROYECTO { get; set; }

        public string MONEDA { get; set; }

        public decimal? IMPORTE { get; set; }

        public string CONCEPTO { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

        public Facturas Facturas { get; set; }

    }
}