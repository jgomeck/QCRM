using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("CUENTAS", Schema = "dbo")]
    public partial class Cuentas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_CUENTA { get; set; }

        public string CODE { get; set; }

        [Required]
        public string NOMBRE { get; set; }

        [Column("RAZON SOCIAL")]
        public string RAZONSOCIAL { get; set; }

        public string GRUPO { get; set; }

        public string DIRECCION { get; set; }

        public string TELEFONO { get; set; }

        public string DESCRIPCION { get; set; }

        public int? VENTAS { get; set; }

        public int? EMPLEADOS { get; set; }

        [Required]
        public string INDUSTRIA { get; set; }

        public string COMPETIDORES { get; set; }

        public string WEBSITE { get; set; }

        [Required]
        public string ESTADO { get; set; }

        public DateTime? FECHA { get; set; }

        [Required]
        public string USUARIO { get; set; }

        [Required]
        public string CIUDAD { get; set; }

        public string WHY { get; set; }

        public string WHYNOW { get; set; }

        public string WHYQ { get; set; }

        public string LOGO { get; set; }

        public bool? BASE { get; set; }

        public int? ANIOVTAS { get; set; }

        public int? ANIOEMPL { get; set; }

        public DateTime? PROXIMO { get; set; }

        public int? PAGO { get; set; }

        public DateTime? DATEADDED { get; set; }

        public string USERADDED { get; set; }

        public DateTime? LASTUPDATED { get; set; }

        public string LASTUSER { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

        public int? OLDID { get; set; }

        public int? LASTID { get; set; }

        public ICollection<Actividades> Actividades { get; set; }

        public ICollection<Contactos> Contactos { get; set; }

        public ICollection<Ctalog> Ctalog { get; set; }

        public Ciudades Ciudades { get; set; }

        public Estados Estados { get; set; }

        public Grupos Grupos { get; set; }

        public Industrias Industrias { get; set; }

        public Usuarios Usuarios { get; set; }

        public ICollection<CuentaS5> CuentaS5 { get; set; }

        public ICollection<Documentos> Documentos { get; set; }

        public ICollection<Ejecutivoscta> Ejecutivoscta { get; set; }

        public ICollection<Facturas> Facturas { get; set; }

        public ICollection<Notascta> Notascta { get; set; }

        public ICollection<Oportunidades> Oportunidades { get; set; }

        public ICollection<Productosinst> Productosinst { get; set; }

        public ICollection<Proyectos> Proyectos { get; set; }

    }
}