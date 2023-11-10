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
    public partial class EditGrupos
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
        public string GRUPO { get; set; }

        protected override async Task OnInitializedAsync()
        {
            grupos = await DB_157005_crm7desService.GetGruposByGrupo(GRUPO);
        }
        protected bool errorVisible;
        protected QCRM.Models.DB_157005_crm7des.Grupos grupos;

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task FormSubmit()
        {
            try
            {
                await DB_157005_crm7desService.UpdateGrupos(GRUPO, grupos);
                DialogService.Close(grupos);
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}