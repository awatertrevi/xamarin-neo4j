//
// QueryHelper.cs
//
// Trevi Awater
// 02-03-2024
//
// © Xamarin.Neo4j
//

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Xamarin.Neo4j.Utilities
{
    public static class QueryHelper
    {
        public static string TransformQuery(string query)
        {
            // Check if there is a return statement, if not return the original query
            if (query.IndexOf("RETURN ", StringComparison.OrdinalIgnoreCase) <= 0) 
                return query;
            
            // This pattern is designed to match relationships with and without labels.
            // It captures the relationship direction and type, but not the existing label (if any).
            const string pattern = @"-\[:?(\w+)?\]->";
            var matches = Regex.Matches(query, pattern);
    
            // Keep track of unique relationships to ensure they are returned
            var relationships = new List<string>();
            var relationshipCounter = 1;
    
            var transformedQuery = query;
    
            foreach (Match match in matches)
            {
                var newLabel = $"display_variable_{relationshipCounter++}"; // Generate a unique label for the relationship
    
                // Replace the first occurrence of this match in the query with the new labeled version
                var oldPattern = match.Value;
                var newPattern = oldPattern.Replace("[:", $"[{newLabel}:").Replace("]->", "]->");
                var idx = transformedQuery.IndexOf(oldPattern);
                
                if (idx != -1)
                {
                    transformedQuery = transformedQuery.Substring(0, idx) + newPattern + transformedQuery.Substring(idx + oldPattern.Length);
                }
    
                relationships.Add(newLabel);
            }
    
            // Construct the new RETURN clause with the added relationships.
            if (relationships.Count > 0)
            {
                // Assuming there's always a RETURN statement to append to.
                var returnIndex = transformedQuery.LastIndexOf("RETURN ", StringComparison.OrdinalIgnoreCase);
                var returnClause = transformedQuery.Substring(returnIndex + "RETURN ".Length);
                var newReturnClause = string.Join(", ", relationships) + ", " + returnClause;
                
                transformedQuery = transformedQuery.Substring(0, returnIndex) + "RETURN " + newReturnClause;
            }
    
            return transformedQuery;
        }
    }
}
