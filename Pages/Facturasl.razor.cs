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
    public partial class Facturasl
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

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Facturasl> facturasl;

        protected RadzenDataGrid<QCRM.Models.DB_157005_crm7des.Facturasl> grid0;

            protected IEnumerable<QCRM.Models.DB_157005_crm7des.Facturas> facturasForIDFACTURA;

            [Inject]
            protected SecurityService Security { get; set; }
        protected override async Task OnInitializedAsync()
        {
            facturasl = await DB_157005_crm7desService.GetFacturasl(new Query { Expand = "Facturas" });

            facturasForIDFACTURA = await DB_157005_crm7desService.GetFacturas();
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await grid0.InsertRow(new QCRM.Models.DB_157005_crm7des.Facturasl());
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, QCRM.Models.DB_157005_crm7des.Facturasl facturasl)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await DB_157005_crm7desService.DeleteFacturasl(facturasl.ID_LINEA);

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
                    Detail = $"Unable to delete Facturasl"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await DB_157005_crm7desService.ExportFacturaslToCSV(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "Facturas",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Facturasl");
            }

            if (args == null || args.Value == "xlsx")
            {
                await DB_157005_crm7desService.ExportFacturaslToExcel(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "Facturas",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Facturasl");
            }
        }

        protected async Task GridRowUpdate(QCRM.Models.DB_157005_crm7des.Facturasl args)
        {
            await DB_157005_crm7desService.UpdateFacturasl(args.ID_LINEA, args);
        }

        protected async Task GridRowCreate(QCRM.Models.DB_157005_crm7des.Facturasl args)
        {
            await DB_157005_crm7desService.CreateFacturasl(args);
            await grid0.Reload();
        }

        protected async Task EditButtonClick(MouseEventArgs args, QCRM.Models.DB_157005_crm7des.Facturasl data)
        {
            await grid0.EditRow(data);
        }

        protected async Task SaveButtonClick(MouseEventArgs args, QCRM.Models.DB_157005_crm7des.Facturasl data)
        {
            await grid0.UpdateRow(data);
        }

        protected async Task CancelButtonClick(MouseEventArgs args, QCRM.Models.DB_157005_crm7des.Facturasl data)
        {
            grid0.CancelEditRow(data);
            await DB_157005_crm7desService.CancelFacturaslChanges(data);
        }
    }
}