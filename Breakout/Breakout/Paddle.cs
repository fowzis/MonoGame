using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    class Paddle : Entity
    {
        public static Paddle Instance { get; private set; }

        #region Public Properties
        public float Speed { get; set; }

        public override Vector2 Position
        {
            get { return position; }
            // Set new position while clamping to screen boundaries
            set { position = Vector2.Clamp(value, Vector2.Zero, Game1.ScreenSize - Size); }
        }
        #endregion

        public Paddle()
        {
            Instance = this;
            SourceRect = Art.SRect_VecPaddleSmall;

            // Set Initial Speed
            Speed = 9;

            // Starting position of the paddle
            Position = new Vector2(Game1.Viewport.Width / 2, Game1.Viewport.Height - Art.SRect_PaddleSmall.Height);
        }

        public override void Update()
        {
            Velocity += Speed * InputHandler.GetMovementDirection();
            Position += Velocity;
            Velocity = Vector2.Zero;
        }
    }
}
