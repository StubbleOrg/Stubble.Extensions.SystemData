using System.Data;
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

        [Fact]
        public void It_Should_Not_Error_On_Table_Miss()
        {
            var dt = new DataTable("TableA");
            var dt2 = new DataTable("TableB");
            var ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dt2);

            Assert.Null(SystemData.DataSetGetter(ds, "TableC"));
        }

        [Fact]
        public void It_Should_Treat_DataTable_As_List()
        {
            var dt = new DataTable();
            dt.Columns.Add("IntColumn", typeof(int));
            dt.Rows.Add(1);
            dt.Rows.Add(2);
            dt.Rows.Add(3);

            var stubble = new StubbleBuilder().AddSystemData().Build();

            var output = stubble.Render("{{#foo}}{{IntColumn}}{{/foo}}", new { foo = dt });
            Assert.Equal("123", output);
        }

        [Fact]
        public void It_Should_Be_Able_To_Find_DataSet_Tables_By_Name()
        {
            var dt = new DataTable("TableA");
            dt.Columns.Add("IntColumn", typeof(int));
            dt.Rows.Add(1);
            dt.Rows.Add(2);
            dt.Rows.Add(3);

            var dt2 = new DataTable("TableB");
            dt2.Columns.Add("IntColumn", typeof(int));
            dt2.Rows.Add(3);
            dt2.Rows.Add(2);
            dt2.Rows.Add(1);

            var ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dt2);
            var stubble = new StubbleBuilder().AddSystemData().Build();

            var output = stubble.Render("{{#foo.TableA}}{{IntColumn}}{{/foo.TableA}}-{{#foo.TableB}}{{IntColumn}}{{/foo.TableB}}", new { foo = ds });
            Assert.Equal("123-321", output);
        }

        [Fact]
        public void It_Should_Be_Able_To_Access_DataTables_In_DataSet_By_Index()
        {
            var dt = new DataTable();
            dt.Columns.Add("IntColumn", typeof(int));
            dt.Rows.Add(1);
            dt.Rows.Add(2);
            dt.Rows.Add(3);

            var dt2 = new DataTable();
            dt2.Columns.Add("IntColumn", typeof(int));
            dt2.Rows.Add(3);
            dt2.Rows.Add(2);
            dt2.Rows.Add(1);

            var ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dt2);
            var stubble = new StubbleBuilder().AddSystemData().Build();

            var output = stubble.Render("{{#foo.0}}{{IntColumn}}{{/foo.0}}-{{#foo.1}}{{IntColumn}}{{/foo.1}}", new { foo = ds });
            Assert.Equal("123-321", output);
        }

        [Fact]
        public void It_Should_Enumerate_Through_DataSet()
        {
            var dt = new DataTable("TableA");
            dt.Columns.Add("IntColumn", typeof(int));
            dt.Rows.Add(1);
            dt.Rows.Add(2);
            dt.Rows.Add(3);

            var dt2 = new DataTable("TableB");
            dt2.Columns.Add("IntColumn", typeof(int));
            dt2.Rows.Add(3);
            dt2.Rows.Add(2);
            dt2.Rows.Add(1);

            var ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dt2);
            var stubble = new StubbleBuilder().AddSystemData().Build();

            var output = stubble.Render("{{#foo}}{{#.}}{{IntColumn}}{{/.}}{{/foo}}", new { foo = ds });
            Assert.Equal("123321", output);
        }
    }
}
