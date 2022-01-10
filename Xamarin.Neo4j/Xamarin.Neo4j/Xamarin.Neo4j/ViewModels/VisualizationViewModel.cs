//
// VisualizationViewModel.cs
//
// Trevi Awater
// 10-01-2022
//
// © Xamarin.Neo4j
//

using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Neo4j.Annotations;
using Xamarin.Neo4j.Models;

namespace Xamarin.Neo4j.ViewModels
{
    public class VisualizationViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private HtmlWebViewSource _source;

        public VisualizationViewModel(INavigation navigation, QueryResult queryResult) : base(navigation)
        {
            ParseNeovisHtml(queryResult);
        }

        private void ParseNeovisHtml(QueryResult queryResult)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Xamarin.Neo4j.Visualization.neovis.html";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                var result = reader.ReadToEnd();

                result = result.Replace("{{host}}", queryResult.ConnectionString.Host);
                result = result.Replace("{{port}}", queryResult.ConnectionString.Port.ToString());
                result = result.Replace("{{database}}", queryResult.ConnectionString.Database);
                result = result.Replace("{{username}}", queryResult.ConnectionString.Username);
                result = result.Replace("{{password}}", queryResult.ConnectionString.Password);
                result = result.Replace("{{encryption}}", queryResult.ConnectionString.Encrypted ? "ENCRYPTION_ON" : "ENCRYPTION_OFF");
                result = result.Replace("{{query}}", queryResult.Query);

                Source = new HtmlWebViewSource
                {
                    Html = result
                };
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Bindable Properties

        public HtmlWebViewSource Source
        {
            get => _source;

            set
            {
                _source = value;

                OnPropertyChanged();
            }
        }

        #endregion
    }
}
