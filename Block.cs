using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3
{
    public class Block
    {
        public string Category;
        public int quantity = 1;
        public int id = -1;
        public Texture2D Texture;
        public string name = string.Empty;
        public bool is_collidable = true;
        public bool is_backround = false;
        public int stength = 1;
        public bool apaque = false;

    }
}
