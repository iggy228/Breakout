
using Microsoft.Xna.Framework;

namespace Breakout
{
    internal class Ball
    {
        private const int BALL_SIZE = 8;

        private const float START_SPEED = 200f;
        private float speed = START_SPEED;

        public Rectangle rect;

        private Vector2 position;
        public Vector2 dir;

        public Vector2 Position
        {
            get => position;
            set
            {
                position = value;
                rect.X = (int)value.X;
                rect.Y = (int)value.Y;
            }
        }

        public event VoidCallback OnBallFallout;

        public Ball()
        {
            rect = new Rectangle(Globals.BALL_START_X + BALL_SIZE / 2, Globals.BALL_START_Y + BALL_SIZE / 2, BALL_SIZE, BALL_SIZE);
            position = rect.Center.ToVector2();
            dir = new Vector2(0, 1f);
        }

        public void Update(GameTime gameTime)
        {
            position.X += dir.X * speed * (float)(gameTime.ElapsedGameTime.TotalSeconds);
            position.Y += dir.Y * speed * (float)(gameTime.ElapsedGameTime.TotalSeconds);

            rect.X = (int)(position.X) - rect.Width / 2;
            rect.Y = (int)(position.Y) - rect.Height / 2;

            if (rect.Left < 0)
            {
                dir.X = -dir.X;
            }
            if (rect.Right > Globals.windowWidth)
            {
                dir.X = -dir.X;
            }
            if (rect.Top < 0)
            {
                dir.Y = -dir.Y;
            }
            if (rect.Bottom >= Globals.windowHeight)
            {
                OnBallFallout?.Invoke();
            }
        }

        public void Draw()
        {
            Globals.spriteBatch.Draw(Globals.whitePixel, rect, Color.Red);
        }

        public void IncreaseSpeedBy(float amount)
        {
            speed += amount;
        }

        public void ResetSpeed()
        {
            speed = START_SPEED;
        }
    }
}
