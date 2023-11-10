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
    public partial class Productosinst
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

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Productosinst> productosinst;

        protected RadzenDataGrid<QCRM.Models.DB_157005_crm7des.Productosinst> grid0;

        protected string search = "";

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task Search(ChangeEventArgs args)
        {
            search = $"{args.Value}";

            await grid0.GoToPage(0);

            productosinst = await DB_157005_crm7desService.GetProductosinst(new Query { Filter = $@"i => i.AREA.Contains(@0) || i.FABRICANTE.Contains(@0) || i.PRODUCTO.Contains(@0) || i.VERSION.Contains(@0) || i.IMPLEMENTADOR.Contains(@0) || i.NOTAS.Contains(@0) || i.LASTUSER.Contains(@0)", FilterParameters = new object[] { search }, Expand = "Fabricantes,Cuentas" });
        }
        protected override async Task OnInitializedAsync()
        {
            productosinst = await DB_157005_crm7desService.GetProductosinst(new Query { Filter = $@"i => i.AREA.Contains(@0) || i.FABRICANTE.Contains(@0) || i.PRODUCTO.Contains(@0) || i.VERSION.Contains(@0) || i.IMPLEMENTADOR.Contains(@0) || i.NOTAS.Contains(@0) || i.LASTUSER.Contains(@0)", FilterParameters = new object[] { search }, Expand = "Fabricantes,Cuentas" });
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            NavigationManager.NavigateTo("add-productosinst");
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<QCRM.Models.DB_157005_crm7des.Productosinst> args)
        {
            NavigationManager.NavigateTo($"edit-productosinst/{args.Data.ID_PRODUCTO}");
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, QCRM.Models.DB_157005_crm7des.Productosinst productosinst)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await DB_157005_crm7desService.DeleteProductosinst(productosinst.ID_PRODUCTO);

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
                    Detail = $"Unable to delete Productosinst"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await DB_157005_crm7desService.ExportProductosinstToCSV(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "Fabricantes,Cuentas",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Productosinst");
            }

            if (args == null || args.Value == "xlsx")
            {
                await DB_157005_crm7desService.ExportProductosinstToExcel(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "Fabricantes,Cuentas",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Productosinst");
            }
        }
    }
}