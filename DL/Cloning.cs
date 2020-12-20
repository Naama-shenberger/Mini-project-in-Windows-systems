using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DL
{
    /// <summary>
    /// Copy Department
    /// Reason for class: We do not want BL to get the address of the object in dal's functions 
    /// because in that way the BL has access to change the object and we want as little contact as possible
    /// thats why we will create a copy of the object
    /// </summary>
    static class Cloning
    {
        /// <summary>
        /// Generic copy function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <returns></returns>
        internal static T Clone<T>(this T original) where T : new()
        {
            T copyToObject = new T();
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                propertyInfo.SetValue(copyToObject, propertyInfo.GetValue(original, null), null);
            return copyToObject;
        }

    }
}
