using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BO
{
    public static class Tools
    {
        /// <summary>
        /// Generic printable function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string ToStringProperty<T>(this T t, string suffix = "")
        {
            string str = "";
            foreach (PropertyInfo prop in t.GetType().GetProperties())
            {
                var value = prop.GetValue(t, null);
                if (value is IEnumerable)
                    foreach (var item in (IEnumerable)value)
                        str += item.ToStringProperty("   ");
                else
                    str += "\n" + suffix + prop.Name + ": " + value;
            }
            return str;
        }
        /// <summary>
        /// General function for updating items
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="outer"></param>
        /// <param name="updator"></param>
        public static void Update<TSource>(this IEnumerable<TSource> outer, Action<TSource> updator)
        {
            foreach (var item in outer)
            {
                updator(item);
            }
        }
      
    }
}