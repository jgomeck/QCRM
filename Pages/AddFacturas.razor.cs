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
    public partial class AddFacturas
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
            facturas = new QCRM.Models.DB_157005_crm7des.Facturas();

            cuentasForIDCUENTA = await DB_157005_crm7desService.GetCuentas();
        }
        protected bool errorVisible;
        protected QCRM.Models.DB_157005_crm7des.Facturas facturas;

        protected IEnumerable<QCRM.Models.DB_157005_crm7des.Cuentas> cuentasForIDCUENTA;

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task FormSubmit()
        {
            try
            {
                await DB_157005_crm7desService.CreateFacturas(facturas);
                NavigationManager.NavigateTo("facturas");
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            NavigationManager.NavigateTo("facturas");
        }
    }
}