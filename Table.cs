using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Quickie011;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace DD_Dungeons_Destiny;
public enum Dun
{
    Sk,
    Sl,
    G,
    Th,
    Dem,
    H
}

public enum WAR
{
    Tank,
    Mag,
    Warrior,
    Thief,
    Gurd,
    Admin,
}

public class Table
{
    public readonly int Sk;
    public readonly int Sl;
    public readonly int G;
    public readonly int Th;
    public readonly int Dem = 0;
    public readonly int H;
    public Dictionary<Dun, int> monsters = new Dictionary<Dun, int>() { {Dun.Sk, 0 }, { Dun.Sl, 0 }, { Dun.G, 0 }, { Dun.Th, 0 }, { Dun.Dem, 0 }, { Dun.H, 0 } };
    public int Phase;
    private int level = 8;
    public int Score = 0;
    private int Walk = 1;

    public List<Component> components = new();
    public List<Gem> _gems = new();
    public List<Socket> _sockets = new();
    public List<Sprite> _sprites = new();
    public JustText[] text = new JustText[3] { null, null, null };
    public readonly List<Socket> _game = new();
    public List<Sprite> _sprites2 = new();

    ContentManager content = Globals.Content;
    Game1 game = Globals.Game;
    GraphicsDevice graphicsDevice = Globals.Game.GraphicsDevice;

    public Table() { }

    public Table (int sk, int sl, int g, int th, int dem)
    {
        Sk = sk;
        Sl = sl;
        G = g;
        Th = th;
        Dem += dem;
        if (Dem >= 3)
        {
            Dem = 0;
            H++;
        }
    }

    public void Gener()
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
        var socketTexture2 = Globals.Content.Load<Texture2D>("11");

        //for (int i = 0; i < 7; i++)
        //{
        //    _gems.Add(new(gemTexture, new(600 + (i * 75), 800), (WAR)new Random().Next(3)));
        //    //_sockets.Add(new(socketTexture, new(600 + (i * 75), 900)));
        //}
        //_game.Clear();
        _game.Add(new(socketTexture2, new(425, 850)));
        _game.Add(new(socketTexture2, new(1325, 850)));
        for (int i = 0; i < 3; i++)
            _sockets.Add(new(socketTexture, new(400 + (i * 600), 500)));
        for (int i = 0; i < 7; i++)
        {
            var z = (WAR)new Random().Next(4);
            _gems.Add(new Gem(Globals.Content.Load<Texture2D>(z.ToString()), _game[0].Position - new Vector2(_game[0].Rectangle.Width / 2 -100, 0) + new Vector2(100, 0) * _game[0].WAR.Count, z));
            _game[0].WAR.Add(_gems.Last());
        }
        MonGen(level);
    }

    #region GameMech
    public void Fight(int i)
    {
        _sockets[i].MonsterC = monsters[(Dun)i];
        if (_sockets[i].MonsterC <= _sockets[i].WAR.Count || _sockets[i].WAR.Select(x => x.WAR).Contains((WAR)i))
            text[i] = (new(content.Load<SpriteFont>("Fonts\\SplashFont"), "Win", new Vector2(400 + 600 * (i), 50), Color.Gold));
        else
            text[i] = (new(content.Load<SpriteFont>("Fonts\\SplashFont"), "Lose", new Vector2(400 + 600 * (i), 50), Color.Gold));
    }

    //Рефлексия
    public void MonGen(int x)
    {
        var cof = 6;
        if (x > 8) x = 8;
        for (int i = 0; i < x; i++)
        {
            monsters[(Dun)(new Random().Next(cof))]++;
        }
        for (int i = 0; i < 3; i++)
        {
            var gemTexture = Globals.Content.Load<Texture2D>(((Dun)i).ToString());
            for (int j = 0; j < monsters[(Dun)i]; j++)
                _sprites.Add(new(gemTexture, _sockets[i].Position - _sockets[i].origin + new Vector2(100, 100) + new Vector2(j % 3 * 200, j / 3 * 150)));
        }

        //var c = z.Select(p => p.GetValue(new object())).ToArray();

    }

    public void LevelEnd()
    {
        if (InputManager.MouseClicked)
        {
            foreach (var item in _game[1].WAR)
            {
                if (item.Rectangle.Contains(InputManager.MousePosition))
                {
                    DragDropManager.Drag(item);
                    _game[1].WAR.Remove(item);
                    break;
                }
            }
        }

        if (text.All(x => x != null && x.Text == "Win"))
        {
            _sprites.Clear();
            level++;
            text = new JustText[3];
            for (int i = 0; i < 3; i++)
            {
                foreach (var z in _sockets[i].WAR)
                {
                    z.Position = _game[1].Position - new Vector2(_game[1].Rectangle.Width / 2 - 100, 0) + new Vector2(100, 0) * _game[1].WAR.Count;
                    DragDropManager.NoDrag(z);
                    _game[1].WAR.Add(z);
                    _game[0].WAR.Remove(z);
                }
                _sockets[i].WAR.Clear();
            }
            for (int i = 0; i < monsters.Count; i++)
            {
                if ((Dun)i == Dun.Dem) continue;
                monsters[(Dun)i] = 0;
            }
            var zero = _game[1].Position - new Vector2(_game[1].Rectangle.Width / 2 - 100, 0);
            for (int j = 0; j < _game[1].WAR.Count; j++)
            {
                _game[1].WAR[j].Position = zero + new Vector2(100, 0) * j;
            }
            var zero2 = _game[0].Position - new Vector2(_game[0].Rectangle.Width / 2 - 100, 0);
            for (int j = 0; j < _game[0].WAR.Count; j++)
            {
                _game[0].WAR[j].Position = zero2 + new Vector2(100, 0) * j;
            }
            foreach (var i in _game) { }
            MonGen(level);
        }
        GameEnd();
    }

    //public void LevelEnd()
    //{
    //    if (text.All(x => x != null && x.Text == "Win"))
    //    {
    //        _sprites.Clear();
    //        level++;
    //        text = new JustText[3];
    //        for (int i = 0; i < 3; i++)
    //        {
    //            foreach (var z in _sockets[i].WAR)
    //            {
    //                _gems.Remove((Gem)z);
    //                _game[0].WAR.Remove(z);
    //            }
    //            _sockets[i].WAR.Clear();
    //        }
    //        for (int i = 0; i < monsters.Count; i++)
    //        {
    //            if ((Dun)i == Dun.Dem) continue;
    //            monsters[(Dun)i] = 0;
    //        }
    //        MonGen(level);
    //    }
    //    GameEnd();
    //}

    public void GameEnd()
    {
        if (_game[0].WAR.Count < 1 && text.Any(x => x != null && x.Text == "Lose"))
        {
            Score += level - 1;
            level = 1;
            Walk++;
            monsters[Dun.Dem] = 0;
            DragDropManager.Clean();
            _gems.Clear();
            _sprites.Clear();
            _sockets.Clear();
            _game.Clear();
            text = new JustText[3];
            WalkEnd();
            for (int i = 0; i < monsters.Count; i++)
            {
                if ((Dun)i == Dun.Dem) continue;
                monsters[(Dun)i] = 0;
            }
            Gener();
        }
    }

    public void WalkEnd()
    {
        if (Walk > 3)
            game.ChangeState(new MainMenu(game, graphicsDevice, content));
    }

    #endregion

    public void Quit(object sender, EventArgs e) => game.Exit();
}
