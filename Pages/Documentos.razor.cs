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
    public partial class Documentos
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

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Documentos> documentos;

        protected RadzenDataGrid<QCRM.Models.DB_157005_crm7des.Documentos> grid0;

        protected string search = "";

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task Search(ChangeEventArgs args)
        {
            search = $"{args.Value}";

            await grid0.GoToPage(0);

            documentos = await DB_157005_crm7desService.GetDocumentos(new Query { Filter = $@"i => i.CODE.Contains(@0) || i.DESCRIPCION.Contains(@0) || i.PATH.Contains(@0) || i.TIPODOC.Contains(@0) || i.USUARIO.Contains(@0) || i.LASTUSER.Contains(@0)", FilterParameters = new object[] { search }, Expand = "Actividades,Cuentas,Oportunidades,Tiposdoc,Usuarios" });
        }
        protected override async Task OnInitializedAsync()
        {
            documentos = await DB_157005_crm7desService.GetDocumentos(new Query { Filter = $@"i => i.CODE.Contains(@0) || i.DESCRIPCION.Contains(@0) || i.PATH.Contains(@0) || i.TIPODOC.Contains(@0) || i.USUARIO.Contains(@0) || i.LASTUSER.Contains(@0)", FilterParameters = new object[] { search }, Expand = "Actividades,Cuentas,Oportunidades,Tiposdoc,Usuarios" });
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            NavigationManager.NavigateTo("add-documentos");
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<QCRM.Models.DB_157005_crm7des.Documentos> args)
        {
            NavigationManager.NavigateTo($"edit-documentos/{args.Data.ID_DOC}");
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, QCRM.Models.DB_157005_crm7des.Documentos documentos)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await DB_157005_crm7desService.DeleteDocumentos(documentos.ID_DOC);

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
                    Detail = $"Unable to delete Documentos"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await DB_157005_crm7desService.ExportDocumentosToCSV(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "Actividades,Cuentas,Oportunidades,Tiposdoc,Usuarios",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Documentos");
            }

            if (args == null || args.Value == "xlsx")
            {
                await DB_157005_crm7desService.ExportDocumentosToExcel(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "Actividades,Cuentas,Oportunidades,Tiposdoc,Usuarios",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Documentos");
            }
        }
    }
}