using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    class Font
    {
        // Font related properties
        private static string[] fontNamesArray = { "Arial", "ArialBold", "CourierNew", "Kootenay", "Lindsey", "Miramonte", "NovaSquare", "Pescadero" };
        private static Dictionary<string, SpriteFont> spriteFontsDictionary;

        // Properties
        public static SpriteFont DefaultFont { get; private set; }

        //public static SpriteFont GetSpriteFont(string fontName)
        //{
        //    spriteFontsDictionary.TryGetValue(fontName, out SpriteFont sp);
        //    return sp;
        //}

        //public static bool SetSpriteFont(string fontName)
        //{
        //    DefaultFont = GetSpriteFont(fontName);
        //}

        public static void Load(ContentManager contentManager)
        {
            // load all the spritefonts
            foreach (var fontName in fontNamesArray)
            {
                spriteFontsDictionary.Add(fontName, contentManager.Load<SpriteFont>(fontName));
            }

            // Set Default Font
            DefaultFont = spriteFontsDictionary["Arial"];
        }
    }
}
