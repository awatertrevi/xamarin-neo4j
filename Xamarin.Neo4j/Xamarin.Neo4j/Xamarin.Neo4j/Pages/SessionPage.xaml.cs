using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Neo4j.Models;
using Xamarin.Neo4j.ViewModels;

namespace Xamarin.Neo4j.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SessionPage : ContentPage
    {
        public SessionPage(Neo4jConnectionString connectionString)
        {
            InitializeComponent();

            BindingContext = new SessionViewModel(Navigation, connectionString);
        }

        private void FocusDatabasePicker(object sender, EventArgs e)
        {
            databasePicker.Focus();
        }
    }
}
