//
// RootViewModel.cs
//
// Trevi Awater
// 13-11-2021
//
// © Xamarin.Neo4j
//

using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Neo4j.Annotations;
using Xamarin.Neo4j.Pages;

namespace Xamarin.Neo4j.ViewModels
{
    public class RootViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public RootViewModel(INavigation navigation) : base(navigation)
        {
            Commands.Add("AddConnection", new Command(() =>
            {
                Navigation.PushAsync(new AddConnectionPage());
            }));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
