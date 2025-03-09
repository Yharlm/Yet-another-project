﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3
{
    public class Block
    {
        public int id = -1;
        public Texture2D Texture;
        public string name = string.Empty;
        public bool is_collidable = true;
        public int stength = 1;

    }
}
