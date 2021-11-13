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

        public int Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool Encrypted { get; set; }
    }
}
