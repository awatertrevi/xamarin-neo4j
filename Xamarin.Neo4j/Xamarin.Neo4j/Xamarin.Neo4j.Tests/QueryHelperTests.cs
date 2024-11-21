using NUnit.Framework;
using Xamarin.Neo4j.Utilities;

namespace Xamarin.Neo4j.Tests
{
    public class QueryHelperTests
    {
        [Test]
        public void ConvertsQueryWithUnlabeledRelationshipToAddDisplayVariable()
        {
            const string query = "MATCH (t:Tender)-[]->(z) RETURN t, z";
            const string expected = "MATCH (t:Tender)-[display_variable_1]->(z) RETURN display_variable_1, t, z";

            var actual = QueryHelper.ToDisplayQuery(query);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ConvertsLabeledRelationshipWithoutAliasToAddDisplayVariable()
        {
            const string query = "MATCH (t:Tender)-[:LAST]->(z) RETURN t, z";
            const string expected = "MATCH (t:Tender)-[display_variable_1:LAST]->(z) RETURN display_variable_1, t, z";

            var actual = QueryHelper.ToDisplayQuery(query);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void LeavesLabeledAndAliasedRelationshipQueryUnchanged()
        {
            const string query = "MATCH (t:Tender)-[y:LAST]->(z) RETURN t, z, y";
            var expected = query;

            var actual = QueryHelper.ToDisplayQuery(query);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ConvertsLabeledRelationshipWithReturnAllToAddDisplayVariable()
        {
            const string query = "MATCH (t:Tender)-[:LAST]->(z) RETURN *";
            const string expected = "MATCH (t:Tender)-[display_variable_1:LAST]->(z) RETURN *";

            var actual = QueryHelper.ToDisplayQuery(query);

            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void ConvertsUnlabeledRelationshipWithReturnAllToAddDisplayVariable()
        {
            const string query = "MATCH (t:Tender)-[]->(z) RETURN *";
            const string expected = "MATCH (t:Tender)-[display_variable_1]->(z) RETURN *";

            var actual = QueryHelper.ToDisplayQuery(query);

            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void ConvertsMultipleLabeledRelationshipsWithReturnAllToAddDisplayVariables()
        {
            const string query = "MATCH (t:Tender)-[:LAST]->(z)-[:NEXT]->(x) RETURN *";
            const string expected = "MATCH (t:Tender)-[display_variable_1:LAST]->(z)-[display_variable_2:NEXT]->(x) RETURN *";

            var actual = QueryHelper.ToDisplayQuery(query);

            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void LeavesQueryWithNoReturnClauseUnchanged()
        {
            const string query = "MATCH (t:Tender)-[:LAST]->(z)";
            var expected = query;

            var actual = QueryHelper.ToDisplayQuery(query);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ConvertsMultipleUnlabeledRelationshipsToAddDisplayVariables()
        {
            const string query = "MATCH (t:Tender)-[]->(z)-[]->(x) RETURN t, z, x";
            const string expected =
                "MATCH (t:Tender)-[display_variable_1]->(z)-[display_variable_2]->(x) RETURN display_variable_2, display_variable_1, t, z, x";

            var actual = QueryHelper.ToDisplayQuery(query);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ConvertsBackwardsRelationshipToAddDisplayVariable()
        {
            const string query = "MATCH (tv:TenderVersion)<-[:HAS_VERSION]-(t:Tender) RETURN tv, t";
            const string expected =
                "MATCH (tv:TenderVersion)<-[display_variable_1:HAS_VERSION]-(t:Tender) RETURN display_variable_1, tv, t";

            var actual = QueryHelper.ToDisplayQuery(query);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ConvertsMultipleLabeledRelationshipsToAddDisplayVariables()
        {
            const string query = "MATCH (t:Tender)-[:LAST]->(z)-[:NEXT]->(x) RETURN t, z, x";
            const string expected =
                "MATCH (t:Tender)-[display_variable_1:LAST]->(z)-[display_variable_2:NEXT]->(x) RETURN display_variable_2, display_variable_1, t, z, x";

            var actual = QueryHelper.ToDisplayQuery(query);

            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void LeavesQueryWithMultipleLabeledRelationshipsAndReturnAllWithAliasUnchanged()
        {
            const string query = "MATCH (t:Tender)-[y:LAST]->(z)-[x:NEXT]->(x) RETURN *";
            var expected = query;

            var actual = QueryHelper.ToDisplayQuery(query);

            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void LeavesComplexQueryWithMultipleRelationshipsAndAliasesUnchanged()
        {
            const string query = "MATCH (t:Tender)-[y:LAST]->(z)-[x:NEXT]->(x)-[]->(y) RETURN *";
            const string expected = "MATCH (t:Tender)-[y:LAST]->(z)-[x:NEXT]->(x)-[display_variable_1]->(y) RETURN *";

            var actual = QueryHelper.ToDisplayQuery(query);

            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void AdjustsComplexQueryWithLimitToAddDisplayVariable()
        {
            const string query = "MATCH (t:Tender)-[y:LAST]->(z)-[x:NEXT]->(x)-[]->(y) RETURN * LIMIT 10";
            const string expected = "MATCH (t:Tender)-[y:LAST]->(z)-[x:NEXT]->(x)-[display_variable_1]->(y) RETURN * LIMIT 10";

            var actual = QueryHelper.ToDisplayQuery(query);

            Assert.AreEqual(expected, actual);
        }
    }
}
