//
// TrustManagerService.cs
//
// Trevi Awater
// 14-03-2022
//
// © Xamarin.Neo4j.iOS
//

using Neo4j.Driver;
using Xamarin.Forms;
using Xamarin.Neo4j.iOS.Services;
using Xamarin.Neo4j.Services;

[assembly: Dependency(typeof(TrustManagerService))]
namespace Xamarin.Neo4j.iOS.Services
{
    public class TrustManagerService : ITrustManagerService
    {
        public TrustManager GetNativeTrustManager()
        {
            return new NativeTrustManager();
        }
    }
}
