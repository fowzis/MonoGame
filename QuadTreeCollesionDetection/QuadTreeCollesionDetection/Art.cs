using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace QuadTree
{
    static class Art
    {
        public static Texture2D Pixel { get; private set; }		// a single white pixel

        public static void Load(ContentManager content)
        {
            // TODO
            Pixel = content.Load<Texture2D>(@"Images\Pixel");
        }
    }
}
