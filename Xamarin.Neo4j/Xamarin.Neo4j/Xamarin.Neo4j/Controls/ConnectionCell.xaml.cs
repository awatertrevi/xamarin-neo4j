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
    public partial class ConnectionCell : ViewCell
    {
        public ConnectionCell()
        {
            InitializeComponent();

            isActiveIndicator.SetBinding(VisualElement.IsVisibleProperty, new Binding(nameof(IsActive), source: this));
        }

        #region Bindable Properties

        public bool IsActive
        {
            get => (bool)GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }

        public static BindableProperty IsActiveProperty = BindableProperty.Create("IsActive", typeof(bool),
            typeof(ConnectionCell), false, BindingMode.OneWay);

        #endregion
    }
}
