using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace HelperMonoLib
{
    public class ColorsHelper
    {
        public List<Color> GetColorsList()
        {
            List<Color> colorsList = new List<Color>();

            var colors = HelperUtilsLib.PropertyHelper.GetStaticPropertyBag(typeof(Color));
            foreach (KeyValuePair<string, object> colorPair in colors)
            {
                Console.WriteLine(colorPair.Key);
                colorsList.Add((Color)colorPair.Value);
            }

            return colorsList;
        }
    }
}
