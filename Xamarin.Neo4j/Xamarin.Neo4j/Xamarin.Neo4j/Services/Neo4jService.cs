//
// Neo4jService.cs
//
// Trevi Awater
// 13-11-2021
//
// © Xamarin.Neo4j
//

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Neo4j.Driver;
using Neo4jClient;
using Neo4jClient.Cypher;
using Xamarin.Forms;
using Xamarin.Neo4j.Models;
using Xamarin.Neo4j.Services;
[assembly: Dependency(typeof(Neo4jService))]
namespace Xamarin.Neo4j.Services
{
    public class Neo4jService
    {
        private BoltGraphClient GraphClient { get; set; }

        public async Task<bool> EstablishConnection(Neo4jConnectionString connectionString)
        {
            try
            {
                var boltUri = $"bolt://{connectionString.Host}:{connectionString.Port}";

                var driver = GraphDatabase.Driver(boltUri,
                    AuthTokens.Basic(connectionString.Username, connectionString.Password),
                    builder =>
                    {
                        builder.WithEncryptionLevel(connectionString.Encrypted
                            ? EncryptionLevel.Encrypted
                            : EncryptionLevel.None);
                    });

                GraphClient = new BoltGraphClient(driver);

                await GraphClient.ConnectAsync();
            }

            catch (ServiceUnavailableException)
            {
                return false;
            }

            catch (AuthenticationException)
            {
                return false;
            }

            catch (UriFormatException)
            {
                return false;
            }

            return true;
        }

        public async Task<List<Database>> LoadDatabases()
        {
            var session = GraphClient.Driver.AsyncSession(d => d.WithDatabase("system"));
            var cursor = await session.RunAsync("SHOW DATABASES;");

            var databases = new List<Database>();

            while (await cursor.FetchAsync())
            {
                var name = cursor.Current.Values["name"].As<string>();
                var status = cursor.Current.Values["currentStatus"].As<string>();
                var @default = cursor.Current.Values["default"].As<bool>();

                databases.Add(new Database()
                {
                    Name = name,
                    Status = status,
                    Default = @default
                });
            }

            return databases;
        }

        public async Task<QueryResult> ExecuteQuery(string query, Neo4jConnectionString connectionString)
        {
            var results = new Dictionary<string, List<object>>();

            var canDisplayGraph = false;

            try
            {
                var session = GraphClient.Driver.AsyncSession(d => d.WithDatabase(connectionString.Database));
                var cursor = await session.RunAsync(query);

                while (await cursor.FetchAsync())
                {
                    foreach (var key in cursor.Current.Keys)
                        if (results.ContainsKey(key) == false)
                            results.Add(key, new List<object>());

                    foreach (var record in  cursor.Current.Values)
                    {
                        var value = record.Value;

                        switch (value)
                        {
                            case INode _:
                                canDisplayGraph = true;
                                results[record.Key].Add(value.As<INode>());
                                break;

                            case IRelationship _:
                                results[record.Key].Add(value.As<IRelationship>());
                                break;

                            default:
                                results[record.Key].Add(value);
                                break;
                        }
                    }
                }

                return new QueryResult()
                {
                    Id = Guid.NewGuid(),
                    Success = true,
                    CanDisplayGraph = canDisplayGraph,
                    Query = query,
                    ConnectionString = connectionString,
                    Results = results
                };
            }

            catch (ClientException e)
            {
                return new QueryResult()
                {
                    Id = Guid.NewGuid(),
                    Success = false,
                    Query = query,
                    ConnectionString = connectionString,
                    ErrorMessage = e.Message
                };
            }
        }
    }
}
