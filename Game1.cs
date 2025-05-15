using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using Color = Microsoft.Xna.Framework.Color;
using Vector2 = System.Numerics.Vector2;


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
    public List<Button> Button_list = new List<Button>();
    //public crafting_list
    public Button inventory;










    public Game1()
    {

        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = 1540;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.ApplyChanges();
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        PlayerWalkRight = new AnimatedTexture(Vector2.Zero, 0, 1, 0);
        PlayerWalkLeft = new AnimatedTexture(Vector2.Zero, 0, 1, 0);



        Button Spawn = new Button()
        {
            text = "Press me",
            font = Content.Load<SpriteFont>("text1"),

        };





    }

    void Generate_terrain(Minecraft Minecraft)
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

            Minecraft.Fill_block(j, Height, Minecraft.GetBlock("Grass_block"));

            int count = 1;
            while (count < dirt_Height)
            {
                Minecraft.Fill_block(j, Height + count, Minecraft.GetBlock("Dirt"));
                count++;
            }

            while (count < stone_Height)
            {
                Minecraft.Fill_block(j, Height + count, Minecraft.GetBlock("Stone"));
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
                Minecraft.Fill_Index_Cord2(j, Height + coalN - vein, j + vein, Height + coalN, Minecraft.GetBlock("Coal_ore"), 5);
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

                    Minecraft.Caves(j, y, Minecraft.GetBlock("Dirt"), false);
                    Minecraft.Caves(j, y, Minecraft.GetBlock("Iron_ore"), false);
                    Minecraft.Caves(j, y);


                }
                count--;
            }


        }


        ConsoleColor Default = ConsoleColor.Cyan;


        Structure tree = new Structure();
        tree.Minecraft = Minecraft;
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

                tree.structure(i);
                i += 5;
            }


        }

    }

    public List<Block> Create_Blocks(List<Block> Blocks)
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
        Template.stength = 45;
        Template.Texture = Content.Load<Texture2D>("stone");
        Blocks.Add(Template);

        Template = new Block();
        Template.id = 4;
        Template.name = "Coal_ore";
        Template.stength = 45;
        Template.Texture = Content.Load<Texture2D>("coal_ore");
        Blocks.Add(Template);

        Template = new Block();
        Template.id = 5;
        Template.name = "Iron_ore";
        Template.stength = 45;
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
        Template.stength = 25;
        Template.Texture = Content.Load<Texture2D>("oak_log");
        Blocks.Add(Template);


        return Blocks;

    }
    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        base.Initialize();
        Minecraft minecraft = new Minecraft();

        inventory = new Button();
        inventory.scale = new Vector2(3,3);
            inventory.text = "";
            inventory.position = new Vector2(20, 500);

        


    }

    protected override void LoadContent()
    {

        PlayerWalkLeft.Load(Content, "Leftt", 13, 10);
        PlayerWalkRight.Load(Content, "walking player-Sheet", 13, 10);
        player.player_texture = PlayerWalkRight;
        player.player_walkR = PlayerWalkRight;
        player.player_walkL = PlayerWalkLeft;
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        minecraft.grid = grid;
        minecraft.Background = Background_grid;
        minecraft.Block_list = Create_Blocks(Block_list);
        minecraft.Entities = Entity.Load_Mobs();
        Generate_terrain(minecraft);


        Button Crafting = new Button();
        Crafting.text = "";
        Crafting.position = new Vector2(40, 350);
        Crafting.scale = new Vector2(3, 3);
        Button_list.Add(Crafting);

        



        // TODO: use this.Content to load your game content here
    }
    float index_value = 0;
    int val_index = 0;
    float spawn_delay = 0f;
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
        Read_input(player, grid, minecraft);
        if (Keyboard.GetState().IsKeyDown(Keys.P))
        {
            _graphics.ToggleFullScreen();
        }

        //animation stff
        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
        PlayerWalkRight.UpdateFrame(elapsed);
        PlayerWalkLeft.UpdateFrame(elapsed);

        foreach (Entity mob in minecraft.Existing_entities)
        {
            mob.collision.Left = false;
            mob.collision.Right = false;
            mob.collision.Top = false;
            mob.collision.Bootom = false;

            if (!Entity.CheckVertice(new System.Numerics.Vector2(0, 0.5f), mob, player, block_gap, relative_block_size, grid))
            {
                mob.collision.Left = true;
            }
            if (!Entity.CheckVertice(new System.Numerics.Vector2(0.5f, 0), mob, player, block_gap, relative_block_size, grid))
            {
                mob.collision.Top = true;
            }
            if (!Entity.CheckVertice(new System.Numerics.Vector2(1, 0.5f), mob, player, block_gap, relative_block_size, grid))
            {
                mob.collision.Right = true;
            }
            if (!Entity.CheckVertice(new System.Numerics.Vector2(0.5f, 1f), mob, player, block_gap, relative_block_size, grid))
            {
                mob.collision.Bootom = true;

            }

            //Entity.TestCheckVertice(new System.Numerics.Vector2(0, 0.5f), mob, player, block_gap, relative_block_size, grid);
            if (mob != null)
            {
                mob.Apply_Velocity();
                //mob.Position += mob.Velocity.current_velocity;
                //mob.Position.Y += 0.1f;
                mob.Velocity.Add_velocity(new Vector2(0, 0.3f));

            }
            mob.Walk2Player(player);




        }




        if (Keyboard.GetState().IsKeyDown(Keys.D1))
        {
            index_value -= 0.1f;

        }



        if (Keyboard.GetState().IsKeyDown(Keys.D2))
        {

            index_value += 0.1f;
        }

        player.index = (int)index_value;
        if (player.index <= 1)
        {
            player.index = 9;
        }
        if (player.index >= 9)
        {
            player.index = 1;
        }
        base.Update(gameTime);
    }

    public static bool CheckVertice(Vector2 Offset, Player plr, float block_gap, float relative_block_size, int[,] grid)
    {
        Vector2 mousepos = (plr.position + Offset) * new Vector2(block_gap, block_gap);
        double x = Math.Ceiling((double)mousepos.X / block_gap + plr.camera.X * relative_block_size);
        double y = Math.Ceiling((double)mousepos.Y / block_gap + plr.camera.Y * relative_block_size);

        if (grid[(int)y - 1, (int)x - 1] == 0)
        { return true; }
        return false;
    }


    void Read_input(Player plr, int[,] grid, Minecraft minecraft)
    {
        float a = 0.1f;
        float relative_block_size = plr.Zoom;
        float block_gap = 58f * relative_block_size / 3.65f;
        float speed = 0.055f;
        Vector2 mousepos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        double x = Math.Ceiling((double)mousepos.X / block_gap + plr.camera.X * relative_block_size);
        double y = Math.Ceiling((double)mousepos.Y / block_gap + plr.camera.Y * relative_block_size);
        System.Numerics.Vector2 Cursor_toWorld = new System.Numerics.Vector2((float)(double)mousepos.X / block_gap + plr.camera.X * relative_block_size, (float)(double)mousepos.Y / block_gap + plr.camera.Y * relative_block_size);
        foreach (Button button in Button_list)
        {
            if (mousepos.X > button.position.X && mousepos.X < button.position.X + button.scale.X * 10 && mousepos.Y > button.position.Y && mousepos.Y < button.position.Y + button.scale.Y * 10)
            {
                button.color = Color.Gold;
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {


                }
                continue;
            }
            else
            {
                button.color = Color.White;
            }
        }


        if (plr.Jumped)
        {
            if (CheckVertice(new Vector2(0.5f, 1), plr, block_gap, relative_block_size, grid) == false) // returns true if theres nothing under player
            {

                plr.jump_val = 1;
                plr.Jumped = false;
                plr.has_jumped = false;
                plr.is_falling_fast = false;
            }
            if (CheckVertice(new Vector2(0.5f, -1.21f), plr, block_gap, relative_block_size, grid) == false)
            {

                plr.is_falling_fast = true;
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
        if (Keyboard.GetState().IsKeyDown(Keys.E))
        {

            minecraft.spawn_ent("Dirt", Cursor_toWorld);

        }
        if (Mouse.GetState().RightButton == ButtonState.Pressed)
        {


            minecraft.Place_block((int)x - 1, (int)y - 1, plr);

        }

        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
        {

            minecraft.Break_block((int)x - 1, (int)y - 1, plr);

        }
        else
        {
            plr.Breaking_stage = 10f;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.R))
        {

            cooldown -= 0.1f
                ; if (cooldown <= 0)
            {
                minecraft.spawn_ent("Dirt", player.position + player.camera * player.Zoom);
                minecraft.Existing_entities.Last().scale = 1f;
                minecraft.Existing_entities.Last().Velocity.Add_velocity(Vector2.Normalize(mousepos - new Vector2(777, 555)));
                cooldown = 3;
            }


        }



    }
    public float cooldown = 3;
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
            for (int j = (int)(player.camera.X * relative_block_size) + 2; j < (int)(player.camera.X * relative_block_size) + 100; j++)
            {
                if (j <= 0)
                {
                    j = 1;
                }
                if (minecraft.Get_ByID(Background_grid[i, j]) != null)
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
        _spriteBatch.DrawString(Content.Load<SpriteFont>("text1"), player.Breaking_stage.ToString(), new Vector2(0, 90), Color.Red);
        _spriteBatch.DrawString(Content.Load<SpriteFont>("text1"), player.index.ToString(), new Vector2(0, 120), Color.Red);
        _spriteBatch.DrawString(Content.Load<SpriteFont>("text1"), gameTime.ElapsedGameTime.TotalSeconds.ToString(), new Vector2(0, 150), Color.Red);

        foreach (Entity mob in minecraft.Existing_entities)
        {
            _spriteBatch.Draw(Content.Load<Texture2D>("dirt"), new Vector2(mob.Position.X * block_gap + -player.camera.X * block_gap * relative_block_size, mob.Position.Y * block_gap + -player.camera.Y * block_gap * relative_block_size), null, Color.LawnGreen, 0f, Vector2.Zero, relative_block_size, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(Content.Load<SpriteFont>("text1"), mob.collision.Bootom.ToString(), new Vector2(mob.Position.X * block_gap + -player.camera.X * block_gap * relative_block_size, mob.Position.Y * block_gap + -player.camera.Y * block_gap * relative_block_size), Color.Wheat);

        }
        if (minecraft.Existing_entities.Count > 0)
        {
            _spriteBatch.DrawString(Content.Load<SpriteFont>("text1"), minecraft.Existing_entities.Last().collision.Bootom.ToString(), new Vector2(0, 120), Color.Red);
        }
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
        Vector2 NewPos = new Vector2(player.position.X, player.position.Y) + new Vector2(-0.25f, -1.5f);
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        player.player_texture.Scale = 1.6f;
        // Replacing the normal SpriteBatch.Draw call to use the version from the "AnimatedTexture" class instead
        player.player_texture.DrawFrame(_spriteBatch, NewPos * new Vector2(block_gap, block_gap));

        int count = 0;

        foreach (var b in player.Inventoy)
        {
            _spriteBatch.Draw(b.Texture, new Vector2(30 * count, 40), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            _spriteBatch.DrawString(Content.Load<SpriteFont>("text1"), b.quantity.ToString(), new Vector2(30 * count, 60), Color.Wheat);




            count++;

        }
        _spriteBatch.End();
        foreach (var button in Button_list)
        {
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(Content.Load<Texture2D>("crafting_table_front"), button.position, null, button.color, 0f, Vector2.One, button.scale, SpriteEffects.None, 1f);
            _spriteBatch.DrawString(Content.Load<SpriteFont>("text1"), button.text, button.position, Microsoft.Xna.Framework.Color.White, 0f, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, 1f);
            _spriteBatch.End();
        }

        for(int i = 0;i < 9;i++)
        {
            Texture2D texture = Content.Load<Texture2D>("Air");
            if (player.Inventoy.Count > 0)
            {

            }
            
             
            var button = inventory;
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(texture, button.position + new Vector2(i * 60, 0), null, button.color, 0f, Vector2.One, button.scale, SpriteEffects.None, 1f);
            _spriteBatch.DrawString(Content.Load<SpriteFont>("text1"), button.text, button.position + new Vector2(i*60,0), Microsoft.Xna.Framework.Color.White, 0f, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, 1f);
            _spriteBatch.End();
            
        }


        base.Draw(gameTime);
    }
}
