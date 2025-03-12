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

            
            grid[y, x] = Block.id;
            //WriteAt(Block.Texture, x * 2, y);
            
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
                    grid[j + y, i + x] = 17;
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
