using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DD_Dungeons_Destiny
{
    enum Stat
    {
        MainMenu,
        Game,
        Final,
        Pause
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Stat Stat = Stat.MainMenu;
        private List<Component> l;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            MainMenu.MainBackground = Content.Load<Texture2D>("Images\\MainBackground");
            //MainMenu.Font = Content.Load<SpriteFont>("Fonts\\SplashFont");
            var start = new Button(Content.Load<Texture2D>("Images\\Button"), Content.Load<SpriteFont>("Fonts\\SplashFont"))
            {
                Position = new Vector2(960, 360),
                Text = "Start"
            };
            var set = new Button(Content.Load<Texture2D>("Images\\Button"), Content.Load<SpriteFont>("Fonts\\SplashFont"))
            {
                Position = new Vector2(960, 500),
                Text = "Setting"
            };
            var quit = new Button(Content.Load<Texture2D>("Images\\Button"), Content.Load<SpriteFont>("Fonts\\SplashFont"))
            {
                Position = new Vector2(960, 640),
                Text = "Quit"
            };
            quit.Click += Quit;
            l = new List<Component>() { start, set, quit};
            // TODO: use this.Content to load your game content here
        }

        public void Quit(object sender, EventArgs e) => Exit();

        protected override void Update(GameTime gameTime)
        {
            switch (Stat)
            {
                case Stat.MainMenu:
                    MainMenu.Update();
                    if (Keyboard.GetState().IsKeyDown(Keys.Space)) Stat = Stat.Game;
                    break;
                case Stat.Game:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape)) Stat = Stat.MainMenu;
                    break;
            }
            foreach (var z in l)
                z.Update(gameTime);
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            // TODO: Add your update logic here
            MainMenu.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            switch (Stat)
            {
                case Stat.MainMenu:
                    MainMenu.Draw(spriteBatch);
                    break;
                case Stat.Game:
                    break;
            }
            foreach (var z in l)
                z.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}