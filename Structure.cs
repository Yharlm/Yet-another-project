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
        public Minecraft Minecraft;
        public void structure(int Local_x, int Local_y)
        {
            
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

            int x = Struct.GetLength(1);
            int y = Struct.GetLength(0);



            Local_y -= y;
            for (int i = Local_y; i < Local_y + y; i++)
            {
                for (int j = Local_x; j < Local_x + x; j++)
                {
                    if (Struct[i - Local_y, j - Local_x] == 0)
                    {
                        continue;
                    }
                    int ID = Struct[i - Local_y, j - Local_x];


                    Block block = Minecraft.Block_list.Find(x => x.id == ID);
                    //Minecraft.Fill_block(j, i, grid, block);
                    Minecraft.grid[i,j] = ID;


                }
            }


        }
        public void structure(int Local_x)
        {
            
            

            int x = Struct.GetLength(1);
            int y = Struct.GetLength(0);

            int Local_y = 0;
            while (Minecraft.grid[Local_y, Local_x] == 0)
            {
                Local_y++;
            }
            Local_y -= y;
            for (int i = Local_y; i < Local_y + y; i++)
            {
                for (int j = Local_x; j < Local_x + x; j++)
                {
                    if (Struct[i - Local_y, j - Local_x] == 0)
                    {
                        continue;
                    }
                    int ID = Struct[i - Local_y, j - Local_x];


                    Block block = Minecraft.Block_list.Find(x => x.id == Struct[i - Local_y, j - Local_x]);
                    if(i < Minecraft.grid.GetLength(0) && j < Minecraft.grid.GetLength(1))
                    {
                        Minecraft.grid[i,j] = ID;
                    }
                    



                }
            }


        }
    }
}
