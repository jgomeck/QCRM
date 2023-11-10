using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("PROYECTOS5", Schema = "dbo")]
    public partial class ProyectoS5
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string USUARIO { get; set; }

        [Required]
        public string PROYECTO { get; set; }

        [Required]
        public DateTime FECHA { get; set; }

        [Required]
        public string NOMBRE { get; set; }

        public Proyectos Proyectos { get; set; }

        public Usuarios Usuarios { get; set; }

    }
}