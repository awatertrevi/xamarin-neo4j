using NUnit.Framework;
using Xamarin.Neo4j.Utilities;

namespace Xamarin.Neo4j.Tests
{
    public class QueryHelperTests
    {
        [Test]
        public void TestWithUnlabeledRelationship()
        {
            const string query = "MATCH (t:Tender)-[]->(z) RETURN t, z";
            const string expected = "MATCH (t:Tender)-[display_variable_1]->(z) RETURN display_variable_1, t, z";

            var actual = QueryHelper.ToDisplayQuery(query);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestWithLabeledWithoutAliasRelationship()
        {
            const string query = "MATCH (t:Tender)-[:LAST]->(z) RETURN t, z";
            const string expected = "MATCH (t:Tender)-[display_variable_1:LAST]->(z) RETURN display_variable_1, t, z";

            var actual = QueryHelper.ToDisplayQuery(query);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestWithLabeledAndAliasedRelationship()
        {
            const string query = "MATCH (t:Tender)-[y:LAST]->(z) RETURN t, z, y";
            var expected = query;

            var actual = QueryHelper.ToDisplayQuery(query);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestWithReturnAll()
        {
            const string query = "MATCH (t:Tender)-[:LAST]->(z) RETURN *";
            const string expected = "MATCH (t:Tender)-[display_variable_1:LAST]->(z) RETURN *";

            var actual = QueryHelper.ToDisplayQuery(query);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestWithNoReturn()
        {
            const string query = "MATCH (t:Tender)-[:LAST]->(z)";
            var expected = query;

            var actual = QueryHelper.ToDisplayQuery(query);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestWithMultipleUnlabeledRelationships()
        {
            const string query = "MATCH (t:Tender)-[]->(z)-[]->(x) RETURN t, z, x";
            const string expected =
                "MATCH (t:Tender)-[display_variable_1]->(z)-[display_variable_2]->(x) RETURN display_variable_2, display_variable_1, t, z, x";

            var actual = QueryHelper.ToDisplayQuery(query);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestBackwardsDirection()
        {
            const string query = "MATCH (tv:TenderVersion)<-[:HAS_VERSION]-(t:Tender) RETURN tv, t";
            const string expected =
                "MATCH (tv:TenderVersion)<-[display_variable_1:HAS_VERSION]-(t:Tender) RETURN display_variable_1, tv, t";

            var actual = QueryHelper.ToDisplayQuery(query);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestWithMultipleLabeledRelationships()
        {
            const string query = "MATCH (t:Tender)-[:LAST]->(z)-[:NEXT]->(x) RETURN t, z, x";
            const string expected =
                "MATCH (t:Tender)-[display_variable_1:LAST]->(z)-[display_variable_2:NEXT]->(x) RETURN display_variable_2, display_variable_1, t, z, x";

            var actual = QueryHelper.ToDisplayQuery(query);

            Assert.AreEqual(expected, actual);
        }
    }
}
