using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        private Paddle _paddle;
        private Ball _ball;
        private SpriteFont _spriteFont;

        private readonly BreakableBox[] breakableBoxes = new BreakableBox[6 * 4];

        public int lives = 3;

        public float startDelay = 0.5f;
        private float curStartDelay = 0.5f;

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

            _paddle = new Paddle();
            _ball = new Ball();
            _ball.OnBallFallout += () =>
            {
                lives--;
                if (lives == 0)
                {
                    Exit();
                }
                else
                {
                    _ball.Position = new(Globals.BALL_START_X, Globals.BALL_START_Y);
                    _ball.dir = new(0, 1);

                    _paddle.rect.X = Globals.PADDLE_START_X;

                    curStartDelay = startDelay;
                }
            };


            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    breakableBoxes[i * 4 + j] = new BreakableBox(120 + i * 50 + i * 20, 40 + j * 20 + j * 10);
                }
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);

            Globals.whitePixel = Texture2D.FromFile(GraphicsDevice, "../../../Assets/white_pixel.bmp");

            _spriteFont = Content.Load<SpriteFont>("arial");
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

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

            base.Draw(gameTime);
        }
    }
}
