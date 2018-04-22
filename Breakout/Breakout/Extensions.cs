using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    static class Extensions
    {
        /// <summary>
        /// Return the vector angle in radians
        /// </summary>
        /// <param name="vector"></param>
        /// <returns>float</returns>
        public static float ToAngleRad(this Vector2 vector)
        {
            return (float)MathUtil.ToAngleRad(vector);
        }

        /// <summary>
        /// Return the vector angle in degrees
        /// </summary>
        /// <param name="vector"></param>
        /// <returns>float</returns>
        //public static float ToAngleDeg(this Vector2 vector)
        //{
        //    return (float)MathUtil.ToAngleDeg(vector);
        //}

        #region SpriteBatch extention functions

        #region DrawPoint
        public static void DrawPoint(this SpriteBatch spriteBatch, Vector2 position, Color color, float scale = 0.05f)
        {
            spriteBatch.Draw(
                Art.SpriteSheet,        // A texture to be drawn.
                position - new Vector2(Art.SRect_Point.Width*scale/2),               // The drawing location on the screen.
                Art.SRect_Point,        // An optional region in the SriteSheet which will be rendered. If null - draws full texture.
                color,                  // A color mask.
                0f,                     // A rotation of this sprite.
                new Vector2(0.5f),      // Center of the rotation 0,0 by default.
                scale,                  // A Scaling of this sprite.
                SpriteEffects.None,     // Modificators for drawing. Can be combined.
                0.0f);                  // A depth of the layer of this sprite.
        }
        #endregion

        #region DrawLineSegment
        public static void DrawLineSegment(this SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, float thickness = 1f, float scale = 1)
        {
            Vector2 dirVec = (end - start) * scale;
            Vector2 clampedScale = new Vector2(dirVec.Length() * scale, thickness);
            clampedScale = Vector2.Clamp(clampedScale, Vector2.Zero, Game1.ScreenSize);
            spriteBatch.Draw(Art.Pixel, start, null, color, dirVec.ToAngleRad(), new Vector2(0, 0.5f), clampedScale, SpriteEffects.None, 0f);
        }
        #endregion

        #region DrawRay
        public static void DrawRay(this SpriteBatch spriteBatch, Vector2 position, Vector2 direction, Color color, float thickness = 1f, float scale = 1)
        {
            Vector2 clampedScale = new Vector2(direction.Length() * scale, thickness);
            clampedScale = Vector2.Clamp(clampedScale, Vector2.Zero, Game1.ScreenSize);
            spriteBatch.Draw(Art.Pixel, position, null, color, direction.ToAngleRad(), new Vector2(0, 0.5f), clampedScale, SpriteEffects.None, 0f);
        }
        #endregion

        #region DrawRectangle
        public static void DrawRectangle(this SpriteBatch spriteBatch, Rectangle rec, Color color, float thickness = 1f)
        {
            spriteBatch.DrawLineSegment(new Vector2(rec.Left,rec.Top) , new Vector2(rec.Right, rec.Top), color, thickness);
            spriteBatch.DrawLineSegment(new Vector2(rec.Right, rec.Top) , new Vector2(rec.Right, rec.Bottom), color, thickness);
            spriteBatch.DrawLineSegment(new Vector2(rec.Right, rec.Bottom), new Vector2(rec.Left, rec.Bottom), color, thickness);
            spriteBatch.DrawLineSegment(new Vector2(rec.Left, rec.Bottom), new Vector2(rec.Left, rec.Top), color, thickness);
        }

        public static void DrawRectangle(this SpriteBatch spriteBatch, RectangleF rec, Color color, float thickness = 1f)
        {
            spriteBatch.DrawLineSegment(new Vector2(rec.Left, rec.Top), new Vector2(rec.Right, rec.Top), color, thickness);
            spriteBatch.DrawLineSegment(new Vector2(rec.Right, rec.Top), new Vector2(rec.Right, rec.Bottom), color, thickness);
            spriteBatch.DrawLineSegment(new Vector2(rec.Right, rec.Bottom), new Vector2(rec.Left, rec.Bottom), color, thickness);
            spriteBatch.DrawLineSegment(new Vector2(rec.Left, rec.Bottom), new Vector2(rec.Left, rec.Top), color, thickness);
        }
        #endregion

        #endregion
    }
}
