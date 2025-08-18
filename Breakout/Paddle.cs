using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Breakout
{
    internal class Paddle
    {

        private const int PADDLE_WIDTH = 80;
        private const int PADDLE_HEIGHT = 10;

        private float speed = 400f;

        public Rectangle rect;

        public Paddle()
        {
            rect = new Rectangle(Globals.PADDLE_START_X, Globals.PADDLE_START_Y, PADDLE_WIDTH, PADDLE_HEIGHT);
        }

        public void Update(GameTime gameTime, Ball ball)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.A))
            {
                rect.X -= (int)(speed * (float)(gameTime.ElapsedGameTime.TotalSeconds));
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                rect.X += (int)(speed * (float)(gameTime.ElapsedGameTime.TotalSeconds));
            }

            if (rect.X < 0)
            {
                rect.X = 0;
            }
            if (rect.X + rect.Width > Globals.windowWidth)
            {
                rect.X = Globals.windowWidth - rect.Width;
            }

            if (ball.rect.Bottom > rect.Y)
            {
                if (ball.rect.Right > rect.X && ball.rect.X <= rect.Right)
                {

                    // math for unit distance ball from paddle center
                    float ballDistanceFromCenter = ball.rect.Center.X - rect.Center.X;
                    float halfPaddleWidth = (float)(rect.Width) / 2f;
                    float unitBallDistanceFromCenter = ballDistanceFromCenter / halfPaddleWidth;

                    // new direction for ball
                    Vector2 newDir = new(
                        unitBallDistanceFromCenter,
                        -1.15f + MathF.Abs(unitBallDistanceFromCenter)
                        );


                    newDir.Normalize();
                    ball.dir = newDir;
                }
            }
        }

        public void Draw()
        {
            Globals.spriteBatch.Draw(Globals.whitePixel, rect, Color.White);
        }
    }
}