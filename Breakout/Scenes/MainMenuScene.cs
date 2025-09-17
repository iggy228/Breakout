using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout
{
    public class MainMenuScene : Scene
    {
        private SpriteFont _spriteFont;

        private Button _playButton;
        private Button _scoreboardButton;
        private Button _exitButton;

        public MainMenuScene(Game1 game) : base(game)
        {
        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
            _spriteFont = game.Content.Load<SpriteFont>("arial");

            _playButton = new Button(_spriteFont, new Vector2(200, 50), new Vector2(100, 30), "Play");
            _playButton.OnClicked += () =>
            {
                game.currentScene = new GameScene(game);
                game.currentScene.Initialize();
                game.currentScene.LoadContent();
            };

            _scoreboardButton = new Button(_spriteFont, new Vector2(200, 120), new Vector2(100, 30), "Scoreboard");
            _scoreboardButton.OnClicked += () =>
            {
                game.currentScene = new ScoreboardScene(game);
                game.currentScene.Initialize();
                game.currentScene.LoadContent();
            };

            _exitButton = new Button(_spriteFont, new Vector2(200, 200), new Vector2(100, 30), "Exit");
            _exitButton.OnClicked += game.Exit;
        }

        public override void Update(GameTime gameTime)
        {
            _playButton.Update(gameTime);
            _scoreboardButton.Update(gameTime);
            _exitButton.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Globals.spriteBatch.Begin();

            Globals.spriteBatch.DrawString(_spriteFont, "Breakout", new(200, 10), Color.White);
            // Globals.spriteBatch.DrawString(_spriteFont, $"Score {Globals.score}", new(560, 10), Color.White);

            _playButton.Draw();
            _scoreboardButton.Draw();
            _exitButton.Draw();

            Globals.spriteBatch.End();
        }
    }
}
