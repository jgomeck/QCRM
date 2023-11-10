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
    public partial class Ejecutivos
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

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Ejecutivos> ejecutivos;

        protected RadzenDataGrid<QCRM.Models.DB_157005_crm7des.Ejecutivos> grid0;

        protected string search = "";

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task Search(ChangeEventArgs args)
        {
            search = $"{args.Value}";

            await grid0.GoToPage(0);

            ejecutivos = await DB_157005_crm7desService.GetEjecutivos(new Query { Filter = $@"i => i.CODE.Contains(@0) || i.FABRICANTE.Contains(@0) || i.USUARIO.Contains(@0) || i.NOMBRE.Contains(@0) || i.APELLIDO.Contains(@0) || i.INICIALES.Contains(@0) || i.PUESTO.Contains(@0) || i.CIUDAD.Contains(@0) || i.EMAIL.Contains(@0) || i.CELULAR.Contains(@0) || i.LINKEDIN.Contains(@0) || i.TELEFONO.Contains(@0) || i.FAX.Contains(@0) || i.NOTAS.Contains(@0) || i.VERTICAL.Contains(@0) || i.AREA.Contains(@0) || i.FOTO.Contains(@0) || i.LASTUSER.Contains(@0)", FilterParameters = new object[] { search }, Expand = "Ciudades,Fabricantes,Usuarios,Verticales" });
        }
        protected override async Task OnInitializedAsync()
        {
            ejecutivos = await DB_157005_crm7desService.GetEjecutivos(new Query { Filter = $@"i => i.CODE.Contains(@0) || i.FABRICANTE.Contains(@0) || i.USUARIO.Contains(@0) || i.NOMBRE.Contains(@0) || i.APELLIDO.Contains(@0) || i.INICIALES.Contains(@0) || i.PUESTO.Contains(@0) || i.CIUDAD.Contains(@0) || i.EMAIL.Contains(@0) || i.CELULAR.Contains(@0) || i.LINKEDIN.Contains(@0) || i.TELEFONO.Contains(@0) || i.FAX.Contains(@0) || i.NOTAS.Contains(@0) || i.VERTICAL.Contains(@0) || i.AREA.Contains(@0) || i.FOTO.Contains(@0) || i.LASTUSER.Contains(@0)", FilterParameters = new object[] { search }, Expand = "Ciudades,Fabricantes,Usuarios,Verticales" });
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            NavigationManager.NavigateTo("add-ejecutivos");
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<QCRM.Models.DB_157005_crm7des.Ejecutivos> args)
        {
            NavigationManager.NavigateTo($"edit-ejecutivos/{args.Data.ID_EJEC}");
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, QCRM.Models.DB_157005_crm7des.Ejecutivos ejecutivos)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await DB_157005_crm7desService.DeleteEjecutivos(ejecutivos.ID_EJEC);

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
                    Detail = $"Unable to delete Ejecutivos"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await DB_157005_crm7desService.ExportEjecutivosToCSV(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "Ciudades,Fabricantes,Usuarios,Verticales",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Ejecutivos");
            }

            if (args == null || args.Value == "xlsx")
            {
                await DB_157005_crm7desService.ExportEjecutivosToExcel(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "Ciudades,Fabricantes,Usuarios,Verticales",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Ejecutivos");
            }
        }
    }
}