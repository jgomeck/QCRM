using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("PROYECTOS", Schema = "dbo")]
    public partial class Proyectos
    {
        [Key]
        [Required]
        public string PROYECTO { get; set; }

        public int? ID_CUENTA { get; set; }

        public string CLIENTE { get; set; }

        public int? ID_OPORTUNIDAD { get; set; }

        [Required]
        public string NOMBRE { get; set; }

        public string DESCRIPCION { get; set; }

        public string GERENTE { get; set; }

        public string USUARIO { get; set; }

        public string MONEDA { get; set; }

        public decimal? MONTO { get; set; }

        public string TIPO { get; set; }

        public DateTime? FECHALTA { get; set; }

        public string USUARIOALT { get; set; }

        public DateTime? FECHAINICIO { get; set; }

        public DateTime? FECHAFIN { get; set; }

        public int? PAGO { get; set; }

        public DateTime? LASTUPDATED { get; set; }

        public string LASTUSER { get; set; }

        public ICollection<Presupuestos> Presupuestos { get; set; }

        public Cuentas Cuentas { get; set; }

        public Oportunidades Oportunidades { get; set; }

        public Tiposproy Tiposproy { get; set; }

        public Usuarios Usuarios { get; set; }

        public ICollection<ProyectoS5> ProyectoS5 { get; set; }

    }
}