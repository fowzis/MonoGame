using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Shakkour.Utils;

namespace Shakkour.MonoGame.Helper
{
    public class ColorsHelper : IDisposable
    {
        public List<Color> GetColorsList()
        {
            List<Color> colorsList = new List<Color>();

            var colors = PropertyHelper.GetStaticPropertyBag(typeof(Color));
            foreach (KeyValuePair<string, object> colorPair in colors)
            {
                Console.WriteLine(colorPair.Key);
                colorsList.Add((Color)colorPair.Value);
            }

            return colorsList;
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
