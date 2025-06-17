using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector2 = System.Numerics.Vector2;
using Color = Microsoft.Xna.Framework.Color;

namespace Project3
{
    public class Button
    {
        public string Type;
        public string name;
        public Vector2 position = new Vector2(300,44);
        public string text = "Button1";
        public Color color = Color.AliceBlue;
        bool pressed;
        public Vector2 scale;
        public SpriteFont font;
        public Texture2D background;

        public List<Texture2D> Textures = new List<Texture2D>();
        public void reset()
        {
            pressed = false;
        }

        

        
    }
}
