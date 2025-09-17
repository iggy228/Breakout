using Microsoft.Xna.Framework.Graphics;

namespace Breakout
{
    public delegate void VoidCallback();

    internal class Globals
    {
        public static SpriteBatch spriteBatch;

        public static Texture2D whitePixel;

        public static int windowWidth = 640;
        public static int windowHeight = 480;

        public static int BALL_START_X = 320;
        public static int BALL_START_Y = 350;

        public static int PADDLE_START_X = 320;
        public static int PADDLE_START_Y = 470;

        public static int score = 0;

        public static Scoreboard scoreboard;

        public static int BRICKS_ROW_COUNT = 5;
        public static int BRICKS_COLUMN_COUNT = 8;
    }
}
