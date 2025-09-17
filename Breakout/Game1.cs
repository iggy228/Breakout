using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        public GameState gameState = GameState.Playing;

        public Scene currentScene;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = Globals.windowWidth;
            _graphics.PreferredBackBufferHeight = Globals.windowHeight;
            _graphics.ApplyChanges();

            Globals.scoreboard = new Scoreboard();

            currentScene = new MainMenuScene(this);
            currentScene.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);

            Globals.whitePixel = Texture2D.FromFile(GraphicsDevice, "../../../Assets/white_pixel.bmp");

            currentScene.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            currentScene.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            currentScene.Draw(gameTime);

            base.Draw(gameTime);
        }

        public void ChangeScene(Scene newScene)
        {
            currentScene = newScene;
            currentScene.Initialize();
            currentScene.LoadContent();
        }
    }
}
