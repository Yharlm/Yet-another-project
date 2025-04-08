using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Project3
{
    public class Player
    {
        public Vector2 camera = new Vector2(0, 0);
        public float Zoom = 1.0f;
        public int id_copy = 0;
        public Vector2 position = new Vector2(23, 5);
        public float gravity = 0.1f;
        public bool is_walking = false;
        public AnimatedTexture player_texture;
        public AnimatedTexture player_walkL;
        public AnimatedTexture player_walkR;
        public string action = "idle";
        public bool lock_input = false;

        public bool Jumped = false;
        public void jump()
        {
            if (true)
            {   jump_val =-0.3f*(float)Math.Sin(JumpPower);
                if(jump_val > 0)
                {
                    is_falling_fast = true;
                }
                if(is_falling_fast)
                {
                    camera.Y += 0.1f;
                }
                else
                {
                    JumpPower += 0.1f;
                    camera.Y += jump_val;
                }
                
                

            }
            
        }
        public bool is_falling_fast = false;
        public float JumpPower = 0.5f;
        public float jump_val = 0;
        public bool has_jumped = false;
        
    }
}
