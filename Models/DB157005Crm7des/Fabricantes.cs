using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("FABRICANTES", Schema = "dbo")]
    public partial class Fabricantes
    {
        [Key]
        [Required]
        public string FABRICANTE { get; set; }

        [Required]
        public string NOMBRE { get; set; }

        public string LOGO { get; set; }

        public bool? OPO { get; set; }

        public int? ORDEN { get; set; }

        public ICollection<Ejecutivos> Ejecutivos { get; set; }

        public ICollection<Ejecutivoscta> Ejecutivoscta { get; set; }

        public ICollection<Oportunidades> Oportunidades { get; set; }

        public ICollection<Productosinst> Productosinst { get; set; }

    }
}