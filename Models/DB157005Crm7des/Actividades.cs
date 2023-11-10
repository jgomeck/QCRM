using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("ACTIVIDADES", Schema = "dbo")]
    public partial class Actividades
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_ACTIVIDAD { get; set; }

        public string CODE { get; set; }

        [Required]
        public int ID_CUENTA { get; set; }

        public int? ID_CONTACTO { get; set; }

        public int? ID_OPORTUNIDAD { get; set; }

        [Required]
        public string USUARIO { get; set; }

        [Required]
        public DateTime FECHA { get; set; }

        [Required]
        public string TIPO { get; set; }

        [Required]
        public string DESCRIPCION { get; set; }

        public bool? COMPLETADA { get; set; }

        public bool ACTCERRADA { get; set; }

        public DateTime? LASTUPDATED { get; set; }

        public string LASTUSER { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

        public int? OLDID { get; set; }

        public int? LASTID { get; set; }

        public Cuentas Cuentas { get; set; }

        public Oportunidades Oportunidades { get; set; }

        public Tiposact Tiposact { get; set; }

        public Usuarios Usuarios { get; set; }

        public ICollection<Documentos> Documentos { get; set; }

    }
}