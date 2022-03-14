using Neo4j.Driver;

namespace Xamarin.Neo4j.Services
{
    public interface ITrustManagerService
    {
        TrustManager GetNativeTrustManager();
    }
}
