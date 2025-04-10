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
        public List<Block> Block_list = new List<Block>();
        public static void Fill_block(int x, int y, int[,] grid, Block Block)
        {
            
            
            
            //WriteAt(Block.Texture, x * 2, y);
            
        }
        public static void Fill_block(int x, int y, int[,] grid, int[,]Background, Block Block)
        {

            if (x > 0 || y > 0)
            {
                grid[y, x] = Block.id;
                Background[y, x] = Block.id;
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
        public static void Caves(int x, int y, int[,] grid)
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
                            Caves(x - size / 2 + i, y - size / 2 + j, grid);
                        }
                        continue;
                    }
                    grid[j + y, i + x] = 0;
                }
            }
        }

        public static void Caves(int x, int y, int[,] grid, Block solid, bool replace)
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
                            Caves(x - size / 2 + i, y - size / 2 + j, grid);
                        }
                        continue;
                    }
                    grid[j + y, i + x] = solid.id;
                }
            }

        }

        public static void Break_block(int x, int y, int[,] grid, List<Block> List, Player player)
        {

            var block = Minecraft.Get_ByID(grid[y, x], List);
            if (player.Breaking_stage > 0)
            {

                
                ;
                
            }
            else
            {
                
                player.Breaking_stage = 10;
                if (block == null || block.id == 0)
                {

                }
                else if (player.Inventoy.Contains(block))
                {
                    player.Inventoy.Find(x => x.id == block.id).quantity += 1;
                }
                else
                {
                    player.Inventoy.Add(block);
                }
                grid[y, x] = 0;
            }

        }
        public static void Fill_Index_Cord2(int x1, int y1, int x2, int y2, int[,] grid, Block Block, int randomiser)
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
        public static Block GetBlock(string name, List<Block> Block_list)
        {
            return Block_list.Find(x => x.name == name);
        }
        public static Block Get_ByID(int id, List<Block> Block_list)
        {
            return Block_list.Find(x => x.id == id);
        }
    }
}
