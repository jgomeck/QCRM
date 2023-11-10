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
    public partial class Cuotas
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

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Cuotas> cuotas;

        protected RadzenDataGrid<QCRM.Models.DB_157005_crm7des.Cuotas> grid0;

            protected IEnumerable<QCRM.Models.DB_157005_crm7des.Tiposerv> tiposervForTIPO;

            [Inject]
            protected SecurityService Security { get; set; }
        protected override async Task OnInitializedAsync()
        {
            cuotas = await DB_157005_crm7desService.GetCuotas(new Query { Expand = "Tiposerv" });

            tiposervForTIPO = await DB_157005_crm7desService.GetTiposerv();
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await grid0.InsertRow(new QCRM.Models.DB_157005_crm7des.Cuotas());
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, QCRM.Models.DB_157005_crm7des.Cuotas cuotas)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await DB_157005_crm7desService.DeleteCuotas(cuotas.ID);

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
                    Detail = $"Unable to delete Cuotas"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await DB_157005_crm7desService.ExportCuotasToCSV(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "Tiposerv",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Cuotas");
            }

            if (args == null || args.Value == "xlsx")
            {
                await DB_157005_crm7desService.ExportCuotasToExcel(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "Tiposerv",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Cuotas");
            }
        }

        protected async Task GridRowUpdate(QCRM.Models.DB_157005_crm7des.Cuotas args)
        {
            await DB_157005_crm7desService.UpdateCuotas(args.ID, args);
        }

        protected async Task GridRowCreate(QCRM.Models.DB_157005_crm7des.Cuotas args)
        {
            await DB_157005_crm7desService.CreateCuotas(args);
            await grid0.Reload();
        }

        protected async Task EditButtonClick(MouseEventArgs args, QCRM.Models.DB_157005_crm7des.Cuotas data)
        {
            await grid0.EditRow(data);
        }

        protected async Task SaveButtonClick(MouseEventArgs args, QCRM.Models.DB_157005_crm7des.Cuotas data)
        {
            await grid0.UpdateRow(data);
        }

        protected async Task CancelButtonClick(MouseEventArgs args, QCRM.Models.DB_157005_crm7des.Cuotas data)
        {
            grid0.CancelEditRow(data);
            await DB_157005_crm7desService.CancelCuotasChanges(data);
        }
    }
}