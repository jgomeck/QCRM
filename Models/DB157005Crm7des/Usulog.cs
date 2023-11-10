using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("USULOG", Schema = "dbo")]
    public partial class Usulog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_LOG { get; set; }

        [Required]
        public string USUARIO { get; set; }

        [Required]
        public DateTime FECHA { get; set; }

        [Required]
        public string EVENT { get; set; }

        [Required]
        public string MACHINE { get; set; }

        public int? LASTID { get; set; }

        public Usuarios Usuarios { get; set; }

    }
}