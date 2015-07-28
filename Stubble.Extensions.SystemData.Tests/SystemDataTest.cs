using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stubble.Core;
using Xunit;

namespace Stubble.Extensions.SystemData.Tests
{
    public class SystemDataTest
    {
        [Fact]
        public void It_Should_See_Empty_DataTables_As_Falsey()
        {
            var stubble = new StubbleBuilder().AddSystemData().Build();

            var output = stubble.Render("{{#foo}}I'm in foo{{/foo}}", new {foo = new DataTable()});
            Assert.Equal("", output);
        }

        [Fact]
        public void It_Should_See_Empty_DataSets_As_Falsey()
        {
            var stubble = new StubbleBuilder().AddSystemData().Build();

            var output = stubble.Render("{{#foo}}I'm in foo{{/foo}}", new { foo = new DataSet() });
            Assert.Equal("", output);
        }

        [Fact]
        public void It_Should_Be_Able_To_Retrieve_Values_From_DataRows()
        {
            var dt = new DataTable();
            dt.Columns.Add("IntColumn", typeof(int));
            dt.Rows.Add(1);

            Assert.Equal(1, SystemData.DataRowGetter(dt.Rows[0], "IntColumn"));
        }

        [Fact]
        public void It_Should_Not_Error_On_DataRow_Value_Miss()
        {
            var dt = new DataTable();
            dt.Columns.Add("IntColumn", typeof(int));
            dt.Rows.Add(1);

            Assert.Null(SystemData.DataRowGetter(dt.Rows[0], "IntColumns"));
        }
    }
}
