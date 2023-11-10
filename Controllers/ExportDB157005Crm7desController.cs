using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using QCRM.Data;

namespace QCRM.Controllers
{
    public partial class ExportDB_157005_crm7desController : ExportController
    {
        private readonly DB_157005_crm7desContext context;
        private readonly DB_157005_crm7desService service;

        public ExportDB_157005_crm7desController(DB_157005_crm7desContext context, DB_157005_crm7desService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/DB_157005_crm7des/actividades/csv")]
        [HttpGet("/export/DB_157005_crm7des/actividades/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportActividadesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetActividades(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/actividades/excel")]
        [HttpGet("/export/DB_157005_crm7des/actividades/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportActividadesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetActividades(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/cambio/csv")]
        [HttpGet("/export/DB_157005_crm7des/cambio/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCambioToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCambio(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/cambio/excel")]
        [HttpGet("/export/DB_157005_crm7des/cambio/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCambioToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCambio(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/ciudades/csv")]
        [HttpGet("/export/DB_157005_crm7des/ciudades/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCiudadesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCiudades(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/ciudades/excel")]
        [HttpGet("/export/DB_157005_crm7des/ciudades/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCiudadesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCiudades(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/contactos/csv")]
        [HttpGet("/export/DB_157005_crm7des/contactos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportContactosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetContactos(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/contactos/excel")]
        [HttpGet("/export/DB_157005_crm7des/contactos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportContactosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetContactos(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/ctalog/csv")]
        [HttpGet("/export/DB_157005_crm7des/ctalog/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCtalogToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCtalog(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/ctalog/excel")]
        [HttpGet("/export/DB_157005_crm7des/ctalog/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCtalogToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCtalog(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/cuentas/csv")]
        [HttpGet("/export/DB_157005_crm7des/cuentas/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCuentasToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCuentas(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/cuentas/excel")]
        [HttpGet("/export/DB_157005_crm7des/cuentas/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCuentasToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCuentas(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/cuentas5/csv")]
        [HttpGet("/export/DB_157005_crm7des/cuentas5/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCuentaS5ToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCuentaS5(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/cuentas5/excel")]
        [HttpGet("/export/DB_157005_crm7des/cuentas5/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCuentaS5ToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCuentaS5(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/cuotas/csv")]
        [HttpGet("/export/DB_157005_crm7des/cuotas/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCuotasToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCuotas(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/cuotas/excel")]
        [HttpGet("/export/DB_157005_crm7des/cuotas/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCuotasToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCuotas(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/documentos/csv")]
        [HttpGet("/export/DB_157005_crm7des/documentos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDocumentosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetDocumentos(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/documentos/excel")]
        [HttpGet("/export/DB_157005_crm7des/documentos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportDocumentosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetDocumentos(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/ejecutivos/csv")]
        [HttpGet("/export/DB_157005_crm7des/ejecutivos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEjecutivosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetEjecutivos(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/ejecutivos/excel")]
        [HttpGet("/export/DB_157005_crm7des/ejecutivos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEjecutivosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetEjecutivos(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/ejecutivoscta/csv")]
        [HttpGet("/export/DB_157005_crm7des/ejecutivoscta/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEjecutivosctaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetEjecutivoscta(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/ejecutivoscta/excel")]
        [HttpGet("/export/DB_157005_crm7des/ejecutivoscta/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEjecutivosctaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetEjecutivoscta(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/estados/csv")]
        [HttpGet("/export/DB_157005_crm7des/estados/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEstadosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetEstados(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/estados/excel")]
        [HttpGet("/export/DB_157005_crm7des/estados/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEstadosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetEstados(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/etapas/csv")]
        [HttpGet("/export/DB_157005_crm7des/etapas/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEtapasToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetEtapas(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/etapas/excel")]
        [HttpGet("/export/DB_157005_crm7des/etapas/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportEtapasToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetEtapas(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/fabricantes/csv")]
        [HttpGet("/export/DB_157005_crm7des/fabricantes/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportFabricantesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetFabricantes(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/fabricantes/excel")]
        [HttpGet("/export/DB_157005_crm7des/fabricantes/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportFabricantesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetFabricantes(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/facturas/csv")]
        [HttpGet("/export/DB_157005_crm7des/facturas/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportFacturasToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetFacturas(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/facturas/excel")]
        [HttpGet("/export/DB_157005_crm7des/facturas/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportFacturasToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetFacturas(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/facturasl/csv")]
        [HttpGet("/export/DB_157005_crm7des/facturasl/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportFacturaslToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetFacturasl(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/facturasl/excel")]
        [HttpGet("/export/DB_157005_crm7des/facturasl/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportFacturaslToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetFacturasl(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/grupos/csv")]
        [HttpGet("/export/DB_157005_crm7des/grupos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportGruposToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetGrupos(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/grupos/excel")]
        [HttpGet("/export/DB_157005_crm7des/grupos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportGruposToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetGrupos(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/industrias/csv")]
        [HttpGet("/export/DB_157005_crm7des/industrias/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportIndustriasToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetIndustrias(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/industrias/excel")]
        [HttpGet("/export/DB_157005_crm7des/industrias/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportIndustriasToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetIndustrias(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/notascta/csv")]
        [HttpGet("/export/DB_157005_crm7des/notascta/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportNotasctaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetNotascta(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/notascta/excel")]
        [HttpGet("/export/DB_157005_crm7des/notascta/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportNotasctaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetNotascta(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/notiflog/csv")]
        [HttpGet("/export/DB_157005_crm7des/notiflog/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportNotiflogToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetNotiflog(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/notiflog/excel")]
        [HttpGet("/export/DB_157005_crm7des/notiflog/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportNotiflogToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetNotiflog(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/opolog/csv")]
        [HttpGet("/export/DB_157005_crm7des/opolog/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportOpologToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetOpolog(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/opolog/excel")]
        [HttpGet("/export/DB_157005_crm7des/opolog/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportOpologToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetOpolog(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/oportunidades/csv")]
        [HttpGet("/export/DB_157005_crm7des/oportunidades/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportOportunidadesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetOportunidades(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/oportunidades/excel")]
        [HttpGet("/export/DB_157005_crm7des/oportunidades/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportOportunidadesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetOportunidades(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/oportunidades5/csv")]
        [HttpGet("/export/DB_157005_crm7des/oportunidades5/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportOportunidadeS5ToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetOportunidadeS5(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/oportunidades5/excel")]
        [HttpGet("/export/DB_157005_crm7des/oportunidades5/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportOportunidadeS5ToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetOportunidadeS5(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/presupuestos/csv")]
        [HttpGet("/export/DB_157005_crm7des/presupuestos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportPresupuestosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetPresupuestos(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/presupuestos/excel")]
        [HttpGet("/export/DB_157005_crm7des/presupuestos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportPresupuestosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetPresupuestos(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/productosinst/csv")]
        [HttpGet("/export/DB_157005_crm7des/productosinst/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportProductosinstToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetProductosinst(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/productosinst/excel")]
        [HttpGet("/export/DB_157005_crm7des/productosinst/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportProductosinstToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetProductosinst(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/proyectos/csv")]
        [HttpGet("/export/DB_157005_crm7des/proyectos/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportProyectosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetProyectos(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/proyectos/excel")]
        [HttpGet("/export/DB_157005_crm7des/proyectos/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportProyectosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetProyectos(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/proyectos5/csv")]
        [HttpGet("/export/DB_157005_crm7des/proyectos5/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportProyectoS5ToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetProyectoS5(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/proyectos5/excel")]
        [HttpGet("/export/DB_157005_crm7des/proyectos5/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportProyectoS5ToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetProyectoS5(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/status/csv")]
        [HttpGet("/export/DB_157005_crm7des/status/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportStatusToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetStatus(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/status/excel")]
        [HttpGet("/export/DB_157005_crm7des/status/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportStatusToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetStatus(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/tiposact/csv")]
        [HttpGet("/export/DB_157005_crm7des/tiposact/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTiposactToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTiposact(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/tiposact/excel")]
        [HttpGet("/export/DB_157005_crm7des/tiposact/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTiposactToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTiposact(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/tiposdoc/csv")]
        [HttpGet("/export/DB_157005_crm7des/tiposdoc/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTiposdocToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTiposdoc(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/tiposdoc/excel")]
        [HttpGet("/export/DB_157005_crm7des/tiposdoc/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTiposdocToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTiposdoc(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/tiposerv/csv")]
        [HttpGet("/export/DB_157005_crm7des/tiposerv/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTiposervToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTiposerv(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/tiposerv/excel")]
        [HttpGet("/export/DB_157005_crm7des/tiposerv/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTiposervToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTiposerv(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/tiposproy/csv")]
        [HttpGet("/export/DB_157005_crm7des/tiposproy/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTiposproyToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetTiposproy(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/tiposproy/excel")]
        [HttpGet("/export/DB_157005_crm7des/tiposproy/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportTiposproyToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetTiposproy(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/usuarios/csv")]
        [HttpGet("/export/DB_157005_crm7des/usuarios/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUsuariosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetUsuarios(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/usuarios/excel")]
        [HttpGet("/export/DB_157005_crm7des/usuarios/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUsuariosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetUsuarios(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/usulog/csv")]
        [HttpGet("/export/DB_157005_crm7des/usulog/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUsulogToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetUsulog(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/usulog/excel")]
        [HttpGet("/export/DB_157005_crm7des/usulog/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportUsulogToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetUsulog(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/verticales/csv")]
        [HttpGet("/export/DB_157005_crm7des/verticales/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportVerticalesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetVerticales(), Request.Query), fileName);
        }

        [HttpGet("/export/DB_157005_crm7des/verticales/excel")]
        [HttpGet("/export/DB_157005_crm7des/verticales/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportVerticalesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetVerticales(), Request.Query), fileName);
        }
    }
}
