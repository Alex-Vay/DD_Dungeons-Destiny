using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Quickie011;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    private List<Component> components;
    private readonly List<Gem> _gems = new();
    private readonly List<Socket> _sockets = new();
    private readonly List<Sprite> _sprites = new();

    public GameMenu(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
    {
        //var buttonTexture = content.Load<Texture2D>("Images\\Button");
        //var buttonFont = content.Load<SpriteFont>("Fonts\\SplashFont");

        //var start = new Button(buttonTexture, buttonFont)
        //{
        //    //Position = new Vector2(960f/ graphicsDevice.DisplayMode.Width * 600 - buttonTexture.Width/2, 360f / graphicsDevice.DisplayMode.Height * 300 - buttonTexture.Height/2),
        //    Position = new Vector2(960, 360),
        //    Text = "Start"
        //};
        //var set = new Button(buttonTexture, buttonFont)
        //{
        //    Position = new Vector2(960, 500),
        //    Text = "Setting"
        //};
        //var quit = new Button(buttonTexture, buttonFont)
        //{
        //    Position = new Vector2(960, 640),
        //    Text = "Quit"
        //};
        //quit.Click += Quit;
        //components = new List<Component>() { start, set, quit };

        var gemTexture = Globals.Content.Load<Texture2D>("gem");
        //var socketTexture = Globals.Content.Load<Texture2D>("socket");
        var socketTexture = Globals.Content.Load<Texture2D>("Area2");

        for (int i = 0; i < 7; i++)
        {
            _gems.Add(new(gemTexture, new(600 + (i * 75), 800), (WAR)new Random().Next(3)));
            //_sockets.Add(new(socketTexture, new(600 + (i * 75), 900)));
        }
        for (int i = 0; i < 3; i++)
            _sockets.Add(new(socketTexture, new(400 + (i * 600), 500)));
        MonGen(10);
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
                _sprites.Add(new(gemTexture, _sockets[i].Position + new Vector2(j * 50, 0)));
        }

        //var c = z.Select(p => p.GetValue(new object())).ToArray();
        
    }

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

        foreach (var item in _gems)
        {
            item.Draw();
        }
        foreach (var item in _sprites)
            item.Draw(Color.Gray);
        for (int i = 0; i < z.Length; i++)
            spriteBatch.DrawString(content.Load<SpriteFont>("Fonts\\SplashFont"), z[i].ToString(), new Vector2(20, i*50), Color.Gold);
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

        //foreach (var component in components)
        //    component.Update(gameTime);
    }
}
