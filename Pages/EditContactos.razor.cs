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
    public partial class EditContactos
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
        public int ID_CONTACTO { get; set; }

        protected override async Task OnInitializedAsync()
        {
            contactos = await DB_157005_crm7desService.GetContactosByIdContacto(ID_CONTACTO);

            ciudadesForCIUDAD = await DB_157005_crm7desService.GetCiudades();

            cuentasForIDCUENTA = await DB_157005_crm7desService.GetCuentas();

            usuariosForUSUARIO = await DB_157005_crm7desService.GetUsuarios();
        }
        protected bool errorVisible;
        protected QCRM.Models.DB_157005_crm7des.Contactos contactos;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Ciudades> ciudadesForCIUDAD;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Cuentas> cuentasForIDCUENTA;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Usuarios> usuariosForUSUARIO;

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task FormSubmit()
        {
            try
            {
                await DB_157005_crm7desService.UpdateContactos(ID_CONTACTO, contactos);
                NavigationManager.NavigateTo("contactos");
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            NavigationManager.NavigateTo("contactos");
        }
    }
}