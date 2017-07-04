using Stubble.Core.Interfaces;
using Stubble.Core.Settings;
using System.Collections;
using System.Data;
using System.Linq;

namespace Stubble.Extensions.SystemData
{
    public static class SystemData
    {
        public static IRendererSettingsBuilder<IStubbleBuilder<T>> AddSystemData<T>(this IRendererSettingsBuilder<IStubbleBuilder<T>> builder)
        {
            return builder
                .AddValueGetter(typeof(DataRow), DataRowGetter)
                .AddValueGetter(typeof(DataSet), DataSetGetter)
                .AddEnumerationConversion(typeof (DataTable), DataTableEnumerationConversion)
                .AddEnumerationConversion(typeof (DataSet), DataSetEnumerationConversion);
        }

        public static IStubbleBuilder<T> AddSystemData<T>(this IStubbleBuilder<T> builder)
        {
            var settingsBuilder = builder as IRendererSettingsBuilder<IStubbleBuilder<T>>;
            settingsBuilder.AddSystemData();
            return builder;
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

        internal static object DataSetGetter(object value, string key)
        {
            var dataSet = value as DataSet;

            int intVal;
            if (int.TryParse(key, out intVal))
            {
                return dataSet.Tables[intVal];
            }

            return dataSet.Tables.Contains(key) ? dataSet.Tables[key] : null;
        }
    }
}
