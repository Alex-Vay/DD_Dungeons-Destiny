using Controls.DragMechanics;
using States;

namespace DD_Dungeons_Destiny;

public class Game1 : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private State currentState;
    private State nextState;

    public void ChangeState (State state)
    {
        nextState = state;
    }

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        graphics.ApplyChanges();
        Globals.Content = Content;
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        Globals.SpriteBatch = spriteBatch;
        Globals.Game = this;
        currentState = new MainMenu(this, graphics.GraphicsDevice, Content);
    }

    protected override void Update(GameTime gameTime)
    {
        Globals.GameTime = gameTime;
        if (nextState != null)
        {
            currentState = nextState;
            nextState = null;
        }
        currentState.Update(gameTime);
        currentState.PostUpdate(gameTime);
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        currentState.Draw(gameTime, spriteBatch);
        base.Draw(gameTime);
    }
}