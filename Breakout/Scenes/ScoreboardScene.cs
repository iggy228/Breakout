using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout
{
    public class ScoreboardScene : Scene
    {
        private SpriteFont _spriteFont;

        private Button _backButton;

        public ScoreboardScene(Game1 game) : base(game)
        {
        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
            _spriteFont = game.Content.Load<SpriteFont>("arial");

            _backButton = new Button(_spriteFont, new Vector2(200, 300), new Vector2(100, 30), "Play");
            _backButton.OnClicked += () =>
            {
                game.currentScene = new MainMenuScene(game);
                game.currentScene.Initialize();
                game.currentScene.LoadContent();
            };
        }

        public override void Update(GameTime gameTime)
        {
            _backButton.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Globals.spriteBatch.Begin();

            Globals.spriteBatch.DrawString(_spriteFont, "Scoreboard", new(200, 10), Color.White);
            for (int i = 0; i < Globals.scoreboard.Scores.Length; i++)
            {
                Globals.spriteBatch.DrawString(_spriteFont, $"{i + 1}. {Globals.scoreboard.Scores[i]}", new(200, 40 + i * 20), Color.White);
            }
            _backButton.Draw();

            Globals.spriteBatch.End();
        }
    }
}
