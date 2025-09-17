using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class Button
    {
        private SpriteFont font;
        private MouseState currentMouse, previousMouse;

        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public string Text { get; set; }
        public Color BackgroundColor { get; set; } = Color.Gray;
        public Color HoverColor { get; set; } = Color.DarkGray;
        public Color TextColor { get; set; } = Color.White;

        public bool IsHovering { get; private set; }
        public bool IsClicked { get; private set; }

        private Texture2D rectTexture;

        public event VoidCallback OnClicked;

        private Point mousePos;

        public Button(SpriteFont font, Vector2 position, Vector2 size, string text)
        {
            this.font = font;
            Position = position;
            Size = size;
            Text = text;

            // Create a 1x1 white texture (used for rectangle drawing)
            rectTexture = Globals.whitePixel;
        }

        public void Update(GameTime gameTime)
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            Rectangle buttonRect = new(Position.ToPoint(), Size.ToPoint());
            mousePos = currentMouse.Position;

            IsHovering = buttonRect.Contains(mousePos);

            // Detect click
            IsClicked = false;
            if (IsHovering &&
                currentMouse.LeftButton == ButtonState.Pressed &&
                previousMouse.LeftButton == ButtonState.Released)
            {
                IsClicked = true;
                OnClicked?.Invoke();
            }
        }

        public void Draw()
        {
            // Background
            Color drawColor = IsHovering ? HoverColor : BackgroundColor;
            Globals.spriteBatch.Draw(rectTexture, new Rectangle(Position.ToPoint(), Size.ToPoint()), drawColor);

            // Center the text
            Vector2 textSize = font.MeasureString(Text);
            Vector2 textPos = Position + (Size - textSize) / 2f;

            Globals.spriteBatch.DrawString(font, Text, textPos, TextColor);
        }

    }
}
