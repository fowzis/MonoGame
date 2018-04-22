using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Breakout
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        bool paused = false;

        // some helpful static properties
        public static Game1 Instance { get; private set; }
        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }
        public static GameTime GameTime { get; private set; }

        public Game1()
        {
            Instance = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Run this first will cause LoadContent to be triggered before initialize
            base.Initialize();

            // TO DO: Add your initialization logic here
            IsMouseVisible = true;

            // Instanciate the Paddle signleton and Add it to the EntitiesManager.
            EntityManager.Add(new Paddle());
            EntityManager.Add(new Ball());

            Vector2 pos = new Vector2(80, 80);
            Vector2 scale = new Vector2(1, 0.5f);
            Rectangle rect = Art.GetSourceRectangle("brick_01");

            for (int i = 0; i < 10; i++)
            {
                EntityManager.Add(new Brick("brick_01", pos, scale));
                pos += rect.Width * scale;
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TO DO: use this.Content to load your game content here
            Art.Load(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TO DO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            InputHandler.Update(gameTime);

            // Allows the game to exit
            if (InputHandler.IsKeyPressed(Keys.Escape))
                this.Exit();

            if (InputHandler.IsKeyPressed(Keys.P))
                paused = !paused;

            if (!paused)
            {
                EntityManager.Update();
            }

            if (InputHandler.IsKeyPressed(Keys.F))
                Ball.Instance.Freeze = !Ball.Instance.Freeze;

            if (InputHandler.IsKeyPressed(Keys.Add))
                Ball.Instance.Speed += 2;

            if (InputHandler.IsKeyPressed(Keys.Subtract))
                Ball.Instance.Speed -= 2;

            //Vector2 msPos = InputHandler.MousePosition;
            //this.Window.Title = "MS: (" + msPos.X + "," + msPos.Y + ") ";
            //this.Window.Title += "HitPoints: (" + EntityManager.HitPoints + ") ";
            //this.Window.Title += "Ball: (" + Ball.Instance.Position + ") ";

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TO DO: Add your drawing code here
            spriteBatch.Begin();

            EntityManager.Draw(spriteBatch);

            //Draw the timeSpan
            //spriteBatch.DrawString(Font.DefaultFont, ")

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
