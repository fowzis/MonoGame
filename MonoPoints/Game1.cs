using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Shakkour.MonoGame.Helper;

namespace MonoPoints
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont scoreFont;
        Texture2D pointTexture;

        VertexBufferTextured VertexBuffer;

        int vertexSize = 10;
        Color vertexColor = Color.White;

        ArrayList collorArray;
        IDictionary<string, Color> colorsDictionary;
        int colorIndex;

        // Mouse Related Variables
        int prevScrollWheelValue = 0;
        ButtonState rbPrevState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // Instanciate the list of VertexPosition 
            ColorsHelper ch = new ColorsHelper();
            colorsDictionary = ch.GetColorsDictionary();
            collorArray = ch.GetColorsArray();
            colorIndex = collorArray.IndexOf(vertexColor);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            pointTexture = Content.Load<Texture2D>(@"Resources\Point");
            scoreFont = Content.Load<SpriteFont>(@"Fonts\Linds");

            VertexBuffer = new VertexBufferTextured();
            VertexBuffer.Initialize(pointTexture);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            VertexBuffer.Clear();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            HandleUserInput();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            VertexBuffer.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        #region Private Methods
        private void HandleUserInput()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Allows the game to exit
            MouseState ms = Mouse.GetState();

            this.Window.Title = "(X = " + ms.X.ToString() + " , Y = " + ms.Y.ToString() + ") " +
                "(LB: " + (ms.RightButton == ButtonState.Pressed).ToString() + " , " +
                "RB: " + (ms.LeftButton == ButtonState.Pressed).ToString() + "), " +
                "(Points: " + VertexBuffer.Count.ToString() + "), " +
                //"(ScrollWheel: " + ms.ScrollWheelValue.ToString() + ", " + prevScrollWheelValue.ToString() + "), " +
                "(Point Size: " + vertexSize.ToString() + "), " +
                //"(Points exists: " + skippedPoints.ToString() + "), " +
                "(Colors: " + collorArray.Count + ", Index: " + colorIndex.ToString() + ")";

            if (ms.LeftButton == ButtonState.Pressed)
            {
                if (vertexSize > 0)
                {
                    VertexBuffer.Add(ms.X, ms.Y, vertexSize, vertexColor);
                }
            }

            // Increment to next color
            if (rbPrevState == ButtonState.Pressed && ms.RightButton == ButtonState.Released)
            {
                if (++colorIndex > collorArray.Count - 1)
                    colorIndex = 0;

                vertexColor = (Color) collorArray[colorIndex];
            }
            rbPrevState = ms.RightButton;

            // Modify vertex size
            if (ms.ScrollWheelValue > prevScrollWheelValue)
            {
                vertexSize++;
                prevScrollWheelValue = ms.ScrollWheelValue;
            }
            else if (ms.ScrollWheelValue < prevScrollWheelValue)
            {
                if (vertexSize > 1)
                {
                    vertexSize--;
                    prevScrollWheelValue = ms.ScrollWheelValue;
                }
                else
                {
                    vertexSize = 1;
                }
            }
        }
        #endregion Private Methods
    }
}
