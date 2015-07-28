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
        public void It_Should_Not_Error_On_Invalid_Truthy_Check()
        {
            var output = SystemData.DataTableTruthyCheck("Foo");

            Assert.Null(output);
        }
    }
}
