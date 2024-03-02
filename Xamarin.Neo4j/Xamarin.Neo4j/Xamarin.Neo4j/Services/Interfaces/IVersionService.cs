namespace Xamarin.Neo4j.Services.Interfaces
{
    public interface IVersionService
    {
        string GetVersion();
        
        string GetBuild();
    }
}
