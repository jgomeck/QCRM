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
    public partial class EditCuentas
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

        [Parameter]
        public int ID_CUENTA { get; set; }

        protected override async Task OnInitializedAsync()
        {
            cuentas = await DB_157005_crm7desService.GetCuentasByIdCuenta(ID_CUENTA);

            ciudadesForCIUDAD = await DB_157005_crm7desService.GetCiudades();

            estadosForESTADO = await DB_157005_crm7desService.GetEstados();

            gruposForGRUPO = await DB_157005_crm7desService.GetGrupos();

            industriasForINDUSTRIA = await DB_157005_crm7desService.GetIndustrias();

            usuariosForUSUARIO = await DB_157005_crm7desService.GetUsuarios();
        }
        protected bool errorVisible;
        protected QCRM.Models.DB_157005_crm7des.Cuentas cuentas;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Ciudades> ciudadesForCIUDAD;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Estados> estadosForESTADO;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Grupos> gruposForGRUPO;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Industrias> industriasForINDUSTRIA;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Usuarios> usuariosForUSUARIO;

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task FormSubmit()
        {
            try
            {
                await DB_157005_crm7desService.UpdateCuentas(ID_CUENTA, cuentas);
                NavigationManager.NavigateTo("cuentas");
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            NavigationManager.NavigateTo("cuentas");
        }
    }
}