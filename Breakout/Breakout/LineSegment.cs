using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    class LineSegment
    {
        public Vector2 P1 { get; private set; }
        public Vector2 P2 { get; private set; }
        public BrickSide BrickSide { get; set; }

        // P1 to P2 gives the correct directio od the vector
        public float Length { get { return (P1 - P2).Length(); } }  // Length or Magnitude ||W|| = sqrt(sqr(dX) + sqr(dY))
        public float Angle { get { return (P1 - P2).ToAngleRad(); } }  // The Vector angle relative to the 0,0 origin point.

        public LineSegment(Vector2 p1, Vector2 p2, BrickSide brickSide = BrickSide.None)
        {
            this.P1 = p1;
            this.P2 = p2;
            this.BrickSide = brickSide;
        }
    }
}
