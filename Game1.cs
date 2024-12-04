using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using static System.Formats.Asn1.AsnWriter;

namespace FishRunner
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D fishTexture;
        private Texture2D harpoonTexture;
        private Texture2D hunterTexture;
        private SpriteFont gameFont;
        private Texture2D underwaterBackground;

        private List<Fish> fishes = new List<Fish>();
        private Hunter player;
        private Controller gameController;

        private int elapsedTime = 0;
        private bool gameWon = false;
        private bool gameOver = false;
        private int score = 0;

        // Declare scale factors for different images
        float backgroundScale = 1.0f;  // Scale factor for the background
        float fishScale = 0.05f;        // Scale factor for fish
        float harpoonScale = 0.05f;     // Scale factor for harpoon
        float hunterScale = 0.05f;      // Scale factor for hunter


        private Random random = new Random();

        private enum GameState
        {
            Menu,
            Playing,
            GameOver
        }

        private GameState currentState = GameState.Menu;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            // _spriteBatch = new SpriteBatch(GraphicsDevice);
            underwaterBackground = Content.Load<Texture2D>("underwater");
            fishTexture = Content.Load<Texture2D>("fish");
            harpoonTexture = Content.Load<Texture2D>("harpoon");
            hunterTexture = Content.Load<Texture2D>("hunter");
            gameFont = Content.Load<SpriteFont>("GameFont");

            player = new Hunter(new Vector2(100, 400));
            gameController = new Controller();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            switch (currentState)
            {
                case GameState.Menu:
                    UpdateMenuState();
                    break;

                case GameState.Playing:
                    UpdatePlayingState();
                    break;

                case GameState.GameOver:
                    UpdateGameOverState();
                    break;
            }



            base.Update(gameTime);
        }

        private void UpdateMenuState()
        {
            // Check for user input to start the game (e.g., pressing Enter)
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                currentState = GameState.Playing;
            }
        }

        private void UpdatePlayingState()
        {
            // Update the player and fish
            player.Update();

            // Spawn fish
            if (elapsedTime % 60 == 0) // Spawn fish every second
            {
                int speed = random.Next(2, 6);
                Vector2 position = new Vector2(1200, random.Next(50, 750));
                fishes.Add(new Fish(speed, position));
            }

            // Update fish
            foreach (var fish in fishes)
            {
                fish.Update();
            }

            // Check collisions between harpoon and fish
            for (int i = fishes.Count - 1; i >= 0; i--)
            {
                if (gameController.CheckCollision(player.currentHarpoon, fishes[i]))
                {
                    score += 10;
                    fishes.RemoveAt(i);
                }
            }

            // Game timer and condition to end the game
            elapsedTime++;
            if (elapsedTime >= 600) // 10 seconds of gameplay
            {
                gameWon = true;
                currentState = GameState.GameOver;  // Switch to GameOver state when the game is won
            }
        }

        private void UpdateGameOverState()
        {
            // Handle any inputs to restart or exit (e.g., pressing Enter to restart)
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                // Reset the game for a new round
                score = 0;
                elapsedTime = 0;
                fishes.Clear();
                currentState = GameState.Playing;
            }
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here



            _spriteBatch.Begin();

            switch (currentState)
            {
                case GameState.Menu:
                    DrawMenuState();
                    break;

                case GameState.Playing:
                    DrawPlayingState();
                    break;

                case GameState.GameOver:
                    DrawGameOverState();
                    break;
            }



            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawMenuState()
        {
            // Draw the menu background and instructions
            _spriteBatch.DrawString(gameFont, "Welcome to Fish Runner!", new Vector2(400, 200), Color.White);
            _spriteBatch.DrawString(gameFont, "Press Enter to Start", new Vector2(450, 300), Color.White);
        }

        private void DrawPlayingState()
        {
            // Draw the underwater background
            _spriteBatch.Draw(underwaterBackground,
                              new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight),
                              Color.White);

            // Draw fish, harpoon, and hunter
            foreach (var fish in fishes)
            {
                _spriteBatch.Draw(fishTexture, fish.position, null, Color.White, 0f, Vector2.Zero, fishScale, SpriteEffects.None, 0f);
            }

            _spriteBatch.Draw(harpoonTexture, player.currentHarpoon.position, null, Color.White, 0f, Vector2.Zero, harpoonScale, SpriteEffects.None, 0f);
            _spriteBatch.Draw(hunterTexture, player.position, null, Color.White, 0f, Vector2.Zero, hunterScale, SpriteEffects.None, 0f);

            // Draw score
            _spriteBatch.DrawString(gameFont, "Score: " + score, new Vector2(10, 10), Color.White);
        }

        private void DrawGameOverState()
        {
            string message = gameController.GetGameOverMessage(gameWon);
            _spriteBatch.DrawString(gameFont, message, new Vector2(400, 400), Color.White);
            _spriteBatch.DrawString(gameFont, "Press Enter to Restart", new Vector2(400, 500), Color.White);
        }
    }
}
