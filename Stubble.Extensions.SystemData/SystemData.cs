using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stubble.Core.Interfaces;

namespace Stubble.Extensions.SystemData
{
    public static class SystemData
    {
        public static IStubbleBuilder AddSystemData(this IStubbleBuilder builder)
        {
            builder.AddTruthyCheck(DataTableTruthyCheck);
            return builder;
        }

        private static bool? DataTableTruthyCheck(object obj)
        {
            var table = obj as DataTable;
            if (table != null)
            {
                return table.Rows.Count > 0;
            }
            return null;
        }
    }
}
