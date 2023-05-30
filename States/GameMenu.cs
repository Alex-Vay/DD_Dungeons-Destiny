using Controls.DragMechanics;
using Extra;
using States;


namespace DD_Dungeons_Destiny;

public class GameMenu : State
{
    public static Table table = new Table();
    public static List<Unit> Units = new();

    public GameMenu(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
    {
        table.StartGame();
        Units = table.Units;
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(content.Load<Texture2D>("Dungeon"),
            new Rectangle(0, 0, graphicsDevice.DisplayMode.Width, graphicsDevice.DisplayMode.Height), Color.White);
       
        foreach (var item in table.FightAreas)
        {
            item.Draw();
        }

        //foreach (var z in table.UnitsAreas)
        //    z.Draw();

        foreach (var item in table.Objects)
            item.Draw(Color.Gray);
        var countTextFields = 0;
        foreach (var z in table.objectsOnLevel)
            spriteBatch.DrawString(content.Load<SpriteFont>("Fonts\\SplashFont"), z.Value.ToString(), new Vector2(20, countTextFields++ * 50), Color.Gold);
        spriteBatch.DrawString(content.Load<SpriteFont>("Fonts\\SplashFont"), Globals.Score.ToString(), new Vector2(20, 5 * 50), Color.Gold);
        foreach (var item in Units)
            item.Draw();
        foreach (var comp in table.Buttons)
            comp.Draw(gameTime, spriteBatch);
        foreach (var t in table.FightResult)
            if (t != null)
                t.Draw();
        spriteBatch.End();
    }

    public override void PostUpdate(GameTime gameTime)
    { }

    public override void Update(GameTime gameTime)
    {
        DragDropManager.Update2();
        InputManager.Update();
        DragDropManager.Update();
        foreach (var comp in table.Buttons)
            comp.Update(gameTime);
        table.LevelEnd();
        table.WalkEnd();
        table.GameEnd();
    }
}
