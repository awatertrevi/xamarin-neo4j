using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Xamarin.Neo4j.Utilities
{
    public static class QueryHelper
    {
        public static string ToDisplayQuery(string query)
        {
            if (query.IndexOf("RETURN ", StringComparison.OrdinalIgnoreCase) < 0)
                return query;

            const string pattern = @"(-\[(\w*:?(\w+)?)\]->)|(<-\[(\w*:?(\w+)?)\]-)";
            var matches = Regex.Matches(query, pattern);

            var relationships = new List<string>();
            var relationshipCounter = 1;

            var transformedQuery = query;

            foreach (Match match in matches)
            {
                if (string.IsNullOrEmpty(match.Groups[2].Value) || string.IsNullOrEmpty(match.Groups[2].Value.Split(':')[0]))
                {
                    var newLabel = $"display_variable_{relationshipCounter++}";
                    var oldPattern = match.Value;
                    var newPattern = "";

                    if (match.Groups[1].Success)
                    {
                        newPattern = $"-[{newLabel + match.Groups[2]}]->";
                    }
                    else if (match.Groups[5].Success)
                    {
                        newPattern = $"<-[{newLabel + match.Groups[5]}]-";
                    }

                    var idx = transformedQuery.IndexOf(oldPattern);

                    if (idx != -1)
                    {
                        transformedQuery = transformedQuery.Substring(0, idx) + newPattern + transformedQuery.Substring(idx + oldPattern.Length);
                    }

                    relationships.Add(newLabel);
                }
                else if (match.Groups[2].Value.Contains(":"))
                {
                    var label = match.Groups[2].Value.Split(':')[0];
                    if (!relationships.Contains(label))
                    {
                        relationships.Add(label);
                    }
                }
            }

            if (relationships.Count > 0)
            {
                var returnIndex = transformedQuery.LastIndexOf("RETURN ", StringComparison.OrdinalIgnoreCase);
                var returnClause = transformedQuery.Substring(returnIndex + "RETURN ".Length);

                if (!returnClause.Trim().StartsWith("*"))
                {
                    var newReturnClause = returnClause;
                    foreach (var rel in relationships)
                    {
                        if (!returnClause.Contains(rel))
                        {
                            newReturnClause = rel + ", " + newReturnClause;
                        }
                    }

                    transformedQuery = transformedQuery.Substring(0, returnIndex) + "RETURN " + newReturnClause;
                }
            }

            return transformedQuery;
        }
    }
}
