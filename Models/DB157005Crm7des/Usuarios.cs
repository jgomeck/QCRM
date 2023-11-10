using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QCRM.Models.DB_157005_crm7des
{
    [Table("USUARIOS", Schema = "dbo")]
    public partial class Usuarios
    {
        [Key]
        [Required]
        public string USUARIO { get; set; }

        [Required]
        public string NOMBRE { get; set; }

        public bool? MOSTRAR { get; set; }

        public bool? REPORTA { get; set; }

        public string FOTO { get; set; }

        [Required]
        public string PASSWORD { get; set; }

        public bool? ACTIVO { get; set; }

        public bool? PFULLDATOS { get; set; }

        public bool? PMANTENIMIENTO { get; set; }

        public bool? PCIERREOPO { get; set; }

        public bool? PUSUARIOS { get; set; }

        public bool? PCUOTAS { get; set; }

        public bool? TIENECUENTAS { get; set; }

        public bool? PLOG { get; set; }

        public bool? PFACT { get; set; }

        public string EMAIL { get; set; }

        public bool? NOTIFTO { get; set; }

        public bool? NOTIFCC { get; set; }

        public bool? NOTIFBCC { get; set; }

        public bool? NOTIFTO1 { get; set; }

        public bool? NOTIFCC1 { get; set; }

        public bool? NOTIFBCC1 { get; set; }

        [Timestamp]
        [Required]
        public byte[] SSMA_TimeStamp { get; set; }

        public ICollection<Actividades> Actividades { get; set; }

        public ICollection<Contactos> Contactos { get; set; }

        public ICollection<Ctalog> Ctalog { get; set; }

        public ICollection<Cuentas> Cuentas { get; set; }

        public ICollection<CuentaS5> CuentaS5 { get; set; }

        public ICollection<Documentos> Documentos { get; set; }

        public ICollection<Ejecutivos> Ejecutivos { get; set; }

        public ICollection<Notascta> Notascta { get; set; }

        public ICollection<Notiflog> Notiflog { get; set; }

        public ICollection<Opolog> Opolog { get; set; }

        public ICollection<Oportunidades> Oportunidades { get; set; }

        public ICollection<OportunidadeS5> OportunidadeS5 { get; set; }

        public ICollection<Presupuestos> Presupuestos { get; set; }

        public ICollection<Proyectos> Proyectos { get; set; }

        public ICollection<ProyectoS5> ProyectoS5 { get; set; }

        public ICollection<Usulog> Usulog { get; set; }

    }
}