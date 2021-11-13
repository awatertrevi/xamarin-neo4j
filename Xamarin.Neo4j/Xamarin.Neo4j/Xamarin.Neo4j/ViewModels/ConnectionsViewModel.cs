//
// ConnectionsViewModel.cs
//
// Trevi Awater
// 13-11-2021
//
// © Xamarin.Neo4j
//

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Neo4j.Annotations;
using Xamarin.Neo4j.Managers;
using Xamarin.Neo4j.Models;
using Xamarin.Neo4j.Pages;

namespace Xamarin.Neo4j.ViewModels
{
    public class ConnectionsViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private IEnumerable<Neo4jConnectionString> _connectionStrings;

        public event PropertyChangedEventHandler PropertyChanged;

        public ConnectionsViewModel(INavigation navigation) : base(navigation)
        {
            Commands.Add("AddConnection", new Command(() =>
            {
                Navigation.PushAsync(new AddConnectionPage());
            }));

            Commands.Add("DeleteConnectionString", new Command(async (o) =>
            {
                if (!(o is Neo4jConnectionString neo4jConnectionString))
                    return;

                await ConnectionStringManager.DeleteConnectionString(neo4jConnectionString);

                LoadConnectionStrings();
            }));
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void LoadConnectionStrings()
        {
            ConnectionStrings = await ConnectionStringManager.GetConnectionStrings();
        }

        #region Bindable Properties

        public IEnumerable<Neo4jConnectionString> ConnectionStrings
        {
            get => _connectionStrings;

            set
            {
                _connectionStrings = value;

                OnPropertyChanged();
            }
        }

        #endregion
    }
}
