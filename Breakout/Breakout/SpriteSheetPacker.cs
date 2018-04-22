using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.IO;

namespace Breakout
{
    public static class SpriteSheetPacker
    {
        /// <summary>
        /// Use the XML exporter which saves into the XML Intermediate Format.
        /// Add the XML file to your content project and use Content.Load<Dictionary<string, Rectangle>> 
        /// to load the file as a dictionary mapping the image name to the source rectangle.
        /// </summary>
        /// <param name="content">ContentManager reference</param>
        /// <param name="spriteSheetName">The sprite sheet file name</param>
        /// <param name="spritesDict">A dictionary having KeyValuePairs of string and Rectangle</param>
        public static void LoadXMLSpriteSheet(ContentManager content, string spriteSheetName, out Dictionary<string, Rectangle> spritesDict)
        {
            spritesDict = content.Load<Dictionary<string, Rectangle>>(spriteSheetName);
        }

        public static void LoadTXTSpriteSheet(string spriteSheetName)
        {
            Dictionary<string, Rectangle> spriteSourceRectangles = new Dictionary<string, Rectangle>();

            // open a StreamReader to read the index
            string path = Path.Combine("Content\\", spriteSheetName);
            using (StreamReader reader = new StreamReader(path))
            {
                // while we're not done reading...
                while (!reader.EndOfStream)
                {
                    // get a line
                    string line = reader.ReadLine();

                    // split at the equals sign
                    string[] sides = line.Split('=');

                    // trim the right side and split based on spaces
                    string[] rectParts = sides[1].Trim().Split(' ');

                    // create a rectangle from those parts
                    Rectangle r = new Rectangle(
                       int.Parse(rectParts[0]),
                       int.Parse(rectParts[1]),
                       int.Parse(rectParts[2]),
                       int.Parse(rectParts[3]));

                    // add the name and rectangle to the dictionary
                    spriteSourceRectangles.Add(sides[0].Trim(), r);
                }
            }
        }
    }
}
