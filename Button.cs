using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project3
{
    public class Button
    {
        public string name;
        public Vector2 position = new Vector2(0,0);
        public string text = "Button1";
        public Color color;
        bool pressed;
        public SpriteFont font;
        public Texture2D background;

        public void reset()
        {
            pressed = false;
        }

        public static void RenderUI(SpriteBatch spriteBatch, Button button)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(button.background, button.position,null, Microsoft.Xna.Framework.Color.White, 0f, Vector2.One, Vector2.Zero, SpriteEffects.None, 1f);
            spriteBatch.DrawString(button.font, button.text, button.position, Microsoft.Xna.Framework.Color.Black);
            spriteBatch.End();

        }
    }
}
