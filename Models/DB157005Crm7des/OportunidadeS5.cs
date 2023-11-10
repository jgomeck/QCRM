using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("OPORTUNIDADES5", Schema = "dbo")]
    public partial class OportunidadeS5
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string USUARIO { get; set; }

        [Required]
        public int ID_OPORTUNIDAD { get; set; }

        [Required]
        public DateTime FECHA { get; set; }

        public Oportunidades Oportunidades { get; set; }

        public Usuarios Usuarios { get; set; }

    }
}