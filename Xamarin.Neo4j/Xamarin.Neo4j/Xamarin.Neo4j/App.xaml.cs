using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Neo4j.Pages;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Xamarin.Neo4j
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new RootPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
