//
// SettingsViewModel.cs
//
// Trevi Awater
// 13-01-2022
//
// © Xamarin.Neo4j
//

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Neo4j.Annotations;
using Xamarin.Neo4j.Pages;

namespace Xamarin.Neo4j.ViewModels
{
    public class SettingsViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsViewModel(INavigation navigation) : base(navigation)
        {
            Commands.Add("OpenReSoftwareSite", new Command(async () =>
            {
                await Launcher.OpenAsync(new Uri("https://resoftware.nl/"));
            }));

            Commands.Add("OpenLicensesPage", new Command(async () =>
            {
               await Navigation.PushAsync(new LicensesPage());
            }));
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
