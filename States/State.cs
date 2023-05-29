namespace States;
public abstract class State
{
    protected ContentManager content;
    protected GraphicsDevice graphicsDevice;
    protected Game1 game;

    public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    public abstract void PostUpdate(GameTime gameTime);
    public State(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
    {
        this.game = game;
        this.graphicsDevice = graphicsDevice;
        this.content = content;
    }
    public abstract void Update(GameTime gameTime);
}
