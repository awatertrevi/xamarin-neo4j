using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.Neo4j.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QueryBuilder : ContentView
    {
        public event EventHandler ExecuteQuery;

        public QueryBuilder()
        {
            InitializeComponent();

            editor.SetBinding(Editor.TextProperty, new Binding(nameof(Query), source: this));
        }

        #region Bindable Properties

        public string Query
        {
            get => (string)GetValue(QueryProperty);
            set => SetValue(QueryProperty, value);
        }

        public static BindableProperty QueryProperty = BindableProperty.Create("Query", typeof(string),
            typeof(QueryBuilder), string.Empty, BindingMode.TwoWay);

        #endregion

        private void InvokeExecuteQuery (object sender, EventArgs e)
        {
            ExecuteQuery?.Invoke(sender, e);
        }
    }
}
