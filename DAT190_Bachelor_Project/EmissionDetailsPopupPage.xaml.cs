using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace DAT190_Bachelor_Project
{
    public partial class EmissionDetailsPopupPage : PopupPage
    {
        public EmissionDetailsPopupPage()
        {
            InitializeComponent();

        }







        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();

            return false;
        }

        private async void CloseAllPopup()
        {
            await Navigation.PopAllPopupAsync();
        }
    }
}