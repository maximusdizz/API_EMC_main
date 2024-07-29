using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class ConvertUtility
    {
        public static DataTable ConvertTo<T>(T item)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            DataRow row = table.NewRow();

            foreach (PropertyDescriptor prop in properties)
            {
                row[prop.Name] = prop.GetValue(item);
            }

            table.Rows.Add(row);

            return table;
        }

        public static DataTable ConvertTo<T>(IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (T item in list)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }

                table.Rows.Add(row);
            }

            return table;
        }

        public static IList<T> ConvertTo<T>(IList<DataRow> rows)
        {
            IList<T> list = null;

            if (rows != null)
            {
                list = new List<T>();

                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }
            }

            return list;
        }

        public static IList<T> ConvertTo<T>(DataTable table)
        {
            if (table == null)
            {
                return null;
            }

            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }

            return ConvertTo<T>(rows);
        }

        public static T CreateItem<T>(DataRow row)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                    try
                    {
                        object value = row[column.ColumnName];
                        prop.SetValue(obj, value, null);
                    }
                    catch
                    {
                        // You can log something here
                        throw;
                    }
                }
            }

            return obj;
        }

        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            return table;
        }

        /// <summary>
        /// The convert to
        /// Author: SonPN
        /// CreatedDate: 7/11/2013 9:10 AM
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static T ConvertTo<T>(object obj)
        {
            if (obj == null)
            {
                return default(T);
            }

            try
            {
                var t = (T)System.Convert.ChangeType(obj, typeof(T));
                return t;
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// The convert to
        /// Author: SonPN
        /// CreatedDate: 7/11/2013 9:50 AM
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static T ConvertTo<T>(object obj, object defaultValue)
        {
            if (obj == null)
            {
                return ConvertTo<T>(defaultValue);
            }

            try
            {
                var t = (T)System.Convert.ChangeType(obj, typeof(T));
                return t;
            }
            catch
            {
                return ConvertTo<T>(defaultValue);
            }
        }

        /// <summary>
        /// The convert to
        /// Author: SonPN
        /// CreatedDate: 7/11/2013 9:10 AM
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static T ConvertTo<T>(string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return default(T);
            }

            try
            {
                var t = (T)System.Convert.ChangeType(obj, typeof(T));
                return t;
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// The convert to
        /// Author: SonPN
        /// CreatedDate: 7/11/2013 9:55 AM
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static T ConvertTo<T>(string obj, object defaultValue)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return ConvertTo<T>(defaultValue);
            }

            try
            {
                var t = (T)System.Convert.ChangeType(obj, typeof(T));
                return t;
            }
            catch
            {
                return ConvertTo<T>(defaultValue);
            }
        }

        //public static T JsonToObject<T>(string jsonString)
        //{
        //    try
        //    {
        //        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);
        //    }
        //    catch
        //    {
        //        return default(T);
        //    }
        //}

        //public static string ObjectToJson(object obj)
        //{
        //    return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        //}

        public static T ChangeType<T>(object obj)
        {
            if (null == obj)
                return default(T);

            try
            {
                var t = (T)Convert.ChangeType(obj, typeof(T));
                return t;
            }
            catch
            {
                return default(T);
            }
        }
    }
}
