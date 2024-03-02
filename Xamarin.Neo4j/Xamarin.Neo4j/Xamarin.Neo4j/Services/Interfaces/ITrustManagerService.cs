using Neo4j.Driver;

namespace Xamarin.Neo4j.Services.Interfaces
{
    public interface ITrustManagerService
    {
        TrustManager GetNativeTrustManager();
    }
}
