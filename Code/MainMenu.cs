using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace DD_Dungeons_Destiny;
public class MainMenu : State
{
    public static Texture2D MainBackground { get; set; }
    //static int timeCounter = 0;
    //static Color color;
    private List<Component> components;
    public static SpriteFont Font { get; set; }

    public MainMenu(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
    {
        var buttonTexture = content.Load<Texture2D>("Images\\Button");
        var buttonFont = content.Load<SpriteFont>("Fonts\\SplashFont");

        var start = new Button(buttonTexture, buttonFont)
        {
            //Position = new Vector2(960f/ graphicsDevice.DisplayMode.Width * 600 - buttonTexture.Width/2, 360f / graphicsDevice.DisplayMode.Height * 300 - buttonTexture.Height/2),
            Position = new Vector2(960, 360),
            Text = "Start"
        };
        start.Click += Start;
        var set = new Button(buttonTexture, buttonFont)
        {
            Position = new Vector2(960, 500),
            Text = "Setting"
        };
        var quit = new Button(buttonTexture, buttonFont)
        {
            Position = new Vector2(960, 640),
            Text = "Quit"
        };
        quit.Click += Quit;
        components = new List<Component>() { start, set, quit };
    }
    public void Quit(object sender, EventArgs e) => game.Exit();

    public void Start(object sender, EventArgs e) => game.ChangeState(new GameMenu(game, graphicsDevice, content));

    static public void Update() { }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(content.Load<Texture2D>("Images\\MainBackground"),
            new Rectangle(0, 0, graphicsDevice.DisplayMode.Width, graphicsDevice.DisplayMode.Height), Color.White);
            //Vector2.Zero, Color.White);
        foreach (var component in components)
            component.Draw(gameTime, spriteBatch);
        spriteBatch.End();
        //spriteBatch.Draw(MainBackground, Vector2.Zero, Color.White);
    }

    public override void PostUpdate(GameTime gameTime)
    {

    }

    public override void Update(GameTime gameTime)
    {
        foreach (var component in components)
            component.Update(gameTime);
    }

    //static public void Update()
    //{
    //    color = Color.FromNonPremultiplied(255, 255, 255, timeCounter % 256);
    //    timeCounter++;
    //}
}
