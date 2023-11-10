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
    public partial class EditDocumentos
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
        public int ID_DOC { get; set; }

        protected override async Task OnInitializedAsync()
        {
            documentos = await DB_157005_crm7desService.GetDocumentosByIdDoc(ID_DOC);

            actividadesForIDACTIVIDAD = await DB_157005_crm7desService.GetActividades();

            cuentasForIDCUENTA = await DB_157005_crm7desService.GetCuentas();

            oportunidadesForIDOPORTUNIDAD = await DB_157005_crm7desService.GetOportunidades();

            tiposdocForTIPODOC = await DB_157005_crm7desService.GetTiposdoc();

            usuariosForUSUARIO = await DB_157005_crm7desService.GetUsuarios();
        }
        protected bool errorVisible;
        protected QCRM.Models.DB_157005_crm7des.Documentos documentos;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Actividades> actividadesForIDACTIVIDAD;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Cuentas> cuentasForIDCUENTA;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Oportunidades> oportunidadesForIDOPORTUNIDAD;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Tiposdoc> tiposdocForTIPODOC;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Usuarios> usuariosForUSUARIO;

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task FormSubmit()
        {
            try
            {
                await DB_157005_crm7desService.UpdateDocumentos(ID_DOC, documentos);
                NavigationManager.NavigateTo("documentos");
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            NavigationManager.NavigateTo("documentos");
        }
    }
}