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
    static Minecraft minecraft = new Minecraft();

    public List<Block> Block_list = minecraft.Block_list;
    public int[,] grid = new int[500, 1000];
    public int[,] Background_grid = new int[500, 1000];
    public Player player = new Player();
    public Vector2 mousepos;
    public AnimatedTexture PlayerWalkRight;
    public AnimatedTexture PlayerWalkLeft;













    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        PlayerWalkRight = new AnimatedTexture(Vector2.Zero, 0, 1, 0);
        PlayerWalkLeft = new AnimatedTexture(Vector2.Zero, 0, 1, 0);



    }

    static void Generate_terrain(int[,] grid, int[,] backrond, List<Block> list)
    {
        //for (int i = 0; i < 500; i++)
        //{
        //    for (int j = 0; j < 1000; j++)
        //    {

        //        backrond[i, j] = Minecraft.GetBlock("Stone", list).id;
        //    }
        //}
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

            Minecraft.Fill_block(j, Height, grid, backrond, Minecraft.GetBlock("Grass_block", list));

            int count = 1;
            while (count < dirt_Height)
            {
                Minecraft.Fill_block(j, Height + count, grid, backrond, Minecraft.GetBlock("Dirt", list));
                count++;
            }

            while (count < stone_Height)
            {
                Minecraft.Fill_block(j, Height + count, grid, backrond, Minecraft.GetBlock("Stone", list));
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


        for (int j = 32; j < Width - 32; j++)
        {

            int count = random.Next(0, 11);
            while (count > 0)
            {
                int y = random.Next(30, 460);
                if (grid[y, j] != 0)
                {

                    Minecraft.Caves(j, y, grid, Minecraft.GetBlock("Dirt", list), false);
                    Minecraft.Caves(j, y, grid, Minecraft.GetBlock("Iron_ore", list), false);
                    Minecraft.Caves(j, y, grid);


                }
                count--;
            }


        }


        ConsoleColor Default = ConsoleColor.Cyan;


        Structure tree = new Structure();
        tree.Struct = new int[,]{
                { 0,6,6,6,0 },
                { 0,6,7,6,0 },
                { 6,6,7,6,6 },
                { 6,6,7,6,6 },
                { 0,0,7,0,0 },
                { 0,0,7,0,0 }
            };
        int tree_rate = 14;
        int Tree_r = 0;
        for (int i = 4; i < 700; i++)
        {
            Tree_r = random.Next(1, tree_rate);
            if (Tree_r >= tree_rate - 2)
            {

                tree.structure(tree, i, grid, list);
                i += 5;
            }


        }

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
        Template.Texture = Content.Load<Texture2D>("coal_ore");
        Blocks.Add(Template);

        Template = new Block();
        Template.id = 5;
        Template.name = "Iron_ore";
        Template.Texture = Content.Load<Texture2D>("iron_ore");
        Blocks.Add(Template);

        Template = new Block();
        Template.id = 6;
        Template.apaque = true;
        Template.name = "Leaves_oak";
        Template.Texture = Content.Load<Texture2D>("leaves_oak_opaque");
        Blocks.Add(Template);

        Template = new Block();
        Template.id = 7;
        Template.name = "Oak_log";

        Template.Texture = Content.Load<Texture2D>("oak_log");
        Blocks.Add(Template);




    }
    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        base.Initialize();
        Minecraft minecraft = new Minecraft();
    }

    protected override void LoadContent()
    {

        PlayerWalkLeft.Load(Content, "Leftt", 13, 10);
        PlayerWalkRight.Load(Content, "walking player-Sheet", 13, 10);
        player.player_texture = PlayerWalkRight;
        player.player_walkR = PlayerWalkRight;
        player.player_walkL = PlayerWalkLeft;
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Create_Blocks(Block_list);
        Generate_terrain(grid, Background_grid, Block_list);
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
        Read_input(player, grid, Block_list);


        //animation stff
        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
        PlayerWalkRight.UpdateFrame(elapsed);
        PlayerWalkLeft.UpdateFrame(elapsed);

        base.Update(gameTime);
    }

    static bool CheckVertice(Vector2 Offset, Player plr, float block_gap, float relative_block_size, int[,] grid)
    {
        Vector2 mousepos = (plr.position + Offset) * new Vector2(block_gap, block_gap);
        double x = Math.Ceiling((double)mousepos.X / block_gap + plr.camera.X * relative_block_size);
        double y = Math.Ceiling((double)mousepos.Y / block_gap + plr.camera.Y * relative_block_size);

        if (grid[(int)y - 1, (int)x - 1] == 0)
        { return true; }
        return false;
    }
    static void Read_input(Player plr, int[,] grid, List<Block> list)
    {
        float a = 0.1f;
        float relative_block_size = plr.Zoom;
        float block_gap = 58f * relative_block_size / 3.65f;
        float speed = 0.055f;
        Vector2 mousepos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        double x = Math.Ceiling((double)mousepos.X / block_gap + plr.camera.X * relative_block_size);
        double y = Math.Ceiling((double)mousepos.Y / block_gap + plr.camera.Y * relative_block_size);


        if (plr.Jumped)
        {
            if (CheckVertice(new Vector2(0.5f, 1), plr, block_gap, relative_block_size, grid) == false) // returns true if theres nothing under player
            {
                
                plr.jump_val = 1;
                plr.Jumped = false;
                plr.has_jumped = false;
                plr.is_falling_fast = false;
            }
            plr.jump();

        }
        plr.is_walking = false;

        if (Keyboard.GetState().IsKeyDown(Keys.OemMinus))
        {

            plr.Zoom += 0.01f;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.OemPlus))
        {
            plr.Zoom -= 0.01f;
        }


        

        if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Space))
        {

            if (!plr.has_jumped)
            {

                plr.has_jumped = true;
                plr.Jumped = true;


            }

        }

        if (CheckVertice(new Vector2(0.5f, 1), plr, block_gap, relative_block_size, grid))
        {
            plr.gravity += 0.001f; plr.camera.Y += plr.gravity;
        }
        else
        {
            plr.gravity = 0.1f; plr.JumpPower = 1;
        }




        if (Keyboard.GetState().IsKeyDown(Keys.S))
        {
            if (
                CheckVertice(new Vector2(0.5f, 1)
                , plr, block_gap, relative_block_size, grid)
                )
            { plr.camera.Y += speed; }

        }

        if (Keyboard.GetState().IsKeyDown(Keys.A))
        {
            plr.is_walking = true;
            plr.player_texture = plr.player_walkL;

            if (
                CheckVertice(new Vector2(0, 0.5f)
                , plr, block_gap, relative_block_size, grid)
                )
            { plr.camera.X -= speed; }

        }
        if (Keyboard.GetState().IsKeyDown(Keys.D))
        {


            plr.is_walking = true;
            plr.player_texture = plr.player_walkR;
            if (
                CheckVertice(new Vector2(1, 0.5f)
                , plr, block_gap, relative_block_size, grid))

            { plr.camera.X += speed; }

        }
        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
        {


            grid[(int)y - 1, (int)x - 1] = plr.id_copy;

        }

        if (Mouse.GetState().RightButton == ButtonState.Pressed)
        {



            grid[(int)y - 1, (int)x - 1] = 0;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.E))
        {



            plr.id_copy = grid[(int)y - 1, (int)x - 1];
        }
        if (Keyboard.GetState().IsKeyDown(Keys.F))
        {

            Structure tree = new Structure();
            tree.Struct = new int[,]{
                { 0,1,1,1,0 },
                { 0,6,1,1,0 },
                { 6,6,1,1,6 },
                { 6,6,1,6,6 },
                { 0,0,1,0,0 },
                { 0,0,1,0,0 }
            };
            tree.structure(tree, (int)x, grid, list);
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Q))
        {


            if (grid[(int)y - 1, (int)x - 1] != 0)
            { }
        }


    }
    public float relative_block_size;
    public float block_gap;
    protected override void Draw(GameTime gameTime)
    {
        relative_block_size = player.Zoom;
        block_gap = 58f * relative_block_size / 3.65f;
        Color Backround = Color.FromNonPremultiplied(170, 170, 170, 255);
        Vector2 WorldPos = new Vector2(0, 0);
        GraphicsDevice.Clear(Color.CornflowerBlue);
        for (int i = 0; i < 100; i++)
        {
            for (int j = (int)(player.camera.X * relative_block_size) + 2; j < (int)(player.camera.X * relative_block_size) + 47; j++)
            {
                if (j <= 0)
                {
                    j = 5;
                }
                if (Minecraft.Get_ByID(Background_grid[i, j], Block_list) != null)
                {
                    _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                    _spriteBatch.Draw(Block_list.Find(x => x.id == Background_grid[i, j]).Texture, new Vector2(j * block_gap + -player.camera.X * block_gap * relative_block_size, i * block_gap + -player.camera.Y * block_gap * relative_block_size), null, Backround, 0f, Vector2.Zero, relative_block_size, SpriteEffects.None, 0f);
                    _spriteBatch.End();

                }
                if (Block_list.Find(x => x.id == grid[i, j]) == null)
                {
                    _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                    _spriteBatch.Draw(Block_list[1].Texture, new Vector2(j * block_gap + -player.camera.X * block_gap * relative_block_size, i * block_gap + -player.camera.Y * block_gap * relative_block_size), null, Color.White, 0f, Vector2.Zero, relative_block_size, SpriteEffects.None, 0f);
                    _spriteBatch.End();
                    continue;
                }
                if (Block_list.Find(x => x.id == grid[i, j]).apaque == true)
                {
                    _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                    _spriteBatch.Draw(Block_list.Find(x => x.id == grid[i, j]).Texture, new Vector2(j * block_gap + -player.camera.X * block_gap * relative_block_size, i * block_gap + -player.camera.Y * block_gap * relative_block_size), null, Color.LawnGreen, 0f, Vector2.Zero, relative_block_size, SpriteEffects.None, 0f);
                    _spriteBatch.End();
                    continue;
                }

                _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                _spriteBatch.Draw(Block_list.Find(x => x.id == grid[i, j]).Texture, new Vector2(j * block_gap + -player.camera.X * block_gap * relative_block_size, i * block_gap + -player.camera.Y * block_gap * relative_block_size), null, Color.White, 0f, Vector2.Zero, relative_block_size, SpriteEffects.None, 0f);
                _spriteBatch.End();
            }
        }
        _spriteBatch.Begin();
        _spriteBatch.Draw(Block_list[3].Texture, player.position * new Vector2(block_gap, block_gap), null, Color.LawnGreen, 0f, Vector2.Zero, relative_block_size, SpriteEffects.None, 0f);
        _spriteBatch.End();
        Vector2 mousepos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        double x = Math.Ceiling((double)mousepos.X / block_gap + player.camera.X);
        double y = Math.Ceiling((double)mousepos.Y / block_gap + player.camera.Y);
        _spriteBatch.Begin();
        _spriteBatch.DrawString(Content.Load<SpriteFont>("text1"), "X: " + player.camera.X + " Y: " + player.camera.Y, new Vector2(0, 0), Color.White);
        _spriteBatch.DrawString(Content.Load<SpriteFont>("text1"), "X:" + x + "Y:" + y, new Vector2(0, 20), Backround);
        _spriteBatch.DrawString(Content.Load<SpriteFont>("text1"), player.id_copy.ToString(), new Vector2(0, 40), Backround);
        _spriteBatch.DrawString(Content.Load<SpriteFont>("text1"), Mouse.GetState().Position.ToString(), new Vector2(0, 40), Color.Red);
        _spriteBatch.DrawString(Content.Load<SpriteFont>("text1"), player.jump_val.ToString(), new Vector2(0, 60), Color.Red);

        _spriteBatch.End();

        PlayerWalkRight.Scale = relative_block_size;
        PlayerWalkLeft.Scale = relative_block_size;
        if (player.is_walking)
        {
            player.player_texture.Play();
        }
        else
        {
            player.player_texture.Pause();
            player.player_texture.frame = 3;

        }
        Vector2 NewPos = new Vector2(player.position.X, player.position.Y) + new Vector2(-0.5f, -2.8f);
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        // Replacing the normal SpriteBatch.Draw call to use the version from the "AnimatedTexture" class instead
        player.player_texture.DrawFrame(_spriteBatch, NewPos * new Vector2(block_gap, block_gap));

        _spriteBatch.End();


        base.Draw(gameTime);
    }
}
