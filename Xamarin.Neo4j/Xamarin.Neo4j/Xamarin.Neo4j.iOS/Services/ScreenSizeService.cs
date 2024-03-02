//
// ScreenSizeService.cs
//
// Trevi Awater
// 13-01-2022
//
// © Xamarin.Neo4j.iOS
//

using UIKit;
using Xamarin.Forms;
using Xamarin.Neo4j.iOS.Services;
using Xamarin.Neo4j.Services;
using Xamarin.Neo4j.Services.Interfaces;

[assembly: Dependency(typeof(ScreenSizeService))]
namespace Xamarin.Neo4j.iOS.Services
{
    public class ScreenSizeService : IScreenSizeService
    {
        public int GetScreenHeight()
        {
            return (int)UIScreen.MainScreen.Bounds.Height;
        }

        public int GetScreenWidth()
        {
            return (int)UIScreen.MainScreen.Bounds.Width;
        }
    }
}
