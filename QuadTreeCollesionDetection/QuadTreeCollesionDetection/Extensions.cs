//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace QuadTree
//{
//    static class Extensions
//    {
//        /// <summary>
//        /// A line is a straight set of points that extend in opposite directions without ending.
//        /// </summary>
//        /// <param name="spriteBatch"> DrawLine is a SpriteBatch extention method</param>
//        /// <param name="start"></param>
//        /// <param name="end"></param>
//        /// <param name="color"></param>
//        /// <param name="thickness"></param>
//        public static void DrawLine(this SpriteBatch spriteBatch, Vector2 A, Vector2 B, Color color, float thickness = 2f)
//        {
//            Vector2 delta = B - A;

//            // y = ax+b
//            // a - line slope
//            // b - intersection with Y axis

//            //float a = delta.ToAngle();
//            float a = (B.X - A.X) / (B.Y - A.Y);
//            float b = A.Y - a * A.X;

//            // Calc Y at the Viewport.Width intersection
//            float x = (float)spriteBatch.GraphicsDevice.Viewport.Width;
//            float y = a * x + b;

//            Vector2 newStart = new Vector2(0, b);
//            Vector2 newEnd = new Vector2(x, y);

//            delta = newEnd - newStart;
//            float angle = delta.ToAngle();
//            //Vector2 viewPortLength = new Vector2(spriteBatch.GraphicsDevice.Viewport.Width, spriteBatch.GraphicsDevice.Viewport.Height) - Vector2.Zero;
//            //Vector2 start, Vector2 end
//            // Draw the Line
//            spriteBatch.Draw(Art.Pixel, newStart, null, color, delta.ToAngle(), new Vector2(0, 0.5f), new Vector2(delta.Length(), thickness), SpriteEffects.None, 0f);
//            // Draw the Points
//            spriteBatch.Draw(Art.Pixel, A, null, Color.White, delta.ToAngle(), new Vector2(0, 0.5f), new Vector2(thickness, thickness), SpriteEffects.None, 0f);
//            spriteBatch.Draw(Art.Pixel, B, null, Color.White, delta.ToAngle(), new Vector2(0, 0.5f), new Vector2(thickness, thickness), SpriteEffects.None, 0f);
//        }

//        public static void DrawLineSegment(this SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, float thickness = 2f)
//        {
//            Vector2 delta = end - start;
//            spriteBatch.Draw(Art.Pixel, start, null, color, delta.ToAngle(), new Vector2(0, 0.5f), new Vector2(delta.Length(), thickness), SpriteEffects.None, 0f);
//            spriteBatch.Draw(Art.Pixel, start, null, Color.White, delta.ToAngle(), new Vector2(0, 0.5f), new Vector2(thickness, thickness), SpriteEffects.None, 0f);
//            spriteBatch.Draw(Art.Pixel, end, null, Color.White, delta.ToAngle(), new Vector2(0, 0.5f), new Vector2(thickness, thickness), SpriteEffects.None, 0f);
//        }

//        public static float ToAngle(this Vector2 vector)
//        {
//            return (float)Math.Atan2(vector.Y, vector.X);
//        }
//    }
//}
