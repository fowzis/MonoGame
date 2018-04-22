using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Breakout
{
    // Singleton Pattern
    // Thread-safe without using locks, not quite as lazy
    public static class InputHandler
    {
        private static KeyboardState currentKeyboardState, previousKeyboardState;
        private static MouseState mouseState, lastMouseState;

        private static bool isAimingWithMouse = false;

        #region Class Properties
        // Private
        private static GameTime GameTime { get; set; }
        // Public
        public static Keys[] PressedKeys { get; private set; }

        public static Vector2 MousePosition { get { return new Vector2(mouseState.X, mouseState.Y); } }
        #endregion

        static InputHandler() { }

        public static void Update(GameTime gameTime)
        {
            GameTime = gameTime;

            // Save previous input state at every refresh
            previousKeyboardState = currentKeyboardState;
            lastMouseState = mouseState;

            // Get newinput state
            currentKeyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            if (MousePosition != new Vector2(lastMouseState.X, lastMouseState.Y))
                isAimingWithMouse = true;
        }

        /// <summary>
        /// Check if a key is pressed.
        /// </summary>
        public static bool IsKeyPressed(Keys key)
        {
            return previousKeyboardState.IsKeyUp(key) && currentKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Check if a key was just pressed in the most recent update.
        /// </summary>
        public static bool IsKeyTriggered(Keys key)
        {
            return (currentKeyboardState.IsKeyDown(key)) && (!previousKeyboardState.IsKeyDown(key));
        }

        public static Vector2 GetMovementDirection()
        {
            Vector2 direction = Vector2.Zero;
            
            if (currentKeyboardState.IsKeyDown(Keys.Left))
                direction.X -= 1;

            if (currentKeyboardState.IsKeyDown(Keys.Right))
                direction.X += 1;

            //if (currentKeyboardState.IsKeyDown(Keys.Left))
            //    direction.X -= (float)1.5;

            //if (currentKeyboardState.IsKeyDown(Keys.Right))
            //    direction.X += (float)1.5;

            return direction;
        }
    }
}
