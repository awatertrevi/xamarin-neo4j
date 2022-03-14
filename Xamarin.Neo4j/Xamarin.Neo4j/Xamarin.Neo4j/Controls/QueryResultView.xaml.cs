using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Neo4j.Managers;
using Xamarin.Neo4j.Models;
using Xamarin.Neo4j.Pages;
using Xamarin.Neo4j.Utilities;

namespace Xamarin.Neo4j.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QueryResultView : ContentView
    {
        private QueryResult QueryResult => (QueryResult) BindingContext;

        public event EventHandler<GenericEventArgs<QueryResult>> CloseRequested;

        private string _neovisHtml;

        public QueryResultView()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(BindingContext))
            {
                ParseNeovisHtml();

                graphView.Source = new HtmlWebViewSource()
                {
                    Html = _neovisHtml
                };
            }
        }

        private void ParseNeovisHtml()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Xamarin.Neo4j.Visualization.neovis.html";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                var (url, isEncrypted, ignoreTrust) = QueryResult.ConnectionString.ParseHost();

                var result = reader.ReadToEnd();

                result = result.Replace("{{host}}", url);
                result = result.Replace("{{database}}", QueryResult.ConnectionString.Database);
                result = result.Replace("{{username}}", QueryResult.ConnectionString.Username);
                result = result.Replace("{{password}}", QueryResult.ConnectionString.Password);
                result = result.Replace("{{encryption}}", isEncrypted ? "ENCRYPTION_ON" : "ENCRYPTION_OFF");
                result = result.Replace("{{trust}}", ignoreTrust ? "TRUST_ALL_CERTIFICATES" : "TRUST_SYSTEM_CA_SIGNED_CERTIFICATES");
                result = result.Replace("{{query}}", QueryResult.Query);
                result = result.Replace("{{backgroundColor}}", App.Current.RequestedTheme == OSAppTheme.Dark ? "#292C31" : "#FFFFFF");

                _neovisHtml = result;
            }
        }


        private async void OpenNeovis(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new GraphPage(_neovisHtml));
        }

        private async void SaveQuery(object sender, EventArgs e)
        {
            var queryNameResult = await UserDialogs.Instance.PromptAsync("How should the query be called?");

            if (queryNameResult.Ok)
            {
                var query = new Query()
                {
                    Id = Guid.NewGuid(),
                    QueryText = QueryResult.Query,
                    Name = queryNameResult.Value,
                };

                SavedQueryManager.AddSavedQuery(query);
            }
        }

        private void CloseResultView(object sender, EventArgs e)
        {
            CloseRequested?.Invoke(sender, new GenericEventArgs<QueryResult>(QueryResult));
        }

        private async void OpenTableView(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new TablePage(QueryResult));
        }
    }
}
