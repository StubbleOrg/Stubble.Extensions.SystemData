using System;
using System.Collections;
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
            return builder
                .AddValueGetter(typeof (DataRow), DataRowGetter)
                .AddEnumerationConversion(typeof (DataTable), DataTableEnumerationConversion)
                .AddEnumerationConversion(typeof (DataSet), DataSetEnumerationConversion);
        }

        internal static IEnumerable DataTableEnumerationConversion(object obj)
        {
            var dt = obj as DataTable;
            return dt != null ? (IEnumerable) dt.Rows : Enumerable.Empty<object>();
        }

        internal static IEnumerable DataSetEnumerationConversion(object obj)
        {
            var set = obj as DataSet;
            return set != null ? (IEnumerable)set.Tables : Enumerable.Empty<object>();
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
