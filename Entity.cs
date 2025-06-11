using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Metadata;
using Color = Microsoft.Xna.Framework.Color;
using Vector2 = System.Numerics.Vector2;

namespace Project3
{
    class Behaviour
    {
        public string Mode = "Idle";
        public Vector2 TargetPos = Vector2.Zero;
        public int Failed_attempts = 0;
        public bool Retreat = false;
        public bool ChargeAtPlr = false;
        public bool LookForPlayer = false;
        public bool LowHP = false;
        public bool StopPLmidAir = false;
    }

    class Collision_box
    {
        public bool Bootom = false;
        public bool Top = false;
        public bool Left = false;
        public bool Right = false;

    }

    class Velocity
    {
        public Vector2 current_velocity = new Vector2(0f, 0f);
        public float Drag = 0.05f;


        public void Add_velocity(Vector2 Force)
        {
            //current_velocity = Force;
            current_velocity += Force / 10;
        }
    }
    class Entity
    {

        public Color color = Color.Green;
        public Behaviour behaviour = new Behaviour();

        public float MaxJumpPower = 2;
        public float walkspeed = 1;
        public Collision_box collision = new Collision_box();
        public Velocity Velocity = new Velocity();
        public Vector2 Position;
        public float rotation;
        public float health;
        public int ID;
        public Texture2D Texture;
        public float scale;
        public string name = "Name";
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
        public void Walk2Player()
        {
            Random random   = new Random();
            //Walking and jumping
            if (collision.Bootom && collision.Left || collision.Right)
            {
                Velocity.current_velocity.Y = -MaxJumpPower * 3;

            }
            if (behaviour.TargetPos.X < Position.X)
            {
                Velocity.current_velocity.X = -walkspeed;


            }
            else
            {
                Velocity.current_velocity.X = walkspeed;

            }
            //if the player is unreachable for a long time gives up
            if (behaviour.Mode == "Idle")
            {
                int direction = random.Next(-1, 2);
                
                
                    behaviour.TargetPos.X = Position.X + direction * 10;
                
            }
            //if (behaviour.Failed_attempts > 130)
            //{
            //    color = Color.Red;
            //    Velocity.current_velocity.X = -Velocity.current_velocity.X;
            //    behaviour.TargetPos = Vector2.One;
            //    behaviour.Failed_attempts = 0;
            //}
            //if (behaviour.TargetPos.X > Position.X - 8 || behaviour.TargetPos.X < Position.X + 8)
            //{

            //    if (collision.Left || collision.Right)
            //    {
            //        behaviour.Failed_attempts += 1;
            //    }
            //}



        }

        public static bool CheckVertice(Vector2 Offset, Entity Mob, Player plr, float block_gap, float relative_block_size, int[,] grid)
        {
            Vector2 mousepos = (Mob.Position + Offset);
            double x = Math.Ceiling((double)mousepos.X / block_gap * relative_block_size);
            double y = Math.Ceiling((double)mousepos.Y / block_gap * relative_block_size);

            if (mousepos.X <= 0 || mousepos.Y <= 0)
            {
                return false;
            }
            if (grid[(int)mousepos.Y, (int)mousepos.X] == 0)
            { return true; }
            return false;
        }
        public static void TestCheckVertice(Vector2 Offset, Entity Mob, Player plr, float block_gap, float relative_block_size, int[,] grid)
        {
            Vector2 mousepos = (Mob.Position + Offset);
            double x = Math.Ceiling((double)mousepos.X / block_gap * relative_block_size);
            double y = Math.Ceiling((double)mousepos.Y / block_gap * relative_block_size);

            grid[(int)mousepos.Y, (int)mousepos.X] = 2;
        }

        public void Apply_Velocity()
        {
            var Box = collision;

            if (Velocity.current_velocity.Y > 0 && !Box.Bootom)
            {
                Position.Y += Velocity.current_velocity.Y * Velocity.Drag;
                //Velocity.current_velocity.Y += 0.2f;
            }

            else if (Velocity.current_velocity.Y < 0 && !Box.Top)
            {
                Position.Y += Velocity.current_velocity.Y * Velocity.Drag;
                //Velocity.current_velocity.Y -= 0.2f;
            }

            if (Velocity.current_velocity.X > 0 && !Box.Right)
            {
                Position.X += Velocity.current_velocity.X * Velocity.Drag;
                //Velocity.current_velocity.X += 0.2f;
            }

            else if (Velocity.current_velocity.X < 0 && !Box.Left)
            {
                Position.X += Velocity.current_velocity.X * Velocity.Drag;
                //Velocity.current_velocity.X -= 0.2f;
            }




        }

    }
}
