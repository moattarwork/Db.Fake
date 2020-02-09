using System.Linq;
using System.Threading.Tasks;
using DbFake.SchemaReader.API.Models;
using DbFake.SchemaReader.API.Services;
using FluentAssertions;
using Xunit;

namespace DbFake.SchemaReader.API.UnitTests.Models
{
    public class SqlDbSchemaReaderTests
    {
        private const string Connection = "Data Source=localhost,1433;Persist Security Info=True;User ID=sa;Password=Password123!";

        [Fact]
        public async Task Should_GetDatabasesAsync_ReturnsListOfAllDatabasesForAGivenConnectionString()
        {
            // ARRANGE
            var sut = new SqlDbSchemaReader();

            // ACT
            var actual = (await sut.GetDatabasesAsync(new ConnectionInfo(Connection))).ToList();


            // ASSERT
            actual.Should().NotBeNull().And.HaveCount(1);
            actual[0].Name.Should().Be("Sample_Database");
        }        
        
        [Fact]
        public async Task Should_GetDatabaseTablesAsync_ReturnsListOfAllTablesForAGivenConnectionStringAndDatabase()
        {
            // ARRANGE
            var sut = new SqlDbSchemaReader();

            // ACT
            var actual = (await sut.GetDatabaseTablesAsync(new ConnectionInfo(Connection), "Sample_Database")).ToList();

            // ASSERT
            actual.Should().NotBeNull()
                .And.HaveCount(2)
                .And.Contain(m => m.TableName == "Table1" && m.SchemaName == "dbo" && m.Fields.Length == 4)
                .And.Contain(m => m.TableName == "Table2" && m.SchemaName == "dbo" && m.Fields.Length == 3);
        }
    }
}
