using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Project3;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    // game's variables go here i guess, since this is a class

    public List<Block> Block_list = new List<Block>();
    public int[,] grid = new int[500, 1000];
    public Player player = new Player();
    public Vector2 mousepos;













    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }
    static void Generate_terrain(int[,] grid, List<Block> list)
    {
        
        Random random = new Random();
        int Width = 1000;
        ; int Height = 45
        ;
        int dirt_Height = 5;
        int stone_Height = 302;
        int min_level = 60;
        int min = 1;
        int max = 3;
        int c = 0;
        int sea_level = 70;


        for (int j = 2; j < Width; j++)
        {
            c = random.Next(min, max + 1);
            if (c == min)
            { Height++; }
            else if (c == max)
            { Height--; }

            Minecraft.Fill_block(j, Height, grid, Minecraft.GetBlock("Grass_block", list));

            int count = 1;
            while (count < dirt_Height)
            {
                Minecraft.Fill_block(j, Height + count, grid, Minecraft.GetBlock("Dirt", list));
                count++;
            }

            while (count < stone_Height)
            {
                Minecraft.Fill_block(j, Height + count, grid, Minecraft.GetBlock("Stone", list));
                count++;
            }
            //Caves(j, random.Next(sea_level, 100), grid);
        }


        for (int j = 0; j < Width - 12;)
        {
            int coalN = random.Next(1, 40);
            int vein = random.Next(1, 6);

            if (random.Next(1, 30) < 4)
            {
                Minecraft.Fill_Index_Cord2(j, Height + coalN - vein, j + vein, Height + coalN, grid, Minecraft.GetBlock("Coal_ore", list), 5);
            }
            j++;
        }
        //for (int j = 0; j < Width - 12;)
        //{
        //    int ironN = random.Next(1, 40);
        //    int vein = random.Next(1, 6);
        //    if (random.Next(1, 30) < 4)
        //    {
        //        Minecraft.Fill_Index_Cord2(j, Height + ironN - vein, j + vein, Height + ironN, grid, Minecraft.GetBlock("Iron_ore", list), 5);
        //    }
        //    j++;
        //}

        //for (int j = 0; j < Width - 30; j++)
        //{
        //    for (int i = sea_level - 10; i < sea_level + 50; i++)
        //    {
        //        if (grid[i, j] == 0)
        //        {
        //            Minecraft.Fill_block(j, i, grid, Minecraft.GetBlock("water", list));
        //        }
        //    }
        //}

        //for (int j = 12; j < Width - 12; j++)
        //{

        //    int count = random.Next(0, 11);
        //    while (count > 0)
        //    {
        //        int y = random.Next(30, 460);
        //        if (grid[y, j] != 0)
        //        {

        //            Minecraft.Caves(j, y, grid, Minecraft.GetBlock("Dirt", list), false);
        //            Minecraft.Caves(j, y, grid, Minecraft.GetBlock("Iron_ore", list), false);
        //            Minecraft.Caves(j, y, grid);
        //        }
        //        count--;
        //    }


        //}


    }

    public void Create_Blocks(List<Block> Blocks)
    {
        Block Template = new Block();
        Template = new Block();
        Template.id = 0;
        Template.name = "Air";
        Template.Texture = Content.Load<Texture2D>("Air");
        Blocks.Add(Template);

        Template = new Block();
        Template.id = 1;
        Template.name = "Grass_block";
        Template.Texture = Content.Load<Texture2D>("grass_side_carried");
        Blocks.Add(Template);

        Template = new Block();
        Template.id = 2;
        Template.name = "Dirt";
        Template.Texture = Content.Load<Texture2D>("dirt");
        Blocks.Add(Template);

        Template = new Block();
        Template.id = 3;
        Template.name = "Stone";
        Template.Texture = Content.Load<Texture2D>("stone");
        Blocks.Add(Template);

        Template = new Block();
        Template.id = 4;
        Template.name = "Coal_ore";
        Template.Texture = Content.Load<Texture2D>("ladder");
        Blocks.Add(Template);
    }
    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Create_Blocks(Block_list);
        Generate_terrain(grid, Block_list);
        //for (int i = 0; i < 500; i++)
        //{
        //    for (int j = 0; j < 1000; j++)
        //    {
        //        grid[i, j] = 1;
        //    }
        //}
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        mousepos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        // TODO: Add your update logic here
        //if (Keyboard.GetState().IsKeyDown(Keys.S))
        //{
        //    camera.Y += 10f;
        //}
        Read_input(player);
        base.Update(gameTime);
    }

    static void Read_input(Player plr)
    {
        float speed = 0.1f;
        if(Keyboard.GetState().IsKeyDown(Keys.OemMinus))
        {
            plr.Zoom += 0.01f;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
        {
            plr.Zoom -= 0.01f;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.W))
        {
            plr.camera.Y += speed * 81;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.S))
        {
            plr.camera.Y -= speed * 81;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.A))
        {
            plr.camera.X -= speed;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D))
        {
            plr.camera.X += speed;
        }
        if(Mouse.GetState().LeftButton == ButtonState.Pressed)
        {
            
        }
        
    }
    protected override void Draw(GameTime gameTime)
    {
        float relative_block_size = player.Zoom;
        float block_gap = 58f * relative_block_size / 3.65f;
       

        GraphicsDevice.Clear(Color.CornflowerBlue);
        for (int i = 0; i < 100; i++)
        {
            for (int j = (int)(player.camera.X * relative_block_size)+2; j < (int)(player.camera.X * relative_block_size) + 27; j++)
            {
                if(j <= 0)
                {
                    j = 5;
                }
                if (Block_list.Find(x => x.id == grid[i, j]) == null)
                {
                    _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                    _spriteBatch.Draw(Block_list[1].Texture, new Vector2(j * block_gap + -player.camera.X, i * block_gap + -player.camera.Y), null, Color.White, 0f, Vector2.Zero, relative_block_size, SpriteEffects.None, 0f);
                    _spriteBatch.End();
                    continue;
                }

                _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                _spriteBatch.Draw(Block_list.Find(x => x.id == grid[i, j]).Texture, new Vector2(j * block_gap + -player.camera.X * block_gap * relative_block_size, i * block_gap + player.camera.Y), null, Color.White, 0f, Vector2.Zero, relative_block_size, SpriteEffects.None, 0f);
                _spriteBatch.End();
            }
        }

        _spriteBatch.Begin();
        _spriteBatch.DrawString(Content.Load<SpriteFont>("text1"), "X: " + player.camera.X + " Y: " + player.camera.Y, new Vector2(0,0), Color.White);
        _spriteBatch.DrawString(Content.Load<SpriteFont>("text1"), "X:"+ mousepos.X + "Y:"+mousepos.Y, new Vector2(0, 20), Color.White);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
