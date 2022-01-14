using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Neo4j.Pages;
using Xamarin.Neo4j.Themes;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Xamarin.Neo4j
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SetTheme(Current.RequestedTheme);

            MainPage = new NavigationPage(new RootPage());
        }


        private void SetTheme(OSAppTheme theme)
        {
            Resources = theme switch
            {
                OSAppTheme.Dark => new DarkTheme(),
                OSAppTheme.Light => new LightTheme(),

                _ => new LightTheme()
            };
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
