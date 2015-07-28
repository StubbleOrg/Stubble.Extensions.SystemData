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
            builder.AddValueGetter(typeof (DataRow), DataRowGetter);
            return builder;
        }

        internal static bool? DataTableTruthyCheck(object obj)
        {
            var table = obj as DataTable;
            if (table != null)
            {
                return table.Rows.Count > 0;
            }
            return null;
        }

        internal static object DataRowGetter(object value, string key)
        {
            var dataRow = value as DataRow;

            if (dataRow != null && dataRow.Table.Columns.Contains(key))
            {
                return dataRow[key];
            }
            return null;
        }
    }
}
