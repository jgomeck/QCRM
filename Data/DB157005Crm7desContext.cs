using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using QCRM.Models.DB_157005_crm7des;

namespace QCRM.Data
{
    public partial class DB_157005_crm7desContext : DbContext
    {
        public DB_157005_crm7desContext()
        {
        }

        public DB_157005_crm7desContext(DbContextOptions<DB_157005_crm7desContext> options) : base(options)
        {
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Actividades>()
              .HasOne(i => i.Cuentas)
              .WithMany(i => i.Actividades)
              .HasForeignKey(i => i.ID_CUENTA)
              .HasPrincipalKey(i => i.ID_CUENTA);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Actividades>()
              .HasOne(i => i.Oportunidades)
              .WithMany(i => i.Actividades)
              .HasForeignKey(i => i.ID_OPORTUNIDAD)
              .HasPrincipalKey(i => i.ID_OPORTUNIDAD);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Actividades>()
              .HasOne(i => i.Tiposact)
              .WithMany(i => i.Actividades)
              .HasForeignKey(i => i.TIPO)
              .HasPrincipalKey(i => i.TIPO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Actividades>()
              .HasOne(i => i.Usuarios)
              .WithMany(i => i.Actividades)
              .HasForeignKey(i => i.USUARIO)
              .HasPrincipalKey(i => i.USUARIO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Contactos>()
              .HasOne(i => i.Ciudades)
              .WithMany(i => i.Contactos)
              .HasForeignKey(i => i.CIUDAD)
              .HasPrincipalKey(i => i.CIUDAD);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Contactos>()
              .HasOne(i => i.Cuentas)
              .WithMany(i => i.Contactos)
              .HasForeignKey(i => i.ID_CUENTA)
              .HasPrincipalKey(i => i.ID_CUENTA);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Contactos>()
              .HasOne(i => i.Usuarios)
              .WithMany(i => i.Contactos)
              .HasForeignKey(i => i.USUARIO)
              .HasPrincipalKey(i => i.USUARIO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Ctalog>()
              .HasOne(i => i.Cuentas)
              .WithMany(i => i.Ctalog)
              .HasForeignKey(i => i.ID_CUENTA)
              .HasPrincipalKey(i => i.ID_CUENTA);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Ctalog>()
              .HasOne(i => i.Usuarios)
              .WithMany(i => i.Ctalog)
              .HasForeignKey(i => i.USUARIO)
              .HasPrincipalKey(i => i.USUARIO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Cuentas>()
              .HasOne(i => i.Ciudades)
              .WithMany(i => i.Cuentas)
              .HasForeignKey(i => i.CIUDAD)
              .HasPrincipalKey(i => i.CIUDAD);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Cuentas>()
              .HasOne(i => i.Estados)
              .WithMany(i => i.Cuentas)
              .HasForeignKey(i => i.ESTADO)
              .HasPrincipalKey(i => i.ESTADO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Cuentas>()
              .HasOne(i => i.Grupos)
              .WithMany(i => i.Cuentas)
              .HasForeignKey(i => i.GRUPO)
              .HasPrincipalKey(i => i.GRUPO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Cuentas>()
              .HasOne(i => i.Industrias)
              .WithMany(i => i.Cuentas)
              .HasForeignKey(i => i.INDUSTRIA)
              .HasPrincipalKey(i => i.INDUSTRIA);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Cuentas>()
              .HasOne(i => i.Usuarios)
              .WithMany(i => i.Cuentas)
              .HasForeignKey(i => i.USUARIO)
              .HasPrincipalKey(i => i.USUARIO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.CuentaS5>()
              .HasOne(i => i.Cuentas)
              .WithMany(i => i.CuentaS5)
              .HasForeignKey(i => i.ID_CUENTA)
              .HasPrincipalKey(i => i.ID_CUENTA);

            builder.Entity<QCRM.Models.DB_157005_crm7des.CuentaS5>()
              .HasOne(i => i.Usuarios)
              .WithMany(i => i.CuentaS5)
              .HasForeignKey(i => i.USUARIO)
              .HasPrincipalKey(i => i.USUARIO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Cuotas>()
              .HasOne(i => i.Tiposerv)
              .WithMany(i => i.Cuotas)
              .HasForeignKey(i => i.TIPO)
              .HasPrincipalKey(i => i.TIPO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Documentos>()
              .HasOne(i => i.Actividades)
              .WithMany(i => i.Documentos)
              .HasForeignKey(i => i.ID_ACTIVIDAD)
              .HasPrincipalKey(i => i.ID_ACTIVIDAD);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Documentos>()
              .HasOne(i => i.Cuentas)
              .WithMany(i => i.Documentos)
              .HasForeignKey(i => i.ID_CUENTA)
              .HasPrincipalKey(i => i.ID_CUENTA);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Documentos>()
              .HasOne(i => i.Oportunidades)
              .WithMany(i => i.Documentos)
              .HasForeignKey(i => i.ID_OPORTUNIDAD)
              .HasPrincipalKey(i => i.ID_OPORTUNIDAD);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Documentos>()
              .HasOne(i => i.Tiposdoc)
              .WithMany(i => i.Documentos)
              .HasForeignKey(i => i.TIPODOC)
              .HasPrincipalKey(i => i.TIPO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Documentos>()
              .HasOne(i => i.Usuarios)
              .WithMany(i => i.Documentos)
              .HasForeignKey(i => i.USUARIO)
              .HasPrincipalKey(i => i.USUARIO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Ejecutivos>()
              .HasOne(i => i.Ciudades)
              .WithMany(i => i.Ejecutivos)
              .HasForeignKey(i => i.CIUDAD)
              .HasPrincipalKey(i => i.CIUDAD);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Ejecutivos>()
              .HasOne(i => i.Fabricantes)
              .WithMany(i => i.Ejecutivos)
              .HasForeignKey(i => i.FABRICANTE)
              .HasPrincipalKey(i => i.FABRICANTE);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Ejecutivos>()
              .HasOne(i => i.Usuarios)
              .WithMany(i => i.Ejecutivos)
              .HasForeignKey(i => i.USUARIO)
              .HasPrincipalKey(i => i.USUARIO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Ejecutivos>()
              .HasOne(i => i.Verticales)
              .WithMany(i => i.Ejecutivos)
              .HasForeignKey(i => i.VERTICAL)
              .HasPrincipalKey(i => i.VERTICAL);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Ejecutivoscta>()
              .HasOne(i => i.Fabricantes)
              .WithMany(i => i.Ejecutivoscta)
              .HasForeignKey(i => i.FABRICANTE)
              .HasPrincipalKey(i => i.FABRICANTE);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Ejecutivoscta>()
              .HasOne(i => i.Cuentas)
              .WithMany(i => i.Ejecutivoscta)
              .HasForeignKey(i => i.ID_CUENTA)
              .HasPrincipalKey(i => i.ID_CUENTA);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Ejecutivoscta>()
              .HasOne(i => i.Verticales)
              .WithMany(i => i.Ejecutivoscta)
              .HasForeignKey(i => i.VERTICAL)
              .HasPrincipalKey(i => i.VERTICAL);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Facturas>()
              .HasOne(i => i.Cuentas)
              .WithMany(i => i.Facturas)
              .HasForeignKey(i => i.ID_CUENTA)
              .HasPrincipalKey(i => i.ID_CUENTA);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Facturasl>()
              .HasOne(i => i.Facturas)
              .WithMany(i => i.Facturasl)
              .HasForeignKey(i => i.ID_FACTURA)
              .HasPrincipalKey(i => i.ID_FACTURA);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Notascta>()
              .HasOne(i => i.Cuentas)
              .WithMany(i => i.Notascta)
              .HasForeignKey(i => i.ID_CUENTA)
              .HasPrincipalKey(i => i.ID_CUENTA);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Notascta>()
              .HasOne(i => i.Usuarios)
              .WithMany(i => i.Notascta)
              .HasForeignKey(i => i.USUARIO)
              .HasPrincipalKey(i => i.USUARIO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Notiflog>()
              .HasOne(i => i.Usuarios)
              .WithMany(i => i.Notiflog)
              .HasForeignKey(i => i.USUARIO)
              .HasPrincipalKey(i => i.USUARIO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Opolog>()
              .HasOne(i => i.Oportunidades)
              .WithMany(i => i.Opolog)
              .HasForeignKey(i => i.ID_OPORTUNIDAD)
              .HasPrincipalKey(i => i.ID_OPORTUNIDAD);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Opolog>()
              .HasOne(i => i.Usuarios)
              .WithMany(i => i.Opolog)
              .HasForeignKey(i => i.USUARIO)
              .HasPrincipalKey(i => i.USUARIO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Oportunidades>()
              .HasOne(i => i.Contactos)
              .WithMany(i => i.Oportunidades)
              .HasForeignKey(i => i.CLAVE)
              .HasPrincipalKey(i => i.ID_CONTACTO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Oportunidades>()
              .HasOne(i => i.Contactos1)
              .WithMany(i => i.Oportunidades1)
              .HasForeignKey(i => i.DECISOR)
              .HasPrincipalKey(i => i.ID_CONTACTO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Oportunidades>()
              .HasOne(i => i.Etapas)
              .WithMany(i => i.Oportunidades)
              .HasForeignKey(i => i.ETAPA)
              .HasPrincipalKey(i => i.ETAPA);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Oportunidades>()
              .HasOne(i => i.Contactos2)
              .WithMany(i => i.Oportunidades2)
              .HasForeignKey(i => i.EVALUADOR)
              .HasPrincipalKey(i => i.ID_CONTACTO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Oportunidades>()
              .HasOne(i => i.Fabricantes)
              .WithMany(i => i.Oportunidades)
              .HasForeignKey(i => i.FABRICANTE)
              .HasPrincipalKey(i => i.FABRICANTE);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Oportunidades>()
              .HasOne(i => i.Status)
              .WithMany(i => i.Oportunidades)
              .HasForeignKey(i => i.FORECAST)
              .HasPrincipalKey(i => i.FORECAST);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Oportunidades>()
              .HasOne(i => i.Cuentas)
              .WithMany(i => i.Oportunidades)
              .HasForeignKey(i => i.ID_CUENTA)
              .HasPrincipalKey(i => i.ID_CUENTA);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Oportunidades>()
              .HasOne(i => i.Ejecutivos)
              .WithMany(i => i.Oportunidades)
              .HasForeignKey(i => i.ID_EJEC)
              .HasPrincipalKey(i => i.ID_EJEC);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Oportunidades>()
              .HasOne(i => i.Productosinst)
              .WithMany(i => i.Oportunidades)
              .HasForeignKey(i => i.ID_PRODUCTO)
              .HasPrincipalKey(i => i.ID_PRODUCTO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Oportunidades>()
              .HasOne(i => i.Contactos3)
              .WithMany(i => i.Oportunidades3)
              .HasForeignKey(i => i.SPONSOR)
              .HasPrincipalKey(i => i.ID_CONTACTO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Oportunidades>()
              .HasOne(i => i.Tiposerv)
              .WithMany(i => i.Oportunidades)
              .HasForeignKey(i => i.TIPO)
              .HasPrincipalKey(i => i.TIPO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Oportunidades>()
              .HasOne(i => i.Usuarios)
              .WithMany(i => i.Oportunidades)
              .HasForeignKey(i => i.USUARIO)
              .HasPrincipalKey(i => i.USUARIO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.OportunidadeS5>()
              .HasOne(i => i.Oportunidades)
              .WithMany(i => i.OportunidadeS5)
              .HasForeignKey(i => i.ID_OPORTUNIDAD)
              .HasPrincipalKey(i => i.ID_OPORTUNIDAD);

            builder.Entity<QCRM.Models.DB_157005_crm7des.OportunidadeS5>()
              .HasOne(i => i.Usuarios)
              .WithMany(i => i.OportunidadeS5)
              .HasForeignKey(i => i.USUARIO)
              .HasPrincipalKey(i => i.USUARIO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Presupuestos>()
              .HasOne(i => i.Proyectos)
              .WithMany(i => i.Presupuestos)
              .HasForeignKey(i => i.PROYECTO)
              .HasPrincipalKey(i => i.PROYECTO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Presupuestos>()
              .HasOne(i => i.Usuarios)
              .WithMany(i => i.Presupuestos)
              .HasForeignKey(i => i.USUARIO)
              .HasPrincipalKey(i => i.USUARIO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Productosinst>()
              .HasOne(i => i.Fabricantes)
              .WithMany(i => i.Productosinst)
              .HasForeignKey(i => i.FABRICANTE)
              .HasPrincipalKey(i => i.FABRICANTE);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Productosinst>()
              .HasOne(i => i.Cuentas)
              .WithMany(i => i.Productosinst)
              .HasForeignKey(i => i.ID_CUENTA)
              .HasPrincipalKey(i => i.ID_CUENTA);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Proyectos>()
              .HasOne(i => i.Cuentas)
              .WithMany(i => i.Proyectos)
              .HasForeignKey(i => i.ID_CUENTA)
              .HasPrincipalKey(i => i.ID_CUENTA);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Proyectos>()
              .HasOne(i => i.Oportunidades)
              .WithMany(i => i.Proyectos)
              .HasForeignKey(i => i.ID_OPORTUNIDAD)
              .HasPrincipalKey(i => i.ID_OPORTUNIDAD);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Proyectos>()
              .HasOne(i => i.Tiposproy)
              .WithMany(i => i.Proyectos)
              .HasForeignKey(i => i.TIPO)
              .HasPrincipalKey(i => i.TIPO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Proyectos>()
              .HasOne(i => i.Usuarios)
              .WithMany(i => i.Proyectos)
              .HasForeignKey(i => i.USUARIO)
              .HasPrincipalKey(i => i.USUARIO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.ProyectoS5>()
              .HasOne(i => i.Proyectos)
              .WithMany(i => i.ProyectoS5)
              .HasForeignKey(i => i.PROYECTO)
              .HasPrincipalKey(i => i.PROYECTO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.ProyectoS5>()
              .HasOne(i => i.Usuarios)
              .WithMany(i => i.ProyectoS5)
              .HasForeignKey(i => i.USUARIO)
              .HasPrincipalKey(i => i.USUARIO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Usulog>()
              .HasOne(i => i.Usuarios)
              .WithMany(i => i.Usulog)
              .HasForeignKey(i => i.USUARIO)
              .HasPrincipalKey(i => i.USUARIO);

            builder.Entity<QCRM.Models.DB_157005_crm7des.Actividades>()
              .Property(p => p.FECHA)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Actividades>()
              .Property(p => p.LASTUPDATED)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Cambio>()
              .Property(p => p.FECHA)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Contactos>()
              .Property(p => p.NACIMIENTO)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Contactos>()
              .Property(p => p.DATEADDED)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Contactos>()
              .Property(p => p.LASTUPDATED)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Ctalog>()
              .Property(p => p.FECHA)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Cuentas>()
              .Property(p => p.FECHA)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Cuentas>()
              .Property(p => p.PROXIMO)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Cuentas>()
              .Property(p => p.DATEADDED)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Cuentas>()
              .Property(p => p.LASTUPDATED)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.CuentaS5>()
              .Property(p => p.FECHA)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Documentos>()
              .Property(p => p.FECHA)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Documentos>()
              .Property(p => p.LASTUPDATED)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Ejecutivos>()
              .Property(p => p.NACIMIENTO)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Ejecutivos>()
              .Property(p => p.FECHAALTA)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Ejecutivos>()
              .Property(p => p.FECHABAJA)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Ejecutivos>()
              .Property(p => p.LASTUPDATED)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Ejecutivoscta>()
              .Property(p => p.DESDE)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Ejecutivoscta>()
              .Property(p => p.LASTUPDATED)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Facturas>()
              .Property(p => p.FECHA)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Facturas>()
              .Property(p => p.FECHAC)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Facturas>()
              .Property(p => p.FECHACOBRO)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Facturas>()
              .Property(p => p.FECHARECIB)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Facturas>()
              .Property(p => p.FECHACOMPL)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Facturas>()
              .Property(p => p.LASTUPDATED)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Grupos>()
              .Property(p => p.LASTUPDATED)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Notascta>()
              .Property(p => p.FECHA)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Notiflog>()
              .Property(p => p.FECHA)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Opolog>()
              .Property(p => p.FECHA)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Opolog>()
              .Property(p => p.CIERRE)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Oportunidades>()
              .Property(p => p.CIERRE)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Oportunidades>()
              .Property(p => p.FECHACERRADA)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Oportunidades>()
              .Property(p => p.DATEADDED)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Oportunidades>()
              .Property(p => p.LASTUPDATED)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.OportunidadeS5>()
              .Property(p => p.FECHA)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Presupuestos>()
              .Property(p => p.FECHA)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Presupuestos>()
              .Property(p => p.LASTUPDATED)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Productosinst>()
              .Property(p => p.FECHA)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Productosinst>()
              .Property(p => p.LASTUPDATED)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Proyectos>()
              .Property(p => p.FECHALTA)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Proyectos>()
              .Property(p => p.FECHAINICIO)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Proyectos>()
              .Property(p => p.FECHAFIN)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Proyectos>()
              .Property(p => p.LASTUPDATED)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.ProyectoS5>()
              .Property(p => p.FECHA)
              .HasColumnType("datetime");

            builder.Entity<QCRM.Models.DB_157005_crm7des.Usulog>()
              .Property(p => p.FECHA)
              .HasColumnType("datetime");
            this.OnModelBuilding(builder);
        }

        public DbSet<QCRM.Models.DB_157005_crm7des.Actividades> Actividades { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Cambio> Cambio { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Ciudades> Ciudades { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Contactos> Contactos { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Ctalog> Ctalog { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Cuentas> Cuentas { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.CuentaS5> CuentaS5 { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Cuotas> Cuotas { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Documentos> Documentos { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Ejecutivos> Ejecutivos { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Ejecutivoscta> Ejecutivoscta { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Estados> Estados { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Etapas> Etapas { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Fabricantes> Fabricantes { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Facturas> Facturas { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Facturasl> Facturasl { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Grupos> Grupos { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Industrias> Industrias { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Notascta> Notascta { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Notiflog> Notiflog { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Opolog> Opolog { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Oportunidades> Oportunidades { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.OportunidadeS5> OportunidadeS5 { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Presupuestos> Presupuestos { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Productosinst> Productosinst { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Proyectos> Proyectos { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.ProyectoS5> ProyectoS5 { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Status> Status { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Tiposact> Tiposact { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Tiposdoc> Tiposdoc { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Tiposerv> Tiposerv { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Tiposproy> Tiposproy { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Usuarios> Usuarios { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Usulog> Usulog { get; set; }

        public DbSet<QCRM.Models.DB_157005_crm7des.Verticales> Verticales { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
    
    }
}