//
// AddConnectionViewModel.cs
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

namespace Xamarin.Neo4j.ViewModels
{
    public class AddConnectionViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private bool _encrypted;

        private string _host, _username, _password;

        private int _port;

        public AddConnectionViewModel(INavigation navigation) : base(navigation)
        {
            InitializeDefaultValues();
        }

        private void InitializeDefaultValues()
        {
            Username = "neo4j";
            Port = 7687;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
