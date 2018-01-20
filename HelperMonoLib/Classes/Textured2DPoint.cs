using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shakkour.MonoGame.Helper
{
    public class Textured2DPoint
    {
        private Vector2 pos;

        public Texture2D Texture { get; set; }      // The texture that will be drawn to represent the particle
        public Color Color { get; set; }            // The color of the particle
        public int Size { get; set; }             // The size of the particle
        
        // The current position of the particle
        public Vector2 Position
        {
            get { return pos; }
            private set
            {
                pos.X = value.X - Size / 2;
                pos.Y = value.Y - Size / 2;
            }
        }

        public Textured2DPoint(Texture2D texture, Vector2 position, Color color, int size)
        {
            Texture = texture;
            Position = position;
            Color = color;
            Size = size;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // The drawing bounds on the screen
            Rectangle destinationRectangle = new Rectangle(Position.ToPoint(), new Point(Size));

            spriteBatch.Draw(Texture, destinationRectangle, Color);
        }

    }
}
