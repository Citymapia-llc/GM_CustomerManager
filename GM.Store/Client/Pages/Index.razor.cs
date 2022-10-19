using GM.Store.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace GM.Store.Client.Pages
{
    public partial class Index
    {
        private BusinessModel businessModel;
        [Parameter]
        public string PageRelativeUrl { get; set; }

        protected override async Task OnInitializedAsync()
        {
            businessModel = BusinessService.StoreDetails;
            await this.localStorage.SetItemAsync("CurrentPortal", "store");
             
            this.BusinessService.CurrentPortal = "store";
            await JS.InvokeVoidAsync("console.log", "loaded");
        }
    }
}
