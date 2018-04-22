using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    abstract class Entity
    {
        protected Texture2D Image;
        protected Rectangle SourceRect;
        protected Color color = Color.White;
        protected List<LineSegment> segmentsList = new List<LineSegment>();

        public float Orientation;
        public float Radius;            // Used for circular collision detection
        public bool IsExpired;          // true if the entity was destroyed and should be deleted.

        protected Vector2 position;
        public virtual Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        protected Vector2 velocity;
        public virtual Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public Vector2 Scale { get; set; }  // Can scale by width and height
        public float Width { get { return SourceRect.Width * Scale.X; } }
        public float Height { get { return SourceRect.Height * Scale.Y; } }
        public float X {
            get { return Position.X; }
            set { position.X = value; }
        }
        public float Y {
            get { return Position.Y; }
            set { position.Y = value; }
        }
        public Vector2 Size { get { return Image == null ? Vector2.Zero : new Vector2(Width, Height); } }
        public virtual RectangleF BoundingBox { get { return new RectangleF(X, Y, Width, Height); } }
        public Vector2 Center { get { return Position + Size / 2; } }
        public LineSegment LineSegmentTop
        {
            get { return new LineSegment(new Vector2(BoundingBox.X, BoundingBox.Y), new Vector2(BoundingBox.X + Width, BoundingBox.Y), BrickSide.Top); }
        }
        public LineSegment LineSegmentRight
        {
            get { return new LineSegment(new Vector2(BoundingBox.X + Width, BoundingBox.Y), new Vector2(BoundingBox.X + Width, BoundingBox.Y + Height), BrickSide.Right); }
        }
        public LineSegment LineSegmentLeft
        {
            get { return new LineSegment(new Vector2(BoundingBox.X, BoundingBox.Y), new Vector2(BoundingBox.X, BoundingBox.Y + Height), BrickSide.Left); }
        }
        public LineSegment LineSegmentBottom
        {
            get { return new LineSegment(new Vector2(BoundingBox.X, BoundingBox.Y + Height), new Vector2(BoundingBox.X + Width, BoundingBox.Y + Height), BrickSide.Bottom); }
        }

        public Entity()
        {
            Image = Art.SpriteSheet;
            Scale = new Vector2(1f);
        }

        public IList<LineSegment> GetBoundSegments()
        {
            segmentsList.Clear();
            segmentsList.Add(LineSegmentTop);
            segmentsList.Add(LineSegmentRight);
            segmentsList.Add(LineSegmentLeft);
            segmentsList.Add(LineSegmentBottom);

            return segmentsList;
        }

        public abstract void Update();

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Image,                  // A texture to be drawn.
                Position,               // The drawing location on the screen.
                SourceRect,             // An optional region which will be rendered. If null - draws full texture.
                color,                  // A color mask.
                Orientation,            // A rotation of this sprite.
                new Vector2(0.5f),              // Center of the rotation 0,0 by default.
                Scale,                  // A Scaling of this sprite.
                SpriteEffects.None,     // Modificators for drawing. Can be combined.
                0.0f);                  // A depth of the layer of this sprite.

#if SPRITE_ORIGIN
            spriteBatch.DrawPoint(Position + Size/2, Color.Yellow, 0.1f);
            spriteBatch.DrawRectangle(BoundingBox, Color.Red);
#endif
        }
    }
}
