using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("CONTACTOS", Schema = "dbo")]
    public partial class Contactos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_CONTACTO { get; set; }

        public string CODE { get; set; }

        [Required]
        public string NOMBRE { get; set; }

        public string PUESTO { get; set; }

        public string CIUDAD { get; set; }

        public string EMAIL { get; set; }

        public string CELULAR { get; set; }

        public string LINKEDIN { get; set; }

        public DateTime? NACIMIENTO { get; set; }

        public string TELEFONO { get; set; }

        public string FAX { get; set; }

        public string NOTAS { get; set; }

        [Required]
        public int ID_CUENTA { get; set; }

        [Required]
        public string USUARIO { get; set; }

        public int? ID_REPORTA { get; set; }

        [Required]
        public string APELLIDO { get; set; }

        public string INICIALES { get; set; }

        public string STATUS { get; set; }

        public string FOTO { get; set; }

        public DateTime? DATEADDED { get; set; }

        public string USERADDED { get; set; }

        public DateTime? LASTUPDATED { get; set; }

        public string LASTUSER { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

        public int? OLDID { get; set; }

        public int? LASTID { get; set; }

        public Ciudades Ciudades { get; set; }

        public Cuentas Cuentas { get; set; }

        public Usuarios Usuarios { get; set; }

        public ICollection<Oportunidades> Oportunidades { get; set; }

        public ICollection<Oportunidades> Oportunidades1 { get; set; }

        public ICollection<Oportunidades> Oportunidades2 { get; set; }

        public ICollection<Oportunidades> Oportunidades3 { get; set; }

    }
}