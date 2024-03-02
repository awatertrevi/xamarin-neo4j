using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Neo4j.Models;
using Xamarin.Neo4j.Utilities;
using Xamarin.Neo4j.ViewModels;

namespace Xamarin.Neo4j.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SessionPage : ContentPage
    {
        private bool _canCompleteEntry = true;
        
        private SessionViewModel ViewModel => (SessionViewModel) BindingContext;

        public SessionPage(Neo4jConnectionString connectionString, string initialQuery = null)
        {
            InitializeComponent();

            BindingContext = new SessionViewModel(Navigation, connectionString, initialQuery);

            MessagingCenter.Subscribe<SessionViewModel>(this, "ResetScroll", (sender) =>
            {
                resultsCollection.ScrollTo(0, 0, ScrollToPosition.Start, true);
            });
        }

        private void FocusDatabasePicker(object sender, EventArgs e)
        {
            databasePicker.Focus();
        }

        private void CloseResultView(object sender, GenericEventArgs<QueryResult> e)
        {
            ViewModel.DeleteQueryResult(e.Data);
        }

        private void ExecuteQuery(object sender, EventArgs e)
        {
            ViewModel.Commands["ExecuteQuery"].Execute(null);
        }
    }
}
