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
        public static string ToDisplayQuery(string query)
        {
            // Check if there is a return statement, if not return the original query
            if (query.IndexOf("RETURN ", StringComparison.OrdinalIgnoreCase) <= 0) 
                return query;
    
            // Adjusted pattern to match relationships with and without labels/types.
            const string pattern = @"-\[(\w*:?(\w+)?)\]->";
            var matches = Regex.Matches(query, pattern);

            var relationships = new List<string>();
            var relationshipCounter = 1;

            var transformedQuery = query;

            foreach (Match match in matches)
            {
                var newLabel = $"display_variable_{relationshipCounter++}"; // Generate a unique label for the relationship

                // Determine the correct new pattern based on whether the original had a type/label
                var oldPattern = match.Value;
                var newPattern = !string.IsNullOrEmpty(match.Groups[1].Value) ?
                    oldPattern.Replace("[:", $"[{newLabel}:").Replace("]->", "]->") : // For labeled/types
                    oldPattern.Replace("[]", $"[{newLabel}]"); // For unlabeled

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
                var returnIndex = transformedQuery.LastIndexOf("RETURN ", StringComparison.OrdinalIgnoreCase);
                var returnClause = transformedQuery.Substring(returnIndex + "RETURN ".Length);
                var newReturnClause = string.Join(", ", relationships) + ", " + returnClause;
        
                transformedQuery = transformedQuery.Substring(0, returnIndex) + "RETURN " + newReturnClause;
            }

            return transformedQuery;
        }
    }
}
