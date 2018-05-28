using Stubble.Core.Interfaces;
using Stubble.Core.Settings;
using System.Collections;
using System.Data;
using System.Linq;
using System;

namespace Stubble.Extensions.SystemData
{
    public static class SystemData
    {
        public static RendererSettingsBuilder AddSystemData(this RendererSettingsBuilder builder)
        {
            return builder
                .AddValueGetter(typeof(DataRow), DataRowGetter)
                .AddValueGetter(typeof(DataSet), DataSetGetter)
                .AddTruthyCheck<DBNull>(DBNullTruthyCheck)
                .AddEnumerationConversion(typeof (DataTable), DataTableEnumerationConversion)
                .AddEnumerationConversion(typeof (DataSet), DataSetEnumerationConversion);
        }

        internal static IEnumerable DataTableEnumerationConversion(object obj)
        {
            return obj is DataTable dt 
                ? (IEnumerable)dt.Rows 
                : Enumerable.Empty<object>();
        }

        internal static IEnumerable DataSetEnumerationConversion(object obj)
        {
            return obj is DataSet set 
                ? (IEnumerable)set.Tables 
                : Enumerable.Empty<object>();
        }

        internal static object DataRowGetter(object value, string key, bool ignoreCase)
        {
            if (value is DataRow dataRow && dataRow.Table.Columns.Contains(key))
            {
                return dataRow[key];
            }
            return null;
        }

        internal static object DataSetGetter(object value, string key, bool ignoreCase)
        {
            if (value is DataSet dataSet)
            {
                if (int.TryParse(key, out var intVal))
                {
                    return dataSet.Tables[intVal];
                }

                return dataSet.Tables.Contains(key) ? dataSet.Tables[key] : null;
            }

            return null;
        }

        internal static bool DBNullTruthyCheck(DBNull value) => false;
    }
}
