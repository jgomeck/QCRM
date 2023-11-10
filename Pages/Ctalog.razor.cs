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
    public partial class Ctalog
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

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Ctalog> ctalog;

        protected RadzenDataGrid<QCRM.Models.DB_157005_crm7des.Ctalog> grid0;

            protected IEnumerable<QCRM.Models.DB_157005_crm7des.Cuentas> cuentasForIDCUENTA;

            protected IEnumerable<QCRM.Models.DB_157005_crm7des.Usuarios> usuariosForUSUARIO;

            [Inject]
            protected SecurityService Security { get; set; }
        protected override async Task OnInitializedAsync()
        {
            ctalog = await DB_157005_crm7desService.GetCtalog(new Query { Expand = "Cuentas,Usuarios" });

            cuentasForIDCUENTA = await DB_157005_crm7desService.GetCuentas();

            usuariosForUSUARIO = await DB_157005_crm7desService.GetUsuarios();
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await grid0.InsertRow(new QCRM.Models.DB_157005_crm7des.Ctalog());
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, QCRM.Models.DB_157005_crm7des.Ctalog ctalog)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await DB_157005_crm7desService.DeleteCtalog(ctalog.ID_CTALOG);

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
                    Detail = $"Unable to delete Ctalog"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await DB_157005_crm7desService.ExportCtalogToCSV(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "Cuentas,Usuarios",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Ctalog");
            }

            if (args == null || args.Value == "xlsx")
            {
                await DB_157005_crm7desService.ExportCtalogToExcel(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "Cuentas,Usuarios",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Ctalog");
            }
        }

        protected async Task GridRowUpdate(QCRM.Models.DB_157005_crm7des.Ctalog args)
        {
            await DB_157005_crm7desService.UpdateCtalog(args.ID_CTALOG, args);
        }

        protected async Task GridRowCreate(QCRM.Models.DB_157005_crm7des.Ctalog args)
        {
            await DB_157005_crm7desService.CreateCtalog(args);
            await grid0.Reload();
        }

        protected async Task EditButtonClick(MouseEventArgs args, QCRM.Models.DB_157005_crm7des.Ctalog data)
        {
            await grid0.EditRow(data);
        }

        protected async Task SaveButtonClick(MouseEventArgs args, QCRM.Models.DB_157005_crm7des.Ctalog data)
        {
            await grid0.UpdateRow(data);
        }

        protected async Task CancelButtonClick(MouseEventArgs args, QCRM.Models.DB_157005_crm7des.Ctalog data)
        {
            grid0.CancelEditRow(data);
            await DB_157005_crm7desService.CancelCtalogChanges(data);
        }
    }
}