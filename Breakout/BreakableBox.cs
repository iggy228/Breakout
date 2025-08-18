
using Microsoft.Xna.Framework;
using System;

namespace Breakout
{
    internal class BreakableBox(int x, int y)
    {
        private const int BOX_WIDTH = 50;
        private const int BOX_HEIGHT = 20;

        public Rectangle rect = new(x, y, BOX_WIDTH, BOX_HEIGHT);
        public bool isActive = true;

        public event VoidCallback OnBreak;

        public void Update(Ball ball)
        {
            if (!isActive)
            {
                return;
            }

            if (ball.rect.Intersects(rect))
            {
                isActive = false;
                Globals.score += 10;
                ball.IncreaseSpeedBy(10);

                // list of values of boxes to ball overlaps
                int overlapLeft = ball.rect.Right - rect.Left;
                int overlapRight = rect.Right - ball.rect.Left;
                int overlapTop = ball.rect.Bottom - rect.Top;
                int overlapBottom = rect.Bottom - ball.rect.Top;

                // Find the smallest overlap, because when overlap is smallest it means it touching that side the most (when left overlap is smallest it means ball is touching box from left side o|)  
                int minOverlap = Math.Min(Math.Min(overlapLeft, overlapRight), Math.Min(overlapTop, overlapBottom));

                // check if only top and bottom edges are touching the box
                if (minOverlap == overlapTop || minOverlap == overlapBottom)
                {
                    ball.dir.Y = -ball.dir.Y;
                }
                // check if only right and left edges are touching the box
                if (minOverlap == overlapLeft || minOverlap == overlapRight)
                {
                    ball.dir.X = -ball.dir.X;
                }
            }
        }

        public void Draw()
        {
            if (isActive)
            {
                Globals.spriteBatch.Draw(Globals.whitePixel, rect, Color.White);
            }
        }
    }
}
