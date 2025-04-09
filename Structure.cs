using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Project3
{
    internal class Structure
    {
        public int[,] Struct;
        public void structure(object struc, int Local_x, int Local_y, int[,] grid, List<Block> list)
        {
            Structure structure = (Structure)struc;
            //Block_ids block = (Block_ids)Block;
            //Solid tile = (Solid)Block;
            //int[,] str =
            //{
            //    {0,0,1,0,0 },
            //    {0,0,1,0,0 },
            //    {0,1,1,0,0 },
            //    {0,0,1,0,0 },
            //    {0,0,1,0,0 }
            //};

            int x = structure.Struct.GetLength(1);
            int y = structure.Struct.GetLength(0);



            Local_y -= y;
            for (int i = Local_y; i < Local_y + y; i++)
            {
                for (int j = Local_x; j < Local_x + x; j++)
                {
                    if (structure.Struct[i - Local_y, j - Local_x] == 0)
                    {
                        continue;
                    }
                    int ID = structure.Struct[i - Local_y, j - Local_x];


                    Block block = list.Find(x => x.id == ID);
                    //Minecraft.Fill_block(j, i, grid, block);
                    grid[i,j] = ID;


                }
            }


        }
        public void structure(object struc, int Local_x, int[,] grid, List<Block> list)
        {
            Structure structure = (Structure)struc;
            //Block_ids block = (Block_ids)Block;
            //Solid tile = (Solid)Block;
            //int[,] str =
            //{
            //    {0,0,1,0,0 },
            //    {0,0,1,0,0 },
            //    {0,1,1,0,0 },
            //    {0,0,1,0,0 },
            //    {0,0,1,0,0 }
            //};

            int x = structure.Struct.GetLength(1);
            int y = structure.Struct.GetLength(0);

            int Local_y = 0;
            while (grid[Local_y, Local_x] == 0)
            {
                Local_y++;
            }
            Local_y -= y;
            for (int i = Local_y; i < Local_y + y; i++)
            {
                for (int j = Local_x; j < Local_x + x; j++)
                {
                    if (structure.Struct[i - Local_y, j - Local_x] == 0)
                    {
                        continue;
                    }
                    int ID = structure.Struct[i - Local_y, j - Local_x];


                    Block block = list.Find(x => x.id == structure.Struct[i - Local_y, j - Local_x]);
                    if(i < grid.GetLength(0) && j < grid.GetLength(1))
                    {
                        grid[i,j] = ID;
                    }
                    



                }
            }


        }
    }
}
