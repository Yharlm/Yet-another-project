using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Project3
{
    class Velocity : Entity
    {
        public Vector2 current_velocity = new Vector2(0, 0.1f);

        

        public void Add_velocity(Vector2 Force) 
        {
            current_velocity = Force;
        }
    }
    class Entity
    {
        public Velocity Velocity;
        public Vector2 Position;
        public float rotation;
        public float health;
        public int ID;
        public Texture2D Texture;
        public float scale;
        public string name;
        public static List<Entity> Load_Mobs()
        {
            List<Entity> list = new List<Entity>();
            Entity mob = new Entity();
            mob.Velocity = new Velocity();
            
            mob.name = "Dirt";
            
            mob.ID = 1;
            mob.scale = 6;
            mob.rotation = 0;
            mob.health = 10;
            list.Add(mob);
            return list;

        }
        public void Apply_Velocity()
        {
            
            Position += Velocity.current_velocity/10;
            if(Velocity.current_velocity.Y < 0)
            {
                Velocity.current_velocity.Y += 1f;
            }
            else if (Velocity.current_velocity.Y > 0)
            {
                Velocity.current_velocity.Y -= 1f;
            }
            //if (Velocity.current_velocity.X > 0.1)
            //{
            //    Velocity.current_velocity.X += 0.1f;
            //}
            //else
            //{
            //    Velocity.current_velocity.X -= 0.1f;
            //}

        }

    }
}
