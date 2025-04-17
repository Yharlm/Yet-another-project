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
        public Vector2 current_velocity = new Vector2(0, 20);

        public void Apply_Velocity()
        {

            Position += current_velocity;
        }

        public void Add_velocity(Vector2 Force) 
        {
            current_velocity = Force;
        }
    }
    internal class Entity : Game1
    {
        public Velocity Velocity = new Velocity();
        public Vector2 Position;
        public float rotation;
        public float health;
        public int ID;
        public Texture2D Texture;
        public float scale;
        public string name;
        public List<Entity> Load_Mobs()
        {
            List<Entity> list = new List<Entity>();
            Entity mob = new Entity();

            mob.Position = player.position;
            mob.name = "Dirt";
            mob.ID = 1;
            mob.scale = 6;
            mob.rotation = 0;
            mob.health = 10;
            list.Add(mob);
            return list;

        }

    }
}
