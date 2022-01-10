using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Neo4j.Models;
using Xamarin.Neo4j.Pages;

namespace Xamarin.Neo4j.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QueryResultCell : ViewCell
    {
        public QueryResult QueryResult => (QueryResult) BindingContext;

        public QueryResultCell()
        {
            InitializeComponent();


        }

        private async void OpenNeovis(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new VisualizationPage(QueryResult));
        }
    }
}
