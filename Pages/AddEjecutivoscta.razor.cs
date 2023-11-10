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
    public partial class AddEjecutivoscta
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
            ejecutivoscta = new QCRM.Models.DB_157005_crm7des.Ejecutivoscta();

            fabricantesForFABRICANTE = await DB_157005_crm7desService.GetFabricantes();

            cuentasForIDCUENTA = await DB_157005_crm7desService.GetCuentas();

            verticalesForVERTICAL = await DB_157005_crm7desService.GetVerticales();
        }
        protected bool errorVisible;
        protected QCRM.Models.DB_157005_crm7des.Ejecutivoscta ejecutivoscta;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Fabricantes> fabricantesForFABRICANTE;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Cuentas> cuentasForIDCUENTA;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Verticales> verticalesForVERTICAL;

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task FormSubmit()
        {
            try
            {
                await DB_157005_crm7desService.CreateEjecutivoscta(ejecutivoscta);
                NavigationManager.NavigateTo("ejecutivoscta");
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            NavigationManager.NavigateTo("ejecutivoscta");
        }
    }
}