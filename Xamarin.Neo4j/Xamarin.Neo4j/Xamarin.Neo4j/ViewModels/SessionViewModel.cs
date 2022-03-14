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
using Xamarin.Forms.Internals;
using Xamarin.Neo4j.Annotations;
using Xamarin.Neo4j.Models;
using Xamarin.Neo4j.Pages;
using Xamarin.Neo4j.Services;
using Xamarin.Neo4j.Utilities;

namespace Xamarin.Neo4j.ViewModels
{
    public class SessionViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Neo4jService _neo4jService;

        private readonly IScreenSizeService _screenSizeService;

        private Database _currentDatabase;

        private List<Database> _availableDatabases;

        private ObservableCollection<QueryResult> _queryResults;

        private Neo4jConnectionString _connectionString;

        private string _query;

        public SessionViewModel(INavigation navigation, Neo4jConnectionString connectionString, string initialQuery) : base(navigation)
        {
            _connectionString = connectionString;

            _neo4jService = DependencyService.Resolve<Neo4jService>();
            _screenSizeService = DependencyService.Resolve<IScreenSizeService>();

            Query = initialQuery;
            QueryResults = new ObservableCollection<QueryResult>();

            Commands.Add("ExecuteQuery", new Command(async () =>
            {
                if (CanExecuteQuery == false)
                    return;

                _connectionString.Database = CurrentDatabase.Name;

                var result = await _neo4jService.ExecuteQuery(Query, _connectionString);

                if (result.Success)
                {
                    QueryResults.Insert(0, result);

                    MessagingCenter.Send(this, "ResetScroll");
                }

                else
                    await UserDialogs.Instance.AlertAsync(result.ErrorMessage);
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

        public void DeleteQueryResult(QueryResult queryResult)
        {
            var index = QueryResults.IndexOf(qr => qr.Id == queryResult.Id);

            QueryResults.RemoveAt(index);
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

        public bool CanExecuteQuery => !string.IsNullOrWhiteSpace(Query) && CurrentDatabase != null;

        #endregion
    }
}
