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
    public partial class AddOportunidades
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

        protected override async Task OnInitializedAsync()
        {
            oportunidades = new QCRM.Models.DB_157005_crm7des.Oportunidades();

            contactosForCLAVE = await DB_157005_crm7desService.GetContactos();

            contactosForDECISOR = await DB_157005_crm7desService.GetContactos();

            etapasForETAPA = await DB_157005_crm7desService.GetEtapas();

            contactosForEVALUADOR = await DB_157005_crm7desService.GetContactos();

            fabricantesForFABRICANTE = await DB_157005_crm7desService.GetFabricantes();

            statusForFORECAST = await DB_157005_crm7desService.GetStatus();

            cuentasForIDCUENTA = await DB_157005_crm7desService.GetCuentas();

            ejecutivosForIDEJEC = await DB_157005_crm7desService.GetEjecutivos();

            productosinstForIDPRODUCTO = await DB_157005_crm7desService.GetProductosinst();

            contactosForSPONSOR = await DB_157005_crm7desService.GetContactos();

            tiposervForTIPO = await DB_157005_crm7desService.GetTiposerv();

            usuariosForUSUARIO = await DB_157005_crm7desService.GetUsuarios();
        }
        protected bool errorVisible;
        protected QCRM.Models.DB_157005_crm7des.Oportunidades oportunidades;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Contactos> contactosForCLAVE;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Contactos> contactosForDECISOR;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Etapas> etapasForETAPA;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Contactos> contactosForEVALUADOR;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Fabricantes> fabricantesForFABRICANTE;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Status> statusForFORECAST;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Cuentas> cuentasForIDCUENTA;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Ejecutivos> ejecutivosForIDEJEC;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Productosinst> productosinstForIDPRODUCTO;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Contactos> contactosForSPONSOR;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Tiposerv> tiposervForTIPO;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Usuarios> usuariosForUSUARIO;

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task FormSubmit()
        {
            try
            {
                await DB_157005_crm7desService.CreateOportunidades(oportunidades);
                NavigationManager.NavigateTo("oportunidades");
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            NavigationManager.NavigateTo("oportunidades");
        }
    }
}