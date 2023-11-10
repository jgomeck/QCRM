using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("PRODUCTOSINST", Schema = "dbo")]
    public partial class Productosinst
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_PRODUCTO { get; set; }

        [Required]
        public string AREA { get; set; }

        [Required]
        public string FABRICANTE { get; set; }

        [Required]
        public string PRODUCTO { get; set; }

        public string VERSION { get; set; }

        public DateTime? FECHA { get; set; }

        public string IMPLEMENTADOR { get; set; }

        public string NOTAS { get; set; }

        [Required]
        public int ID_CUENTA { get; set; }

        public DateTime? LASTUPDATED { get; set; }

        public string LASTUSER { get; set; }

        public int? OLDID { get; set; }

        public int? LASTID { get; set; }

        public ICollection<Oportunidades> Oportunidades { get; set; }

        public Fabricantes Fabricantes { get; set; }

        public Cuentas Cuentas { get; set; }

    }
}