//
// License.cs
//
// Trevi Awater
// 15-03-2022
//
// © Xamarin.Neo4j
//

using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Xamarin.Neo4j.Models
{
    public class License
    {
        public string Name { get; set; }

        public string Repo { get; set; }

        public string LicenseText { get; set; }

        /// <summary>
        /// Opens the repository in the browser.
        /// </summary>
        public ICommand OpenRepo => new Command(() => Launcher.OpenAsync(new Uri(Repo)));
    }
}
