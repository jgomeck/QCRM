using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace QCRM.Pages
{
    public partial class Oportunidades
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        public DB_157005_crm7desService DB_157005_crm7desService { get; set; }

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Oportunidades> oportunidades;

        protected RadzenDataGrid<QCRM.Models.DB_157005_crm7des.Oportunidades> grid0;

        protected string search = "";

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task Search(ChangeEventArgs args)
        {
            search = $"{args.Value}";

            await grid0.GoToPage(0);

            oportunidades = await DB_157005_crm7desService.GetOportunidades(new Query { Filter = $@"i => i.CODE.Contains(@0) || i.FABRICANTE.Contains(@0) || i.NOMBRE.Contains(@0) || i.USUARIO.Contains(@0) || i.MONEDA.Contains(@0) || i.SOURCE.Contains(@0) || i.DESCRIPCION.Contains(@0) || i.LINEA.Contains(@0) || i.COMPETENCIA.Contains(@0) || i.REGISTRO.Contains(@0) || i.ETAPA.Contains(@0) || i.FORECAST.Contains(@0) || i.TIPO.Contains(@0) || i.NOTAS.Contains(@0) || i.USUARIOCIERRE.Contains(@0) || i.PROYECTO.Contains(@0) || i.LASTUSER.Contains(@0)", FilterParameters = new object[] { search }, Expand = "Contactos,Contactos1,Etapas,Contactos2,Fabricantes,Status,Cuentas,Ejecutivos,Productosinst,Contactos3,Tiposerv,Usuarios" });
        }
        protected override async Task OnInitializedAsync()
        {
            oportunidades = await DB_157005_crm7desService.GetOportunidades(new Query { Filter = $@"i => i.CODE.Contains(@0) || i.FABRICANTE.Contains(@0) || i.NOMBRE.Contains(@0) || i.USUARIO.Contains(@0) || i.MONEDA.Contains(@0) || i.SOURCE.Contains(@0) || i.DESCRIPCION.Contains(@0) || i.LINEA.Contains(@0) || i.COMPETENCIA.Contains(@0) || i.REGISTRO.Contains(@0) || i.ETAPA.Contains(@0) || i.FORECAST.Contains(@0) || i.TIPO.Contains(@0) || i.NOTAS.Contains(@0) || i.USUARIOCIERRE.Contains(@0) || i.PROYECTO.Contains(@0) || i.LASTUSER.Contains(@0)", FilterParameters = new object[] { search }, Expand = "Contactos,Contactos1,Etapas,Contactos2,Fabricantes,Status,Cuentas,Ejecutivos,Productosinst,Contactos3,Tiposerv,Usuarios" });
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            NavigationManager.NavigateTo("add-oportunidades");
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<QCRM.Models.DB_157005_crm7des.Oportunidades> args)
        {
            NavigationManager.NavigateTo($"edit-oportunidades/{args.Data.ID_OPORTUNIDAD}");
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, QCRM.Models.DB_157005_crm7des.Oportunidades oportunidades)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await DB_157005_crm7desService.DeleteOportunidades(oportunidades.ID_OPORTUNIDAD);

                    if (deleteResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete Oportunidades"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await DB_157005_crm7desService.ExportOportunidadesToCSV(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "Contactos,Contactos1,Etapas,Contactos2,Fabricantes,Status,Cuentas,Ejecutivos,Productosinst,Contactos3,Tiposerv,Usuarios",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Oportunidades");
            }

            if (args == null || args.Value == "xlsx")
            {
                await DB_157005_crm7desService.ExportOportunidadesToExcel(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "Contactos,Contactos1,Etapas,Contactos2,Fabricantes,Status,Cuentas,Ejecutivos,Productosinst,Contactos3,Tiposerv,Usuarios",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Oportunidades");
            }
        }
    }
}