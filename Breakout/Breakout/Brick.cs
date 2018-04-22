using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    // Powers of two
    [Flags]
    public enum BrickSide
    {
        // Decimal      // Binary
        None = 0,       // 000000
        Top = 1,        // 000001
        Bottom = 2,     // 000010
        Right = 4,      // 000100
        Left = 8        // 001000
    }

    class Brick : Entity
    {
        private int hitPoints = 10;
        private int rewardPoints = 5;

        public Brick(string brickName, Vector2 position, int scale = 1)
        {
            Initialize(brickName, position, new Vector2(scale));
        }

        public Brick(string brickName, Vector2 position, Vector2 scale)
        {
            Initialize(brickName, position, scale);
        }

        private void Initialize(string brickName, Vector2 position, Vector2 scale)
        {
            this.SourceRect = Art.GetSourceRectangle(brickName);
            this.Position = position;
            this.Scale = scale;
        }

        public override void Update()
        {
            //throw new NotImplementedException();
        }

        public void WasHit()
        {
            hitPoints--;
            if (hitPoints <=0)
            {
                IsExpired = true;

                // TODO - Brick - Handle givin player reward points for each brick
                // check brick type should have a different reward points
            }

            // TODO - Brick - Play Hit Sound

            // TODO - Brick - Create explosion particles
        }

        public void Kill()
        {
            hitPoints = 0;
            WasHit();
        }
    }
}
