//
// SessionViewModel.cs
//
// Trevi Awater
// 13-11-2021
//
// © Xamarin.Neo4j
//

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Acr.UserDialogs;
using Xamarin.Forms;
using Xamarin.Neo4j.Annotations;
using Xamarin.Neo4j.Models;
using Xamarin.Neo4j.Services;

namespace Xamarin.Neo4j.ViewModels
{
    public class SessionViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Neo4jService _neo4jService;

        private Database _currentDatabase;

        private List<Database> _availableDatabases;

        private string _query;

        public SessionViewModel(INavigation navigation, Neo4jConnectionString connectionString) : base(navigation)
        {
            _neo4jService = DependencyService.Resolve<Neo4jService>();

            Commands.Add("RunQuery", new Command(async () =>
            {
                _neo4jService.ExecuteQuery(Query, CurrentDatabase.Name);
            }));

            InitializeConnection(connectionString);
        }

        private async void InitializeConnection(Neo4jConnectionString connectionString)
        {
            await _neo4jService.EstablishConnection(connectionString);

            AvailableDatabases = await _neo4jService.LoadDatabases();

            if (!string.IsNullOrWhiteSpace(connectionString.Database))
                CurrentDatabase = AvailableDatabases.SingleOrDefault(ad => ad.Name == connectionString.Database);

            if (CurrentDatabase == null)
                CurrentDatabase = AvailableDatabases.Single(ad => ad.Default);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Bindable Properties

        public Database CurrentDatabase
        {
            get => _currentDatabase;

            set
            {
                _currentDatabase = value;

                OnPropertyChanged();
            }
        }

        public List<Database> AvailableDatabases
        {
            get => _availableDatabases;

            set
            {
                _availableDatabases = value;

                OnPropertyChanged();
            }
        }

        public string Query
        {
            get => _query;

            set
            {
                _query = value;

                OnPropertyChanged();
            }
        }

        #endregion
    }
}
