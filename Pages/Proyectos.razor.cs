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
    public partial class Proyectos
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

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Proyectos> proyectos;

        protected RadzenDataGrid<QCRM.Models.DB_157005_crm7des.Proyectos> grid0;

        protected string search = "";

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task Search(ChangeEventArgs args)
        {
            search = $"{args.Value}";

            await grid0.GoToPage(0);

            proyectos = await DB_157005_crm7desService.GetProyectos(new Query { Filter = $@"i => i.PROYECTO.Contains(@0) || i.CLIENTE.Contains(@0) || i.NOMBRE.Contains(@0) || i.DESCRIPCION.Contains(@0) || i.GERENTE.Contains(@0) || i.USUARIO.Contains(@0) || i.MONEDA.Contains(@0) || i.TIPO.Contains(@0) || i.USUARIOALT.Contains(@0) || i.LASTUSER.Contains(@0)", FilterParameters = new object[] { search }, Expand = "Cuentas,Oportunidades,Tiposproy,Usuarios" });
        }
        protected override async Task OnInitializedAsync()
        {
            proyectos = await DB_157005_crm7desService.GetProyectos(new Query { Filter = $@"i => i.PROYECTO.Contains(@0) || i.CLIENTE.Contains(@0) || i.NOMBRE.Contains(@0) || i.DESCRIPCION.Contains(@0) || i.GERENTE.Contains(@0) || i.USUARIO.Contains(@0) || i.MONEDA.Contains(@0) || i.TIPO.Contains(@0) || i.USUARIOALT.Contains(@0) || i.LASTUSER.Contains(@0)", FilterParameters = new object[] { search }, Expand = "Cuentas,Oportunidades,Tiposproy,Usuarios" });
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            NavigationManager.NavigateTo("add-proyectos");
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<QCRM.Models.DB_157005_crm7des.Proyectos> args)
        {
            NavigationManager.NavigateTo($"edit-proyectos/{args.Data.PROYECTO}");
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, QCRM.Models.DB_157005_crm7des.Proyectos proyectos)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await DB_157005_crm7desService.DeleteProyectos(proyectos.PROYECTO);

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
                    Detail = $"Unable to delete Proyectos"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await DB_157005_crm7desService.ExportProyectosToCSV(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "Cuentas,Oportunidades,Tiposproy,Usuarios",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Proyectos");
            }

            if (args == null || args.Value == "xlsx")
            {
                await DB_157005_crm7desService.ExportProyectosToExcel(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "Cuentas,Oportunidades,Tiposproy,Usuarios",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Proyectos");
            }
        }
    }
}