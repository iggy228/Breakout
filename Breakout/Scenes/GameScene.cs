using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout
{
    public class GameScene : Scene
    {
        private Paddle _paddle;
        private Ball _ball;
        private SpriteFont _spriteFont;


        public int lives = 3;

        public float startDelay = 0.5f;
        private float curStartDelay = 0.5f;

        private readonly BreakableBox[] breakableBoxes = new BreakableBox[Globals.BRICKS_COLUMN_COUNT * Globals.BRICKS_ROW_COUNT];

        public GameScene(Game1 game) : base(game)
        {
        }

        public override void Initialize()
        {
            _paddle = new Paddle();
            _ball = new Ball();
            _ball.OnBallFallout += () =>
            {
                lives--;
                if (lives == 0)
                {
                    Globals.scoreboard.AddScore(Globals.score);

                    game.currentScene = new MainMenuScene(game);
                    game.currentScene.Initialize();
                    game.currentScene.LoadContent();
                }
                else
                {
                    _ball.Position = new(Globals.BALL_START_X, Globals.BALL_START_Y);
                    _ball.dir = new(0, 1);

                    _paddle.rect.X = Globals.PADDLE_START_X;

                    curStartDelay = startDelay;
                }
            };

            for (int i = 0; i < Globals.BRICKS_COLUMN_COUNT; i++)
            {
                for (int j = 0; j < Globals.BRICKS_ROW_COUNT; j++)
                {
                    breakableBoxes[i * Globals.BRICKS_ROW_COUNT + j] = new BreakableBox(90 + i * 55, 40 + j * 30);
                }
            }
        }

        public override void LoadContent()
        {
            _spriteFont = game.Content.Load<SpriteFont>("arial");
        }

        public override void Draw(GameTime gameTime)
        {
            Globals.spriteBatch.Begin();

            _paddle.Draw();
            _ball.Draw();

            for (int i = 0; i < breakableBoxes.Length; i++)
            {
                breakableBoxes[i].Draw();
            }

            Globals.spriteBatch.DrawString(_spriteFont, $"{lives} lives", new(20, 10), Color.White);
            Globals.spriteBatch.DrawString(_spriteFont, $"Score {Globals.score}", new(560, 10), Color.White);

            Globals.spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (curStartDelay <= 0f)
            {
                _paddle.Update(gameTime, _ball);
                _ball.Update(gameTime);

                for (int i = 0; i < breakableBoxes.Length; i++)
                {
                    breakableBoxes[i].Update(_ball);
                }
            }
            else
            {
                curStartDelay -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
