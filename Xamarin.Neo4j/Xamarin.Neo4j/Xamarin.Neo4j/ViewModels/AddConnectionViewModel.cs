//
// AddConnectionViewModel.cs
//
// Trevi Awater
// 13-11-2021
//
// © Xamarin.Neo4j
//

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Acr.UserDialogs;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Neo4j.Annotations;
using Xamarin.Neo4j.Managers;
using Xamarin.Neo4j.Models;
using Xamarin.Neo4j.Pages;
using Xamarin.Neo4j.Services;

namespace Xamarin.Neo4j.ViewModels
{
    public class AddConnectionViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private bool _encrypted;

        private string _host, _username, _password;

        private int _port;

        private readonly Neo4jService _neo4jService;

        public event PropertyChangedEventHandler PropertyChanged;

        public AddConnectionViewModel(INavigation navigation) : base(navigation)
        {
            InitializeDefaultValues();

            _neo4jService = DependencyService.Resolve<Neo4jService>();

            Commands.Add("Test", new Command(async () =>
            {
                // TODO: Validate input.

                var connectionString = BuildConnectionString();

                var couldConnect = await _neo4jService.EstablishConnection(connectionString);

                await UserDialogs.Instance.AlertAsync(couldConnect ? "Connection successful." : "Connection failed.");
            }));

            Commands.Add("Save", new Command(async () =>
            {
                // TODO: Validate input.

                var connectionString = BuildConnectionString();

                var namePromptResult = await UserDialogs.Instance.PromptAsync("How do you want to name this connection?", "Save Connection", "Save", "Cancel");

                if (!namePromptResult.Ok || string.IsNullOrWhiteSpace(namePromptResult.Value))
                    return;

                connectionString.Name = namePromptResult.Value;

                await ConnectionStringManager.AddConnectionString(connectionString);

                await Navigation.PopAsync();
            }));

            Commands.Add("Connect", new Command(async () =>
            {
                // TODO: Validate input.

                var connectionString = BuildConnectionString();

                await Navigation.PushAsync(new SessionPage(connectionString));
            }));
        }

        private void InitializeDefaultValues()
        {
            Username = "neo4j";
            Port = 7687;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Neo4jConnectionString BuildConnectionString()
        {
            return new Neo4jConnectionString
            {
                Id = Guid.NewGuid(),
                Host = Host,
                Port = Port,
                Username = Username,
                Password = Password,
                Encrypted = Encrypted
            };
        }

        #region Bindable Properties

        public bool Encrypted
        {
            get => _encrypted;

            set
            {
                _encrypted = value;

                OnPropertyChanged(nameof(Encrypted));
            }
        }

        public string Host
        {
            get => _host;

            set
            {
                _host = value;

                OnPropertyChanged(nameof(Host));
            }
        }

        public int Port
        {
            get => _port;

            set
            {
                _port = value;

                OnPropertyChanged(nameof(Port));
            }
        }

        public string Username
        {
            get => _username;

            set
            {
                _username = value;

                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get => _password;

            set
            {
                _password = value;

                OnPropertyChanged(nameof(Password));
            }
        }

        #endregion
    }
}
