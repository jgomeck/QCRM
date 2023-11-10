using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("OPORTUNIDADES", Schema = "dbo")]
    public partial class Oportunidades
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_OPORTUNIDAD { get; set; }

        public string CODE { get; set; }

        [Required]
        public int ID_CUENTA { get; set; }

        [Required]
        public string FABRICANTE { get; set; }

        [Required]
        public string NOMBRE { get; set; }

        [Required]
        public string USUARIO { get; set; }

        public string MONEDA { get; set; }

        public decimal? TC { get; set; }

        [Required]
        public decimal MONTOUSD { get; set; }

        public int? SOFTWARE { get; set; }

        [Required]
        public decimal MONTOMN { get; set; }

        [Required]
        public DateTime CIERRE { get; set; }

        public string SOURCE { get; set; }

        public string DESCRIPCION { get; set; }

        public string LINEA { get; set; }

        [Required]
        public int PROBABILIDAD { get; set; }

        public string COMPETENCIA { get; set; }

        public string REGISTRO { get; set; }

        public string ETAPA { get; set; }

        public int? SPONSOR { get; set; }

        public int? DECISOR { get; set; }

        public int? EVALUADOR { get; set; }

        public int? CLAVE { get; set; }

        public int? ID_EJEC { get; set; }

        [Required]
        public string FORECAST { get; set; }

        public int? ID_PRODUCTO { get; set; }

        public int? QUARTER { get; set; }

        [Required]
        public string TIPO { get; set; }

        public string NOTAS { get; set; }

        public bool CERRADA { get; set; }

        public string USUARIOCIERRE { get; set; }

        public DateTime? FECHACERRADA { get; set; }

        public string PROYECTO { get; set; }

        public DateTime? DATEADDED { get; set; }

        public DateTime? LASTUPDATED { get; set; }

        public string LASTUSER { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

        public int? OLDID { get; set; }

        public int? LASTID { get; set; }

        public ICollection<Actividades> Actividades { get; set; }

        public ICollection<Documentos> Documentos { get; set; }

        public ICollection<Opolog> Opolog { get; set; }

        public Contactos Contactos { get; set; }

        public Contactos Contactos1 { get; set; }

        public Etapas Etapas { get; set; }

        public Contactos Contactos2 { get; set; }

        public Fabricantes Fabricantes { get; set; }

        public Status Status { get; set; }

        public Cuentas Cuentas { get; set; }

        public Ejecutivos Ejecutivos { get; set; }

        public Productosinst Productosinst { get; set; }

        public Contactos Contactos3 { get; set; }

        public Tiposerv Tiposerv { get; set; }

        public Usuarios Usuarios { get; set; }

        public ICollection<OportunidadeS5> OportunidadeS5 { get; set; }

        public ICollection<Proyectos> Proyectos { get; set; }

    }
}