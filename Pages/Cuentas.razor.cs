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
    public partial class Cuentas
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

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Cuentas> cuentas;

        protected RadzenDataGrid<QCRM.Models.DB_157005_crm7des.Cuentas> grid0;

        protected string search = "";

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task Search(ChangeEventArgs args)
        {
            search = $"{args.Value}";

            await grid0.GoToPage(0);

            cuentas = await DB_157005_crm7desService.GetCuentas(new Query { Filter = $@"i => i.CODE.Contains(@0) || i.NOMBRE.Contains(@0) || i.GRUPO.Contains(@0) || i.INDUSTRIA.Contains(@0) || i.ESTADO.Contains(@0) || i.USUARIO.Contains(@0) || i.CIUDAD.Contains(@0) || i.LASTUSER.Contains(@0)", FilterParameters = new object[] { search }, Expand = "Ciudades,Estados,Grupos,Industrias,Usuarios" });
        }

// agregar para el dropdown de usuarios
//        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Usuarios> usuariosForUSUARIO;

        protected override async Task OnInitializedAsync()
        {
            cuentas = await DB_157005_crm7desService.GetCuentas(new Query { Filter = $@"i => i.CODE.Contains(@0) || i.NOMBRE.Contains(@0) || i.RAZONSOCIAL.Contains(@0) || i.GRUPO.Contains(@0) || i.DIRECCION.Contains(@0) || i.TELEFONO.Contains(@0) || i.DESCRIPCION.Contains(@0) || i.INDUSTRIA.Contains(@0) || i.COMPETIDORES.Contains(@0) || i.WEBSITE.Contains(@0) || i.ESTADO.Contains(@0) || i.USUARIO.Contains(@0) || i.CIUDAD.Contains(@0) || i.WHY.Contains(@0) || i.WHYNOW.Contains(@0) || i.WHYQ.Contains(@0) || i.LOGO.Contains(@0) || i.USERADDED.Contains(@0) || i.LASTUSER.Contains(@0)", FilterParameters = new object[] { search }, Expand = "Ciudades,Estados,Grupos,Industrias,Usuarios" });
            
            // agregar para el dropdown de usuarios            
         //   usuariosForUSUARIO = await DB_157005_crm7desService.GetUsuarios();

        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            NavigationManager.NavigateTo("add-cuentas");
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<QCRM.Models.DB_157005_crm7des.Cuentas> args)
        {
            NavigationManager.NavigateTo($"edit-cuentas/{args.Data.ID_CUENTA}");
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, QCRM.Models.DB_157005_crm7des.Cuentas cuentas)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await DB_157005_crm7desService.DeleteCuentas(cuentas.ID_CUENTA);

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
                    Detail = $"Unable to delete Cuentas"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await DB_157005_crm7desService.ExportCuentasToCSV(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "Ciudades,Estados,Grupos,Industrias,Usuarios",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Cuentas");
            }

            if (args == null || args.Value == "xlsx")
            {
                await DB_157005_crm7desService.ExportCuentasToExcel(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "Ciudades,Estados,Grupos,Industrias,Usuarios",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Cuentas");
            }
        }
    }
}