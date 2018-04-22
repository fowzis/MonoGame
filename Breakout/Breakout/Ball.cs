using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    class Ball : Entity
    {
        private static Random rand = new Random();
        private static Vector2 aim = Vector2.Zero;      // The ball direction vector relative to the (0,0) origin point

        #region Class Properties
        public static Ball Instance { get; private set; }
        public bool Freeze { get; set; }
        public float Speed { get; set; }
        public Vector2 PositionNextFrame { get { return Position + Velocity; } }
        public Vector2 CenterNextFrame { get { return Center + Velocity; } }
        public LineSegment GetRay { get { return new LineSegment(Center, Center + aim * Radius); } }
        #endregion

        #region Overriden Properties
        // Speed - being a scalar quantity, is the rate at which an object covers distance.
        // The average speed is the distance (a scalar quantity) per time ratio. Speed is ignorant of direction.
        // Veclocity - is Speed with direction, or Vector Mangnitude with direction
        public override Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = (MathUtil.FromPolar(value.ToAngleRad(), Speed)); }
        }
        #endregion

        public Ball()
        {
            Instance = this;
            SourceRect = Art.SRect_VecBallSmall;
            Radius = Width / 2;
            Freeze = false;

            // Speed, being a scalar quantity, is the rate at which an object covers distance.
            // The average speed is the distance (a scalar quantity) per time ratio. Speed is ignorant of direction.
            Speed = 10;

            // Set initial velocity and angle.
            // A unit vector representing the ball direction
            aim = Vector2.Normalize( new Vector2(rand.Next(0, Game1.Viewport.Width), rand.Next(0, Game1.Viewport.Height)) );
            Velocity = -aim;

            // Set initial position
            Position = new Vector2(Game1.Viewport.Width / 2, Game1.Viewport.Height / 2);
        }

        public override void Update()
        {
            // If Freeze, don't update
            if (Freeze)
                return;

            // 1. Check Collision with screen borders and deflect the ball
            //HandleBordersCollision(Game1.Viewport.Bounds);

            // 3. Set new position, Update ball movement
            Position += Velocity;
            Position = Vector2.Clamp(Position, Vector2.Zero, Game1.ScreenSize + Size);
        }

        public void DeflectX()
        {
            aim.X = -aim.X;
            Velocity = aim;
        }

        public void DeflectY()
        {
            aim.Y = -aim.Y;
            Velocity = aim;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

#if SPRITE_ORIGIN
            spriteBatch.DrawRay(Center, aim, Color.Yellow, 1, (aim * Radius).Length());
            spriteBatch.DrawPoint(Center, Color.Blue, 0.08f);
            spriteBatch.DrawRectangle(BoundingBox, Color.LightBlue);
#endif
        }

        #region Commented Code
        //public void HandleBordersCollision(Rectangle bounds)
        //{
        //    // If colliding with the viewport boundaries, deflect the ball
        //    if (X <= Radius || X >= bounds.Width - Radius)
        //    {
        //        aim.X = -aim.X;
        //        Velocity = aim;
        //    }
        //    else if (Y <= Radius || Y >= bounds.Height - Radius)
        //    {
        //        aim.Y = -aim.Y;
        //        Velocity = aim;
        //    }
        //}
        #endregion
    }
}
