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
    public partial class OportunidadeS5
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

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.OportunidadeS5> oportunidadeS5;

        protected RadzenDataGrid<QCRM.Models.DB_157005_crm7des.OportunidadeS5> grid0;

            protected IEnumerable<QCRM.Models.DB_157005_crm7des.Oportunidades> oportunidadesForIDOPORTUNIDAD;

            protected IEnumerable<QCRM.Models.DB_157005_crm7des.Usuarios> usuariosForUSUARIO;

            [Inject]
            protected SecurityService Security { get; set; }
        protected override async Task OnInitializedAsync()
        {
            oportunidadeS5 = await DB_157005_crm7desService.GetOportunidadeS5(new Query { Expand = "Oportunidades,Usuarios" });

            oportunidadesForIDOPORTUNIDAD = await DB_157005_crm7desService.GetOportunidades();

            usuariosForUSUARIO = await DB_157005_crm7desService.GetUsuarios();
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await grid0.InsertRow(new QCRM.Models.DB_157005_crm7des.OportunidadeS5());
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, QCRM.Models.DB_157005_crm7des.OportunidadeS5 oportunidadeS5)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await DB_157005_crm7desService.DeleteOportunidadeS5(oportunidadeS5.ID);

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
                    Detail = $"Unable to delete OportunidadeS5"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await DB_157005_crm7desService.ExportOportunidadeS5ToCSV(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "Oportunidades,Usuarios",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "OportunidadeS5");
            }

            if (args == null || args.Value == "xlsx")
            {
                await DB_157005_crm7desService.ExportOportunidadeS5ToExcel(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "Oportunidades,Usuarios",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "OportunidadeS5");
            }
        }

        protected async Task GridRowUpdate(QCRM.Models.DB_157005_crm7des.OportunidadeS5 args)
        {
            await DB_157005_crm7desService.UpdateOportunidadeS5(args.ID, args);
        }

        protected async Task GridRowCreate(QCRM.Models.DB_157005_crm7des.OportunidadeS5 args)
        {
            await DB_157005_crm7desService.CreateOportunidadeS5(args);
            await grid0.Reload();
        }

        protected async Task EditButtonClick(MouseEventArgs args, QCRM.Models.DB_157005_crm7des.OportunidadeS5 data)
        {
            await grid0.EditRow(data);
        }

        protected async Task SaveButtonClick(MouseEventArgs args, QCRM.Models.DB_157005_crm7des.OportunidadeS5 data)
        {
            await grid0.UpdateRow(data);
        }

        protected async Task CancelButtonClick(MouseEventArgs args, QCRM.Models.DB_157005_crm7des.OportunidadeS5 data)
        {
            grid0.CancelEditRow(data);
            await DB_157005_crm7desService.CancelOportunidadeS5Changes(data);
        }
    }
}