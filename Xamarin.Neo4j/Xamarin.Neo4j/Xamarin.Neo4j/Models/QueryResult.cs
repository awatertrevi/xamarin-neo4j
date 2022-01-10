//
// QueryResult.cs
//
// Trevi Awater
// 13-11-2021
//
// © Xamarin.Neo4j
//

using System.Collections;
using System.Collections.Generic;
using Neo4j.Driver;

namespace Xamarin.Neo4j.Models
{
    public class QueryResult
    {
        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public string Query { get; set; }

        public Neo4jConnectionString ConnectionString { get; set; }

        public IEnumerable<INode> Nodes { get; set; }

        public IEnumerable<IRelationship> Relationships { get; set; }
    }
}
