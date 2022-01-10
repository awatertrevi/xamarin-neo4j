//
// SessionViewModel.cs
//
// Trevi Awater
// 13-11-2021
//
// © Xamarin.Neo4j
//

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Acr.UserDialogs;
using Xamarin.Forms;
using Xamarin.Neo4j.Annotations;
using Xamarin.Neo4j.Models;
using Xamarin.Neo4j.Pages;
using Xamarin.Neo4j.Services;

namespace Xamarin.Neo4j.ViewModels
{
    public class SessionViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Neo4jService _neo4jService;

        private Database _currentDatabase;

        private List<Database> _availableDatabases;

        private ObservableCollection<QueryResult> _queryResults;

        private Neo4jConnectionString _connectionString;

        private string _query;

        public SessionViewModel(INavigation navigation, Neo4jConnectionString connectionString) : base(navigation)
        {
            _connectionString = connectionString;

            _neo4jService = DependencyService.Resolve<Neo4jService>();

            QueryResults = new ObservableCollection<QueryResult>();

            InitializeConnection(connectionString);
        }

        public async void ExecuteQuery()
        {
            _connectionString.Database = CurrentDatabase.Name;

            var result = await _neo4jService.ExecuteQuery(Query, _connectionString);

            if (result.Success)
                QueryResults.Add(result);

            else
                UserDialogs.Instance.Alert(result.ErrorMessage);
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

        public ObservableCollection<QueryResult> QueryResults
        {
            get => _queryResults;

            set
            {
                _queryResults = value;

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
