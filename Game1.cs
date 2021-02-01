using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SquareCatch
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Random randNumber = new Random();
        Texture2D squareTexture;
        Rectangle currentSquare;
        int playerScore = 0;
        float timeRemaining = 0.0f;
        float timePerSquare = 3.0f;
        Color[] colors = new Color[3] { Color.Red, Color.Green, Color.Blue };

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            squareTexture = Content.Load<Texture2D>("square");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (timeRemaining == 0.0f)
            {
                currentSquare = new Rectangle(randNumber.Next(0, this.Window.ClientBounds.Width - 25), randNumber.Next(0, this.Window.ClientBounds.Height - 25), 25, 25);
                timeRemaining = timePerSquare;
            }

            MouseState mouse = Mouse.GetState();
            if((mouse.LeftButton == ButtonState.Pressed) && (currentSquare.Contains(mouse.X, mouse.Y)))
            {
                playerScore++;
                timeRemaining = 0.0f;
                timePerSquare = timePerSquare - 0.1f;
            }
            timeRemaining = MathHelper.Max(0, timeRemaining - (float)gameTime.ElapsedGameTime.TotalSeconds);
            this.Window.Title = "Score: " + playerScore.ToString();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(squareTexture, currentSquare, colors[playerScore % 3]);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
