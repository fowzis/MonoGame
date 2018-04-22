using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Breakout
{
    static class Art
    {
        // The dictionary holding the sprites contained in the sprite sheet.
        public static Dictionary<string, Rectangle> spriteRectDictionary;
        // The sprite sheet texture
        public static Texture2D SpriteSheet { get; private set; }
        public static Texture2D Pixel { get; private set; }		// a single white pixel

        // The location (source rectangles) of the important sprites in the SpriteSheet
        public static Rectangle SRect_PaddleSmall { get; private set; }
        public static Rectangle SRect_PaddleMedium { get; private set; }
        public static Rectangle SRect_PaddleLarge { get; private set; }
        public static Rectangle SRect_BallSmall { get; private set; }
        public static Rectangle SRect_BallLarge { get; private set; }
        public static Rectangle SRect_Point { get; private set; }
        public static Rectangle SRect_VecBallSmall { get; private set; }
        public static Rectangle SRect_VecPaddleSmall { get; private set; }
        
        public static void Load(ContentManager contentManager)
        {
            #region SpriteSheet
            // Load the Sprite Sheet and the Sprite location/rectangles XML
            SpriteSheet = contentManager.Load<Texture2D>("BreakoutSpriteSheet");
            // Load the SpriteSheet content sprites location/rectangles
            SpriteSheetPacker.LoadXMLSpriteSheet(contentManager, "BreakoutSpriteSheetXML", out spriteRectDictionary);

            // Get reference to the important sprites to prevent excessive searches in the disctionary
            // performance considerations.
            SRect_PaddleSmall = spriteRectDictionary["PaddleSmall"];
            SRect_PaddleMedium = spriteRectDictionary["PaddleMedium"];
            SRect_PaddleLarge = spriteRectDictionary["PaddleLarge"];

            SRect_BallSmall = spriteRectDictionary["BallSmall"];
            SRect_BallLarge = spriteRectDictionary["BallLarge"];
            SRect_VecBallSmall = spriteRectDictionary["VecBallSmall"];
            SRect_VecPaddleSmall = spriteRectDictionary["VecPaddleSmall"];
            
            SRect_Point = spriteRectDictionary["Point"];
            #endregion

            Pixel = new Texture2D(Game1.Instance.GraphicsDevice, 1, 1);
            Pixel.SetData(new[] { Color.White });
        }

        /// <summary>
        /// Return source rectangle of the named sprite inside the sprite sheet
        /// </summary>
        /// <param name="spriteName">Sprite Name as per the XML file</param>
        /// <returns></returns>
        public static Rectangle GetSourceRectangle(string spriteName)
        {
            if (String.IsNullOrEmpty(spriteName) || !spriteRectDictionary.ContainsKey(spriteName))
            {
                return Rectangle.Empty;
            }

            return spriteRectDictionary[spriteName];
        }       
    }
}
