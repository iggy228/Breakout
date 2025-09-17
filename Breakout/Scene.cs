using Microsoft.Xna.Framework;

namespace Breakout
{
    public abstract class Scene
    {
        protected Game1 game;

        public Scene(Game1 game)
        {
            this.game = game;
        }

        public abstract void Initialize();
        public abstract void LoadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }
}
