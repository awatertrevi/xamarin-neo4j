//
// Query.cs
//
// Trevi Awater
// 11-01-2022
//
// © Xamarin.Neo4j
//

using System;

namespace Xamarin.Neo4j.Models
{
    public class Query
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string QueryText { get; set; }
    }
}
