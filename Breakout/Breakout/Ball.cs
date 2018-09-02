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

        private bool resetClamp;
        Vector2 topLeftBound, bottomRightBound;
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

        public override Vector2 Position
        {
            get { return position; }
            set
            {
                if (resetClamp)
                {
                    ResetClamp();
                }
                position = Vector2.Clamp(value, topLeftBound, bottomRightBound);
            }
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
            ResetBall();
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
        }

        public void ResetBall()
        {
            aim = Vector2.Normalize(new Vector2(rand.Next(0, Game1.Viewport.Width), rand.Next(0, Game1.Viewport.Height)));
            Velocity = -aim;

            // Set initial position
            Position = new Vector2(Game1.Viewport.Width / 2, Game1.Viewport.Height / 2);

            ResetClamp();
        }

        public void SetOnetimeBounds(Vector2 topLeftBound, Vector2 bottomRightBound)
        {
            this.topLeftBound = topLeftBound;
            this.bottomRightBound = bottomRightBound - new Vector2(0,Height);
            Position = Position;
            resetClamp = true;
        }

        public void SetOnetimeTopLeftBounds(Vector2 topLeftBound)
        {
            this.topLeftBound = topLeftBound;
            Position = Position;
            resetClamp = true;
        }

        public void SetOnetimeBottomRightBounds(Vector2 bottomRightBound)
        {
            this.bottomRightBound = bottomRightBound;
            Position = Position;
            resetClamp = true;
        }

        private void ResetClamp()
        {
            topLeftBound = Vector2.Zero;
            bottomRightBound = Game1.ScreenSize;
            resetClamp = false;
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

        public LineSegment GetRay()
        {
            float rayScale = new Vector2(Game1.Viewport.Width, Game1.Viewport.Height).Length() / velocity.Length();
            return new LineSegment(Center, (Center + (velocity * rayScale)));
        }

        public LineSegment[] GetRays()
        {
            float rayScale = new Vector2(Game1.Viewport.Width, Game1.Viewport.Height).Length() / velocity.Length();

            // Find the two vectors parallel to the Ray emmitting from the circle center in a given direction
            double angleRad1 = aim.ToAngleRad() + (Math.PI / 2);
            double angleRad2 = aim.ToAngleRad() - (Math.PI / 2);
            Vector2 P1 = new Vector2(Center.X + (float)(Math.Cos(angleRad1) * Radius), Center.Y + (float)(Math.Sin(angleRad1) * Radius));
            Vector2 P2 = new Vector2(Center.X + (float)(Math.Cos(angleRad2) * Radius), Center.Y + (float)(Math.Sin(angleRad2) * Radius));

            LineSegment[] rays = new LineSegment[3];
            rays[0] = new LineSegment(P1, (P1 + (velocity * rayScale)));
            rays[1] = new LineSegment(Center, (Center + (velocity * rayScale)));
            rays[2] = new LineSegment(P2, (P2 + (velocity * rayScale)));
            return rays;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

#if SPRITE_ORIGIN
            //spriteBatch.DrawRay(Center, aim, Color.Yellow, 1, (aim * Radius).Length());
            LineSegment ray = GetRay();
            spriteBatch.DrawLineSegment(ray.P1, ray.P2, Color.Yellow);
            spriteBatch.DrawPoint(Center, Color.Yellow, 0.08f);
            spriteBatch.DrawRectangle(BoundingBox, Color.LightBlue);

            LineSegment[] rays = GetRays();
            foreach (LineSegment line in rays)
            {
                spriteBatch.DrawLineSegment(line.P1, line.P2, Color.Yellow);
            }
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
