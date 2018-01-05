using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace HelperUtilsLib
{
    public static class PropertyHelper
    {
        public static Dictionary<string, object> GetStaticPropertyBag(Type t)
        {
            const BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            var map = new Dictionary<string, object>();
            foreach (var prop in t.GetProperties(flags))
            {
                map[prop.Name] = prop.GetValue(null, null);
            }
            return map;

            #region How to use this method
            //var colors = GetStaticPropertyBag(typeof(Color));

            //foreach (KeyValuePair<string, object> colorPair in colors)
            //{
            //    Console.WriteLine(colorPair.Key);
            //    Color color = (Color)colorPair.Value;
            //}
            #endregion How to use this method
        }
    }
}
