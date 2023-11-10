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
    public partial class Facturas
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

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Facturas> facturas;

        protected RadzenDataGrid<QCRM.Models.DB_157005_crm7des.Facturas> grid0;

        protected string search = "";

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task Search(ChangeEventArgs args)
        {
            search = $"{args.Value}";

            await grid0.GoToPage(0);

            facturas = await DB_157005_crm7desService.GetFacturas(new Query { Filter = $@"i => i.ID_FACTURA.Contains(@0) || i.TIPO.Contains(@0) || i.PROYECTO.Contains(@0) || i.CLIENTE.Contains(@0) || i.MONEDA.Contains(@0) || i.CONCEPTO.Contains(@0) || i.NOMBREREC.Contains(@0) || i.COMPLEMENTO.Contains(@0) || i.LASTUSER.Contains(@0) || i.NOMCTA.Contains(@0)", FilterParameters = new object[] { search }, Expand = "Cuentas" });
        }
        protected override async Task OnInitializedAsync()
        {
            facturas = await DB_157005_crm7desService.GetFacturas(new Query { Filter = $@"i => i.ID_FACTURA.Contains(@0) || i.TIPO.Contains(@0) || i.PROYECTO.Contains(@0) || i.CLIENTE.Contains(@0) || i.MONEDA.Contains(@0) || i.CONCEPTO.Contains(@0) || i.NOMBREREC.Contains(@0) || i.COMPLEMENTO.Contains(@0) || i.LASTUSER.Contains(@0) || i.NOMCTA.Contains(@0)", FilterParameters = new object[] { search }, Expand = "Cuentas" });
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            NavigationManager.NavigateTo("add-facturas");
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<QCRM.Models.DB_157005_crm7des.Facturas> args)
        {
            NavigationManager.NavigateTo($"edit-facturas/{args.Data.ID_FACTURA}");
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, QCRM.Models.DB_157005_crm7des.Facturas facturas)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await DB_157005_crm7desService.DeleteFacturas(facturas.ID_FACTURA);

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
                    Detail = $"Unable to delete Facturas"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await DB_157005_crm7desService.ExportFacturasToCSV(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "Cuentas",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Facturas");
            }

            if (args == null || args.Value == "xlsx")
            {
                await DB_157005_crm7desService.ExportFacturasToExcel(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "Cuentas",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Facturas");
            }
        }
    }
}