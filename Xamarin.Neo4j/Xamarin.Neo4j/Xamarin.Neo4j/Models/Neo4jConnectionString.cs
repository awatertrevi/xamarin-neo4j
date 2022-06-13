//
// Neo4jConnectionString.cs
//
// Trevi Awater
// 13-11-2021
//
// © Xamarin.Neo4j
//

using System;

namespace Xamarin.Neo4j.Models
{
    public class Neo4jConnectionString
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Database { get; set; }

        public string Host { get; set; }

        public string Scheme { get; set; }
        
        public string Username { get; set; }

        public string Password { get; set; }

        public Tuple<string, bool, bool> ParseHost()
        {
            var fullHost = Scheme + Host;
            
            if (fullHost.StartsWith("neo4j://"))
                return new Tuple<string, bool, bool>(fullHost, false, false);

            if (fullHost.StartsWith("bolt://"))
                return new Tuple<string, bool, bool>(fullHost.Replace("bolt://", "neo4j://"), false, false);

            if (fullHost.StartsWith("neo4j+s://"))
                return new Tuple<string, bool, bool>(fullHost.Replace("neo4j+s://", "neo4j://"), true, false);

            if (fullHost.StartsWith("neo4j+ssc://"))
                return new Tuple<string, bool, bool>(fullHost.Replace("neo4j+ssc://", "neo4j://"), true, true);

            throw new NotSupportedException("Unknown protocol.");
        }
    }
}
