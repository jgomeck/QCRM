using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("EJECUTIVOS", Schema = "dbo")]
    public partial class Ejecutivos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_EJEC { get; set; }

        public string CODE { get; set; }

        [Required]
        public string FABRICANTE { get; set; }

        [Required]
        public string USUARIO { get; set; }

        [Required]
        public string NOMBRE { get; set; }

        [Required]
        public string APELLIDO { get; set; }

        public string INICIALES { get; set; }

        public string PUESTO { get; set; }

        [Required]
        public string CIUDAD { get; set; }

        public int? NIVEL { get; set; }

        public int? DIRECTOR { get; set; }

        public int? VP { get; set; }

        public string EMAIL { get; set; }

        public string CELULAR { get; set; }

        public string LINKEDIN { get; set; }

        public DateTime? NACIMIENTO { get; set; }

        public string TELEFONO { get; set; }

        public string FAX { get; set; }

        public int? ID_REPORTA { get; set; }

        public string NOTAS { get; set; }

        public string VERTICAL { get; set; }

        public string AREA { get; set; }

        public string FOTO { get; set; }

        public DateTime? FECHAALTA { get; set; }

        public DateTime? FECHABAJA { get; set; }

        public DateTime? LASTUPDATED { get; set; }

        public string LASTUSER { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

        public int? OLDID { get; set; }

        public int? LASTID { get; set; }

        public Ciudades Ciudades { get; set; }

        public Fabricantes Fabricantes { get; set; }

        public Usuarios Usuarios { get; set; }

        public Verticales Verticales { get; set; }

        public ICollection<Oportunidades> Oportunidades { get; set; }

    }
}