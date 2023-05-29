using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Quickie011;
using System;
using System.Linq;

namespace DD_Dungeons_Destiny;

internal class GameMenu : State
{
    public static Texture2D MainBackground { get; set; }
    private List<Component> components = new();
    public static List<Gem> _gems = new();
    private List<Socket> _sockets = new();
    private List<Sprite> _sprites = new();
    private JustText[] text = new JustText[3] { null, null, null };
    public List<Sprite> _sprites2 = new();

    private List<Socket> _game = new();

    private int level = 1;
    private int Score = 0;
    private int Walk = 1;
    public static Table table = new Table();
    public GameMenu(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
    {
        ////var buttonTexture = content.Load<Texture2D>("Images\\Button");
        ////var buttonFont = content.Load<SpriteFont>("Fonts\\SplashFont");

        ////var start = new Button(buttonTexture, buttonFont)
        ////{
        ////    //Position = new Vector2(960f/ graphicsDevice.DisplayMode.Width * 600 - buttonTexture.Width/2, 360f / graphicsDevice.DisplayMode.Height * 300 - buttonTexture.Height/2),
        ////    Position = new Vector2(960, 360),
        ////    Text = "Start"
        ////};
        ////var set = new Button(buttonTexture, buttonFont)
        ////{
        ////    Position = new Vector2(960, 500),
        ////    Text = "Setting"
        ////};
        ////var quit = new Button(buttonTexture, buttonFont)
        ////{
        ////    Position = new Vector2(960, 640),
        ////    Text = "Quit"
        ////};
        ////quit.Click += Quit;
        ////components = new List<Component>() { start, set, quit };
        //var buttonTexture = content.Load<Texture2D>("Images\\Button");
        //var buttonFont = content.Load<SpriteFont>("Fonts\\SplashFont");
        ////for (int i = 0; i < 3; i++)
        ////{
        ////    var start = new Button(buttonTexture, buttonFont)
        ////    {
        ////        //Position = new Vector2(960f/ graphicsDevice.DisplayMode.Width * 600 - buttonTexture.Width/2, 360f / graphicsDevice.DisplayMode.Height * 300 - buttonTexture.Height/2),
        ////        Position = new Vector2(400 + i*600, 900),
        ////        Text = "Fight"
        ////    };
        ////    start.Click += (sender, e) => Fight(i);
        ////    components.Add(start);
        ////}

        //var start = new Button(buttonTexture, buttonFont)
        //{
        //    //Position = new Vector2(960f/ graphicsDevice.DisplayMode.Width * 600 - buttonTexture.Width/2, 360f / graphicsDevice.DisplayMode.Height * 300 - buttonTexture.Height/2),
        //    Position = new Vector2(400, 900),
        //    Text = "Fight"
        //};
        //start.Click += (sender, e) => Fight(0);
        //components.Add(start);
        //var start2 = new Button(buttonTexture, buttonFont)
        //{
        //    //Position = new Vector2(960f/ graphicsDevice.DisplayMode.Width * 600 - buttonTexture.Width/2, 360f / graphicsDevice.DisplayMode.Height * 300 - buttonTexture.Height/2),
        //    Position = new Vector2(400 + 600, 900),
        //    Text = "Fight"
        //};
        //start2.Click += (sender, e) => Fight(1);
        //components.Add(start2);
        //var start3 = new Button(buttonTexture, buttonFont)
        //{
        //    //Position = new Vector2(960f/ graphicsDevice.DisplayMode.Width * 600 - buttonTexture.Width/2, 360f / graphicsDevice.DisplayMode.Height * 300 - buttonTexture.Height/2),
        //    Position = new Vector2(400 + 2 * 600, 900),
        //    Text = "Fight"
        //};
        //start3.Click += (sender, e) => Fight(2);
        //components.Add(start3);

        //var gemTexture = Globals.Content.Load<Texture2D>("gem");
        ////var socketTexture = Globals.Content.Load<Texture2D>("socket");
        //var socketTexture = Globals.Content.Load<Texture2D>("Area2");

        //for (int i = 0; i < 7; i++)
        //{
        //    _gems.Add(new(gemTexture, new(600 + (i * 75), 800), (WAR)new Random().Next(3)));
        //    //_sockets.Add(new(socketTexture, new(600 + (i * 75), 900)));
        //}
        //for (int i = 0; i < 3; i++)
        //    _sockets.Add(new(socketTexture, new(400 + (i * 600), 500)));
        //MonGen(level);
        //var socketTexture = Globals.Content.Load<Texture2D>("socket");
        
        table.Gener();
        _gems = table._gems;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(content.Load<Texture2D>("Images\\Dungeon"),
            new Rectangle(0, 0, graphicsDevice.DisplayMode.Width, graphicsDevice.DisplayMode.Height-200), Color.White);
       
        foreach (var item in _sockets)
        {
            item.Draw();
        }
        foreach (var z in _game)
            z.Draw();
        
        foreach (var item in _sprites)
            item.Draw(Color.Gray);
        foreach (var item in _sprites2)
            item.Draw(Color.Gray);
        //for (int i = 0; i < z.Length; i++)
        var xxx = 0;
        foreach (var z in table.monsters)
            spriteBatch.DrawString(content.Load<SpriteFont>("Fonts\\SplashFont"), z.Value.ToString(), new Vector2(20, xxx++*50), Color.Gold);
        spriteBatch.DrawString(content.Load<SpriteFont>("Fonts\\SplashFont"), table.Score.ToString(), new Vector2(20, 6 * 50), Color.Gold);
        foreach (var item in _gems)
            item.Draw();
        foreach (var comp in components)
            comp.Draw(gameTime, spriteBatch);
        foreach (var t in text)
            if (t != null)
                t.Draw();
        
        //Vector2.Zero, Color.White);
        //foreach (var component in components)
        //    component.Draw(gameTime, spriteBatch);

        //graphicsDevice.Clear(Color.White);

        spriteBatch.End();
    }

    public override void PostUpdate(GameTime gameTime)
    {
        text = table.text;
    }

    public override void Update(GameTime gameTime)
    {
        components = table.components;
        _sockets = table._sockets;
        _sprites = table._sprites;
        text = table.text;
        _game = table._game;
        _sprites2 = table._sprites2;
        DragDropManager.Update2();
        InputManager.Update();
        DragDropManager.Update();
        //_gems = table._gems;
        foreach (var comp in components)
            comp.Update(gameTime);
        
        table.LevelEnd();
        table.GameEnd();
        table.WalkEnd();
        //foreach (var t in text)
        //    if (t != null)
        //        t.Update(gameTime);
        //foreach (var component in components)
        //    component.Update(gameTime);
    }
}
