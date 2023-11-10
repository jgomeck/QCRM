using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("DOCUMENTOS", Schema = "dbo")]
    public partial class Documentos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_DOC { get; set; }

        public string CODE { get; set; }

        [Required]
        public int ID_CUENTA { get; set; }

        public int? ID_OPORTUNIDAD { get; set; }

        public int? ID_ACTIVIDAD { get; set; }

        [Required]
        public DateTime FECHA { get; set; }

        public int? VERSION { get; set; }

        public bool? ULTIMA { get; set; }

        [Required]
        public string DESCRIPCION { get; set; }

        [Required]
        public string PATH { get; set; }

        [Required]
        public string TIPODOC { get; set; }

        [Required]
        public string USUARIO { get; set; }

        public bool? DOCCIERRE { get; set; }

        public bool? DOCCERRADO { get; set; }

        public DateTime? LASTUPDATED { get; set; }

        public string LASTUSER { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

        public int? OLDID { get; set; }

        public int? LASTID { get; set; }

        public Actividades Actividades { get; set; }

        public Cuentas Cuentas { get; set; }

        public Oportunidades Oportunidades { get; set; }

        public Tiposdoc Tiposdoc { get; set; }

        public Usuarios Usuarios { get; set; }

    }
}