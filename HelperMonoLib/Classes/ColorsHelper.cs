using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Shakkour.Utils;

namespace Shakkour.MonoGame.Helper
{
    public class ColorsHelper : IDisposable
    {
        public ArrayList GetColorsArray()
        {
            List<Color> collorArray = new List<Color>();

            var colors = PropertyHelper.GetStaticPropertyBag(typeof(Color));
            foreach (KeyValuePair<string, object> colorPair in colors)
            {
                Console.WriteLine(colorPair.Key);
                collorArray.Add((Color)colorPair.Value);
            }

            return new ArrayList(collorArray);
        }

        public List<Color> GetColorsList()
        {
            List<Color> collorArray = new List<Color>();

            var colors = PropertyHelper.GetStaticPropertyBag(typeof(Color));
            foreach (KeyValuePair<string, object> colorPair in colors)
            {
                Console.WriteLine(colorPair.Key);
                collorArray.Add((Color)colorPair.Value);
            }

            return collorArray;
        }

        public Dictionary<string, Color> GetColorsDictionary()
        {
            Dictionary<string, Color> colorsDictionary = new Dictionary<string, Color>();

            var colorsDict = PropertyHelper.GetStaticPropertyBag(typeof(Color));
            foreach (KeyValuePair<string, object> colorPair in colorsDict)
            {
                Console.WriteLine(colorPair.Key + " - " + colorPair.Value.ToString());
                colorsDictionary.Add(colorPair.Key, (Color)colorPair.Value);
            }

            return colorsDictionary;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
