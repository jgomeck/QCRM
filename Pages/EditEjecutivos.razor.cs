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
    public partial class EditEjecutivos
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
        public int ID_EJEC { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ejecutivos = await DB_157005_crm7desService.GetEjecutivosByIdEjec(ID_EJEC);

            ciudadesForCIUDAD = await DB_157005_crm7desService.GetCiudades();

            fabricantesForFABRICANTE = await DB_157005_crm7desService.GetFabricantes();

            usuariosForUSUARIO = await DB_157005_crm7desService.GetUsuarios();

            verticalesForVERTICAL = await DB_157005_crm7desService.GetVerticales();
        }
        protected bool errorVisible;
        protected QCRM.Models.DB_157005_crm7des.Ejecutivos ejecutivos;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Ciudades> ciudadesForCIUDAD;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Fabricantes> fabricantesForFABRICANTE;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Usuarios> usuariosForUSUARIO;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Verticales> verticalesForVERTICAL;

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task FormSubmit()
        {
            try
            {
                await DB_157005_crm7desService.UpdateEjecutivos(ID_EJEC, ejecutivos);
                NavigationManager.NavigateTo("ejecutivos");
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            NavigationManager.NavigateTo("ejecutivos");
        }
    }
}