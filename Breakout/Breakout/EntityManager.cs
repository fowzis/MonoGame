using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    static class EntityManager
    {
        private static List<Entity> entitiesList = new List<Entity>();
        private static List<Brick> bricksList = new List<Brick>();
        private static List<Point> hitPoints = new List<Point>();

        private static Ball Ball;
        private static Paddle Paddle;
        private static int paddleHitCoolDown = 0, coolDownPeriod = 3;

        static bool isUpdating;
        static List<Entity> addedEntities = new List<Entity>();
        static int hit = 0;

        public static int HitPoints { get { return hitPoints.Count; } }

        public static void Add(Entity entity)
        {
            // While updating the entities, prevent adding new elements to the entities list
            if (!isUpdating)
                AddEntity(entity);
            else
                addedEntities.Add(entity);
        }

        private static void AddEntity(Entity entity)
        {
            entitiesList.Add(entity);
            if (entity is Brick)
            {
                bricksList.Add(entity as Brick);
                return;
            }
            else if (entity is Ball)
            {
                Ball = entity as Ball;
            }
            else if (entity is Paddle)
            {
                Paddle = entity as Paddle;
            }
        }

        public static void Update()
        {
            // While updating the entities, prevent adding new elements to the entities list
            isUpdating = true;
            HandleCollisions();

            foreach (var entity in entitiesList)
                entity.Update();

            // Updating the entities is finished
            isUpdating = false;

            // Insert newly added entities, added to the temporary entities list to the the main entities list
            foreach (var entity in addedEntities)
                AddEntity(entity);

            // Clear Temporary entities list
            addedEntities.Clear();

            // Clear expired entities from main list. Filter out expired entities and overrite main entities list
            entitiesList = entitiesList.Where(x => !x.IsExpired).ToList();
            //bullets = bullets.Where(x => !x.IsExpired).ToList();
            //enemies = enemies.Where(x => !x.IsExpired).ToList();
            //blackHoles = blackHoles.Where(x => !x.IsExpired).ToList();
        }

        static int n = 0;
        private static void HandleCollisions()
        {
            #region Handle ball collision with screen play field bounds
            Rectangle screenSounds = Game1.Viewport.Bounds;
            // If colliding with the defined play field boundaries, deflect the ball
            if (Ball.X <= 0 || Ball.X >= screenSounds.Width - Ball.Width)
            {
                Ball.DeflectX();
                hitPoints.Clear();
            }
            else if (Ball.Y <= 0)
            {
                Ball.DeflectY();
                hitPoints.Clear();
            }
            else if (Ball.Y >= screenSounds.Height)
            {
                hitPoints.Clear();
                Game1.Instance.Exit();
            }

            #endregion

            #region Handle ball collision with Paddle
            // If collission detected, give the bacl chance to get a distance
            if (paddleHitCoolDown == 0)
            {
                // Collission with Paddel Top
                if ((Ball.Center.X > Paddle.BoundingBox.Left)
                    && (Ball.Center.X < Paddle.BoundingBox.Right)
                    && (Ball.Center.Y >= Paddle.BoundingBox.Top - Ball.Radius) // Hit the top line
                    && (Ball.Center.Y < Paddle.BoundingBox.Top))
                {
                    Ball.DeflectY();
                    paddleHitCoolDown = coolDownPeriod;
                    Console.WriteLine("Paddle Hit - Top", ++n);
                }
                else // Collision with Paddle left side
                if ((Ball.Center.X >= Paddle.BoundingBox.Left - Ball.Radius)     // Hit the left line
                    && (Ball.Center.X < Paddle.BoundingBox.Left)     // Hit the left line
                    && (Ball.Center.Y > Paddle.BoundingBox.Top)
                    && (Ball.Center.Y < Paddle.BoundingBox.Bottom))
                {
                    Ball.DeflectX();
                    paddleHitCoolDown = coolDownPeriod;
                    Console.WriteLine("Paddle Hit - Left", ++n);
                }
                else // Collision with Paddle right side
                if ((Ball.Center.X <= Paddle.BoundingBox.Right + Ball.Radius)
                    && (Ball.Center.X > Paddle.BoundingBox.Right)
                    && (Ball.Center.Y > Paddle.BoundingBox.Top)
                    && (Ball.Center.Y < Paddle.BoundingBox.Bottom))
                {
                    Ball.DeflectX();
                    paddleHitCoolDown = coolDownPeriod;
                    Console.WriteLine("Paddle Hit - Right", ++n);
                }
            }
            else
            {
                // Cooldown one cycle.
                paddleHitCoolDown--;
            }

            Game1.Instance.Window.Title = "paddleHitCoolDown={" + paddleHitCoolDown + "}, PositionNextFrame = {" + Ball.PositionNextFrame + ", Diff={" + (Ball.Position - Ball.PositionNextFrame) + "}";
            #endregion

            HandleBallCollision();
        }

        private static void HandleBallCollision()
        {
            // Identify all the bricks in the ball path
            CheckIntersectionBallPath(Paddle);

            // return a list of bricks in the path
            foreach (var brick in bricksList)
            {
                CheckIntersectionBallPath(brick);
                #region Commented
                //if (IsBallColliding(brick, out side))
                //{
                //    //Ball.Freeze = true;
                //    Console.WriteLine("Ball=({0},{1}), Paddle=({2},{3})", Ball.X, Ball.Y, Paddle.X, Paddle.Y);
                //    Console.WriteLine("Ball.Pos-Brick.Pos=({0})", Ball.Position - brick.Position);
                //    //Console.WriteLine("Ball.Bounds=({0},{1}), Paddle.Bounds=({2},{3})", Ball.Bounds.X, Ball.Bounds.Y, Paddle.Bounds.X, Paddle.Bounds.Y);
                //    //Ball.Deflect(side);

                //    //              |
                //    //   Q3         |    Q2
                //    //   (-x,-y)    |    (+x,-y)
                //    //        -------------       
                //    //-------|-----0|0-----|-------
                //    //        ------|------
                //    //   Q4         |    Q1
                //    //   (-x,+y)    |    (+x,+y)
                //    //              |
                //    var dist = Ball.Position - brick.Position;
                //    if (dist.X > 0 && dist.Y < 0) // Q1
                //    {
                //        if (Ball.X - Ball.Radius < brick.X + brick.Width / 2)
                //        {
                //            side = Brick.BrickSide.Top | Brick.BrickSide.Right;
                //        }
                //        else
                //        {
                //            side = Brick.BrickSide.Right;
                //        }
                //    }
                //    else if (dist.X < 0 && dist.Y < 0) // Q4
                //    {
                //        side = Brick.BrickSide.Top | Brick.BrickSide.Left;
                //    }
                //    else if (dist.X > 0 && dist.Y > 0) // Q2
                //    {
                //        side = Brick.BrickSide.Bottom | Brick.BrickSide.Right;
                //    }
                //    else if (dist.X < 0 && dist.Y > 0) // Q3
                //    {
                //        side = Brick.BrickSide.Bottom | Brick.BrickSide.Left;
                //    }
                //}
                #endregion
            }
        }

        private static bool IsBallColliding(Entity entity, out BrickSide side)
        {
            side = BrickSide.None;

            if (entity.BoundingBox.Intersects(Ball.BoundingBox))
            {
                //Game1.Instance.Window.Title = "Hit=" + hit++;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Find all entities in the ball intersection path
        /// </summary>
        /// <param name="entity"></param>
        private static void CheckIntersectionBallPath(Entity entity)
        {
            LineSegment ray = Ball.GetRay;

            foreach (LineSegment lineSegment in entity.GetBoundSegments())
            {
                if (MathUtil.DoLinesIntersect(ray, lineSegment, out Vector2 intersectionPoint))
                {
                    // TODO - EntityManager - To remove or comment
                    // used only to indicate intersection points with the entities found in the ball path
                    if (!hitPoints.Contains(intersectionPoint.ToPoint()))
                        hitPoints.Add(intersectionPoint.ToPoint());

                    // TODO - EntityManager - Generate a small list of entities that for sure intersecting with the ball.
                    //break;
                }
            }
        }

        private static bool IsColliding(Entity a, Entity b)
        {
            float radius = a.Radius + b.Radius;
            float distanceSquared = Vector2.DistanceSquared(a.Position, b.Position);
            float squaredRadius = radius * radius;
            return distanceSquared < squaredRadius;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in entitiesList)
                entity.Draw(spriteBatch);

            foreach (var point in hitPoints)
            {
                spriteBatch.DrawPoint(new Vector2(point.X, point.Y), Color.White, 0.05f);
            }
        }

    }
}
