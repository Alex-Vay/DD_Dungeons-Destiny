using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Quickie011;
using System;
using System.Linq;

namespace DD_Dungeons_Destiny;
public enum Dun
{
    Sk,
    Sl,
    G,
    Th,
    Dem
}

internal class GameMenu : State
{
    public static Texture2D MainBackground { get; set; }
    private List<Component> components = new();
    private List<Gem> _gems = new();
    private List<Socket> _sockets = new();
    private List<Sprite> _sprites = new();
    private JustText[] text = new JustText[3] { null, null, null };

    private int level = 1;
    private int Score = 0;
    private int Walk = 1;
    private readonly List<Socket> _game = new();

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
        Gener();
    }

    public void Gener ()
    {
        var buttonTexture = content.Load<Texture2D>("Images\\Button");
        var buttonFont = content.Load<SpriteFont>("Fonts\\SplashFont");
        //for (int i = 0; i < 3; i++)
        //{
        //    var start = new Button(buttonTexture, buttonFont)
        //    {
        //        //Position = new Vector2(960f/ graphicsDevice.DisplayMode.Width * 600 - buttonTexture.Width/2, 360f / graphicsDevice.DisplayMode.Height * 300 - buttonTexture.Height/2),
        //        Position = new Vector2(400 + i*600, 900),
        //        Text = "Fight"
        //    };
        //    start.Click += (sender, e) => Fight(i);
        //    components.Add(start);
        //}

        var start = new Button(buttonTexture, buttonFont)
        {
            //Position = new Vector2(960f/ graphicsDevice.DisplayMode.Width * 600 - buttonTexture.Width/2, 360f / graphicsDevice.DisplayMode.Height * 300 - buttonTexture.Height/2),
            Position = new Vector2(400, 900),
            Text = "Fight"
        };
        start.Click += (sender, e) => Fight(0);
        components.Add(start);
        var start2 = new Button(buttonTexture, buttonFont)
        {
            //Position = new Vector2(960f/ graphicsDevice.DisplayMode.Width * 600 - buttonTexture.Width/2, 360f / graphicsDevice.DisplayMode.Height * 300 - buttonTexture.Height/2),
            Position = new Vector2(400 + 600, 900),
            Text = "Fight"
        };
        start2.Click += (sender, e) => Fight(1);
        components.Add(start2);
        var start3 = new Button(buttonTexture, buttonFont)
        {
            //Position = new Vector2(960f/ graphicsDevice.DisplayMode.Width * 600 - buttonTexture.Width/2, 360f / graphicsDevice.DisplayMode.Height * 300 - buttonTexture.Height/2),
            Position = new Vector2(400 + 2 * 600, 900),
            Text = "Fight"
        };
        start3.Click += (sender, e) => Fight(2);
        components.Add(start3);

        var gemTexture = Globals.Content.Load<Texture2D>("gem");
        //var socketTexture = Globals.Content.Load<Texture2D>("socket");
        var socketTexture = Globals.Content.Load<Texture2D>("Images\\Door2");

        //for (int i = 0; i < 7; i++)
        //{
        //    _gems.Add(new(gemTexture, new(600 + (i * 75), 800), (WAR)new Random().Next(3)));
        //    //_sockets.Add(new(socketTexture, new(600 + (i * 75), 900)));
        //}
        _game.Clear();
        _game.Add(new(socketTexture, new(500, 700)));
        _game.Add(new(socketTexture, new(1400, 1000)));
        for (int i = 0; i < 3; i++)
            _sockets.Add(new(socketTexture, new(400 + (i * 600), 500)));
        
        for (int i = 0; i < 7; i++)
        {
            _gems.Add(new(gemTexture, new(600 + (i * 75), 800), (WAR)new Random().Next(3)));
            DragDropManager.AddDrag(_game[0], _gems[i]);
        }
        MonGen(level);
    }

    #region GameMech
    public void Fight(int i)
    {
        _sockets[i].MonsterC = z[i];
        if (_sockets[i].MonsterC <= _sockets[i].WAR.Count)
            text[i] = (new (content.Load<SpriteFont>("Fonts\\SplashFont"), "Win", new Vector2(400 + 600 * (i), 50), Color.Gold));
        else
            text[i] = (new (content.Load<SpriteFont>("Fonts\\SplashFont"), "Lose", new Vector2(400 + 600 * (i), 50), Color.Gold));
    }

    int[] z = new int[5];

    //Рефлексия
    public void MonGen (int x)
    {
        var cof = 5;
        //var z = new int[cof];
        for (int i = 0; i < x; i++)
        {
            z[new Random().Next(5)]++; 
        }
        for (int i = 0; i < 3; i++)
        {
            var gemTexture = Globals.Content.Load<Texture2D>("gem");
            for (int j = 0; j < z[i]; j++)
                _sprites.Add(new(gemTexture, _sockets[i].Position - _sockets[i].origin + new Vector2(j * 50, 0)));
        }

        //var c = z.Select(p => p.GetValue(new object())).ToArray();
        
    }

    public void LevelEnd()
    {
        if (text.All(x => x != null && x.Text == "Win"))
        {
            _sprites.Clear();
            level++;
            text = new JustText[3];
            for (int i = 0; i < 3; i++)
            {
                foreach (var z in _sockets[i].WAR)
                {
                    _gems.Remove((Gem)z);
                }
                _sockets[i].WAR.Clear();
            }
            z = new int[5];
            MonGen(level);
        }
    }

    public void GameEnd()
    {
        if (_gems.Count < 1)
        {
            Score += level - 1;
            level = 1;
            Walk++;
            WalkEnd();
            Gener();
            MonGen(level);
        }
    }

    public void WalkEnd()
    {
        if (Walk > 3)
            game.ChangeState(new MainMenu(game, graphicsDevice, content));
    }

    #endregion

    public void Quit(object sender, EventArgs e) => game.Exit();

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

        foreach (var item in _gems)
        {
            item.Draw();
        }
        foreach (var item in _sprites)
            item.Draw(Color.Gray);
        for (int i = 0; i < z.Length; i++)
            spriteBatch.DrawString(content.Load<SpriteFont>("Fonts\\SplashFont"), z[i].ToString(), new Vector2(20, i*50), Color.Gold);
        spriteBatch.DrawString(content.Load<SpriteFont>("Fonts\\SplashFont"), Score.ToString(), new Vector2(20, 6 * 50), Color.Gold);
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
        
    }

    public override void Update(GameTime gameTime)
    {
        InputManager.Update();
        DragDropManager.Update();
        foreach (var comp in components)
            comp.Update(gameTime);
        LevelEnd();
        GameEnd();
        WalkEnd();
        //foreach (var t in text)
        //    if (t != null)
        //        t.Update(gameTime);
        //foreach (var component in components)
        //    component.Update(gameTime);
    }
}
