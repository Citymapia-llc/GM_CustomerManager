using System;
using System.Threading.Tasks;

namespace GM.Store.Client.Shared
{
    public partial class LoginPopup
    {
        protected bool ShowConfirmation { get; set; }

        public Guid Guid = Guid.NewGuid();

        private bool showBackdrop = false;
        public async Task Show()
        {
            Guid = Guid.NewGuid();
            ShowConfirmation = true;
            showBackdrop = true;
            StateHasChanged();
        }
        public void CloseModal()
        {
            ShowConfirmation = false;
            showBackdrop = false;
            StateHasChanged();
        }
    }
}
