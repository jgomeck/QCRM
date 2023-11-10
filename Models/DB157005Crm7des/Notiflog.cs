using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("NOTIFLOG", Schema = "dbo")]
    public partial class Notiflog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_NOTIF { get; set; }

        [Required]
        public string USUARIO { get; set; }

        [Required]
        public DateTime FECHA { get; set; }

        public int? CTACTC { get; set; }

        public int? OPODOC { get; set; }

        public Usuarios Usuarios { get; set; }

    }
}