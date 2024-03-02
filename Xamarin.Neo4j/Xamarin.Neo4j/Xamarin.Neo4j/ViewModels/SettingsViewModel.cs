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
using Xamarin.Neo4j.Services.Interfaces;

namespace Xamarin.Neo4j.ViewModels
{
    public class SettingsViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string _versionLabel { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        private IVersionService _versionService { get; set; }

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
            
            _versionService = DependencyService.Get<IVersionService>();
            
            VersionLabel = $"Version: {_versionService.GetVersion()} (Build: {_versionService.GetBuild()})";
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        #region Bindable Properties
        
        public string VersionLabel
        {
            get => _versionLabel;

            set
            {
                _versionLabel = value;

                OnPropertyChanged(nameof(VersionLabel));
            }
        }
        
        #endregion
        
    }
}
