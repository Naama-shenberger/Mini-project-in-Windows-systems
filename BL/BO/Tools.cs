using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
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
        /// <summary>
        /// The function gets a string which is the password and the salt
        /// The hash is used as a unique value 
        /// </summary>
        /// <param name="passwordWithSalt"></param>
        /// <returns></returns>
        public static string hashPassword(string passwordWithSalt)
        {
            SHA512 shaM = new SHA512Managed();
            return Convert.ToBase64String(shaM.ComputeHash(Encoding.UTF8.GetBytes(passwordWithSalt)));
        }

    }

}