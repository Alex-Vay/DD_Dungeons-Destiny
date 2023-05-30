using Controls;
using States;

namespace DD_Dungeons_Destiny;
public class MainMenu : State
{
    private List<Component> buttons;

    public MainMenu(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
    {
        var buttonTexture = content.Load<Texture2D>("Button");
        var buttonFont = content.Load<SpriteFont>("Fonts\\SplashFont");
        var start = new Button(buttonTexture, buttonFont)
        {
            //Position = new Vector2(960f/ graphicsDevice.DisplayMode.Width * 600 - buttonTexture.Width/2, 360f / graphicsDevice.DisplayMode.Height * 300 - buttonTexture.Height/2),
            Position = new Vector2(960, 360),
            Text = "Начать"
        };
        start.Click += Start;

        var quit = new Button(buttonTexture, buttonFont)
        {
            Position = new Vector2(960, 540),
            Text = "Выйти"
        };
        quit.Click += Quit;
        buttons = new List<Component>() { start, quit };
    }
    public void Quit(object sender, EventArgs e) => game.Exit();

    public void Start(object sender, EventArgs e) => game.ChangeState(new GameMenu(game, graphicsDevice, content));

    static public void Update() { }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(content.Load<Texture2D>("MainBackground"),
            new Rectangle(0, 0, graphicsDevice.DisplayMode.Width, graphicsDevice.DisplayMode.Height), Color.White);
        foreach (var item in buttons)
            item.Draw(gameTime, spriteBatch);
        spriteBatch.End();
    }

    public override void PostUpdate(GameTime gameTime)
    { }

    public override void Update(GameTime gameTime)
    {
        foreach (var item in buttons)
            item.Update(gameTime);
    }
}

    //var set = new Button(buttonTexture, buttonFont)
        //{
        //    Position = new Vector2(960, 500),
        //    Text = "Setting"
        //};

    //static public void Update()
    //{
    //    color = Color.FromNonPremultiplied(255, 255, 255, timeCounter % 256);
    //    timeCounter++;
    //}