using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Project3
{
    class Minecraft
    {
        public int[,] grid;
        public int[,] Background;
        public List<Block> Block_list = new List<Block>();
        public List<Entity> Entities = new List<Entity>();
        public List <Entity> Existing_entities = new List<Entity>();

        public void spawn_ent(string name,System.Numerics.Vector2 pos)
        {
            Entity mob = new Entity();
            Entity entity = Entities.Find(x => x.name == name);
            mob.health = 0;
            mob.Position = pos;
            mob.name = entity.name;
            mob.color = entity.color;   
            mob.Velocity = new Velocity();
            Existing_entities.Add(mob);
            Existing_entities.Last().Position = pos;
            

        }
        public void spawn_ent(Entity mob)
        {
            
            Entity entity = new Entity();
            entity.health = 0;
            entity.Position = mob.Position;
            entity.name = mob.name;
            entity.scale = mob.scale;
            entity.Velocity = mob.Velocity;
            Existing_entities.Add(entity);
            Existing_entities.Last().Position = mob.Position;


        }
        public void Fill_block(int x, int y, Block Block)
        {

            if (x > 0 || y > 0)
            {
                grid[y, x] = Block.id;
                //Background[y, x] = Block.id;
            }
            
            //WriteAt(Block.Texture, x * 2, y);

        }
        public static void Caves(int x, int y, int[,] grid,int[,] background)
        {
            Random random = new Random();

            int size = random.Next(2, 5);


            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == size - 1 && j == size - 1 || i == 0 && j == 0 || i == size - 1 && j == 0 || i == 0 && j == size - 1)
                    {
                        if (random.Next(0, 3) == 1 && grid[i + y, j + x] == 3)
                        {
                            Caves(x - size / 2 + i, y - size / 2 + j, grid, background);
                        }
                        continue;
                    }
                    grid[j + y, i + x] = 0;
                    background[j + y, i + x] = 0;
                }
            }
        }
        public void Caves(int x, int y)
        {
            Random random = new Random();

            int size = random.Next(2, 5);


            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == size - 1 && j == size - 1 || i == 0 && j == 0 || i == size - 1 && j == 0 || i == 0 && j == size - 1)
                    {
                        if (random.Next(0, 3) == 1 && grid[i + y, j + x] == 3)
                        {
                            Caves(x - size / 2 + i, y - size / 2 + j);
                        }
                        continue;
                    }
                    grid[j + y, i + x] = 0;
                }
            }
        }

        public void Caves(int x, int y, Block solid, bool replace)
        {
            Random random = new Random();

            int size = random.Next(2, 5);
            int id = -1;
            if (replace)
            {
                id = 0;
            }


            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == size - 1 && j == size - 1 || i == 0 && j == 0 || i == size - 1 && j == 0 || i == 0 && j == size - 1)
                    {
                        if (random.Next(0, j) == 1 && grid[i + y, j + x] != id)
                        {
                            Caves(x - size / 2 + i, y - size / 2 + j);
                        }
                        continue;
                    }
                    grid[j + y, i + x] = solid.id;
                }
            }

        }

        public void Break_block(int x, int y, Player player)
        {

            var block = Get_ByID(grid[y, x]);
            if (block == null)
            {
                return;

            }
            player.Breaking_stage -= 1 / (0.5f * block.stength);
            if (player.Breaking_stage > 0)
            {

            }
            else
            {
                
                player.Breaking_stage = 10;
                
                if (Contains(player.Inventoy,block) )             
                {
                    AddAmmountItem(player.Inventoy, block);
                }
                else
                {
                    block.quantity = 1;
                    AddItem(player.Inventoy,block);
                }
                grid[y, x] = 0;
            }

        }

        public static bool Contains(Block[,] list, Block item)
        {
            
            foreach (var block in list)
            {
                if (block.id == item.id)
                {
                    return true;
                }
            }
            return false;
        }
        public static void AddAmmountItem(Block[,] list, Block item)
        {
            if (item.id == 0)
            {
                return;
            }
            for (int i = 0; i < list.GetLength(0); i++)
            {
                for (int j = 0; j < list.GetLength(1); j++)
                {
                    if (list[i, j].id == item.id)
                    {
                        list[i, j].quantity += 1;
                    }
                }
            }

        }

        public static void AddItem(Block[,] list, Block item)
        {

            for (int i = 0; i < list.GetLength(0); i++)
            {
                for(int j = 0;j < list.GetLength(1);j++)
                {
                    if (list[i, j].id == 0)
                    {
                        list[i, j] = item;
                        return;
                    }
                    
                }
            }

        }
        public void Place_block(int x, int y, Player player)
        {
            
            var block = Block_list.Find(x => x.id == player.Inventoy[0,player.index-1].id);
            if(block == null)
            {
                return;
            }
            if (player.Inventoy[0,player.index-1].id != 0 && player.Inventoy[0, player.index - 1].quantity > 0)
            {
                if(grid[y, x] != 0)
                {
                    return;
                }
                grid[y, x] = block.id;
                player.Inventoy[0, player.index - 1].quantity -= 1;
                for (int i = 0; i < player.Inventoy.GetLength(0); i++)
                {
                    for (int j = 0; j < player.Inventoy.GetLength(1); j++)
                    {
                        if (player.Inventoy[i, j].quantity < 0)
                        {
                            player.Inventoy[i, j] = Block_list[0];

                        }

                    }
                }
            }
        }
        public void Fill_Index_Cord2(int x1, int y1, int x2, int y2, Block Block, int randomiser)
        {
            Random random = new Random();
            for (int j = y1; j < y2; j++)
            {
                for (int i = x1; i < x2; i++)
                {
                    int e = random.Next(0, randomiser);
                    if (e == 0 || grid[j, i] == 0)
                    {
                        continue;
                    }

                    grid[j, i] = Block.id;
                }
            }

        }
        public Block GetBlock(string name)
        {
            return Block_list.Find(x => x.name == name);
        }
        public Block Get_ByID(int id)
        {
            return Block_list.Find(x => x.id == id);
        }
    }
}
