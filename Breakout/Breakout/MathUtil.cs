using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    static class MathUtil
    {
        /// <summary>
        /// Return a vector of a given magnitude in the direction of the provided angle
        /// </summary>
        /// <param name="angleRad">The angle or direction of the vector</param>
        /// <param name="magnitude">The magnitude or length of the vector</param>
        /// <returns>Vector2</returns>
        public static Vector2 FromPolar(float angleRad, float magnitude)
        {
            return magnitude * new Vector2((float)Math.Cos(angleRad), (float)Math.Sin(angleRad));
        }
        
        // Vector product / Cross product in 2D space
        public static float VectorProduct(Vector2 A, Vector2 B)
        {
            return (A.X * B.Y - A.Y * B.X);
        }

        /// <summary>
        /// Return a unit vector representing the the left side Normal to a line segment represented by two points P1 and P2.
        /// </summary>
        /// <param name="vP1"></param>
        /// <param name="vP2"></param>
        /// <returns>Vector2</returns>
        public static Vector2 Normal(Vector2 vP1, Vector2 vP2)
        {
            float dx = vP2.X - vP1.X;
            float dy = vP2.Y - vP1.Y;
            return Vector2.Normalize(new Vector2(dy, -dx));
        }

        /// <summary>
        /// An angle, θ, measured in radians, such that -π≤θ≤π, and tan(θ) = y / x, where(x, y) is a point in the Cartesian plane.
        /// Observe the following: 
        ///     For(x, y) in quadrant 1 (x>0, y>0) ,    0 < θ < π/2
        ///     For(x, y) in quadrant 2 (x<0, y>0),  π/2 < θ ≤ π.
        ///     For(x, y) in quadrant 3 (x<0, y<0),   -π < θ < -π/2. 
        ///     For(x, y) in quadrant 4 (x>0, y<0), -π/2 < θ < 0. 
        /// </summary>
        /// <param name="vector"></param>
        /// <returns>double</returns>
        public static double ToAngleRad(Vector2 vector)
        {
            // Return 
            return Math.Atan2(vector.Y, vector.X);
        }

        public static double ToAngleDeg(Vector2 vector)
        {
            // degree = radians × 180° / π
            return ToAngleRad(vector) * (180 / Math.PI);
        }

        /// <summary>
        /// This is based off an explanation and expanded math presented by Paul Bourke:
        /// 
        /// It takes two lines as inputs and returns true if they intersect, false if they 
        /// don't.
        /// If they do, ptIntersection returns the point where the two lines intersect.  
        /// </summary>
        /// <param name="L1">The first line</param>
        /// <param name="L2">The second line</param>
        /// <param name="ptIntersection">The point where both lines intersect (if they do).</param>
        /// <returns></returns>
        /// <remarks>See http://paulbourke.net/geometry/pointlineplane/ </remarks>
        public static bool DoLinesIntersect(LineSegment L1, LineSegment L2, out Vector2 ptIntersection)
        {
            ptIntersection = Vector2.Zero;

            // Denominator for ua and ub are the same, so store this calculation
            double d =
                (L2.P2.Y - L2.P1.Y) * (L1.P2.X - L1.P1.X)
                -
                (L2.P2.X - L2.P1.X) * (L1.P2.Y - L1.P1.Y);

            //n_a and n_b are calculated as seperate values for readability
            double n_a =
                (L2.P2.X - L2.P1.X) * (L1.P1.Y - L2.P1.Y)
                -
                (L2.P2.Y - L2.P1.Y) * (L1.P1.X - L2.P1.X);

            double n_b =
                (L1.P2.X - L1.P1.X) * (L1.P1.Y - L2.P1.Y)
                -
                (L1.P2.Y - L1.P1.Y) * (L1.P1.X - L2.P1.X);

            // Make sure there is not a division by zero - this also indicates that
            // the lines are parallel.  
            // If n_a and n_b were both equal to zero the lines would be on top of each 
            // other (coincidental).  This check is not done because it is not 
            // necessary for this implementation (the parallel check accounts for this).
            if (d == 0)
                return false;

            // Calculate the intermediate fractional point that the lines potentially intersect.
            double ua = n_a / d;
            double ub = n_b / d;

            // The fractional point will be between 0 and 1 inclusive if the lines
            // intersect.  If the fractional calculation is larger than 1 or smaller
            // than 0 the lines would need to be longer to intersect.
            if (ua >= 0d && ua <= 1d && ub >= 0d && ub <= 1d)
            {
                ptIntersection.X = (float)(L1.P1.X + (ua * (L1.P2.X - L1.P1.X)));
                ptIntersection.Y = (float)(L1.P1.Y + (ua * (L1.P2.Y - L1.P1.Y)));
                return true;
            }

            return false;
        }

        // Check if the Ray emmittig from the ball is intersecting with the bricks
        //public static bool DoLinesIntersect(LineSegment Line1, LineSegment Line2, out Vector2 ptIntersection)
        //{
        //    // Raycasting in 2D (line segment intersection)
        //    // https://www.youtube.com/watch?v=c065KoXooSw

        //    Vector2 r = (Line1.P2 - Line1.P1);    // Find the direction vector of the first line segment presented by P1 and P2
        //    Vector2 s = (Line2.P2 - Line2.P1);    // Find the direction vector of the first line segment presented by ls.P1 and ls.P2

        //    // t,u are the intersection point location relative to the specific line segment
        //    float t = MathUtil.VectorProduct((Line2.P1 - Line1.P1), s) / MathUtil.VectorProduct(r, s);
        //    float u = MathUtil.VectorProduct((Line2.P1 - Line1.P1), r) / MathUtil.VectorProduct(r, s);

        //    if (t >= 0d && t <= 1d && u >= 0d && u <= 1d)
        //    {
        //        Vector2 intersectionPoint1 = Line1.P1 + t * r;
        //        Vector2 intersectionPoint2 = Line2.P1 + u * s;

        //        ptIntersection = intersectionPoint1;
        //        return true;
        //    }

        //    ptIntersection = Vector2.Zero;
        //    return false;
        //}
    }
}
