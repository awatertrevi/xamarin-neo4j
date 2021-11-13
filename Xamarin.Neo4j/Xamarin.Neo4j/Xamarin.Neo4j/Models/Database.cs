//
// Database.cs
//
// Trevi Awater
// 13-11-2021
//
// © Xamarin.Neo4j
//

namespace Xamarin.Neo4j.Models
{
    public class Database
    {
        public string Name { get; set; }

        public string Status { get; set; }

        public bool Default { get; set; }

        public string DisplayName => Name + (Default ? " 🏠" : string.Empty);
    }
}
