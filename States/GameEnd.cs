using Controls.DragMechanics;
using States;
using System.IO;
using System.Security.AccessControl;

namespace DD_Dungeons_Destiny;

public class GameEnd : State
{
    Dictionary<int, string> congr = new();
    public GameEnd(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
    {
        var file = File.ReadAllText(@"C:\Users\Alex\Desktop\D&D - Dungeons & Destiny\Content\congr.txt");
        var status = file.Split("\n\r\n");
        var needScore = new int[] { 6, 14, 20, 24, 25 };
        for (int i = 0; i < status.Length; i++)
            congr.Add(needScore[i], status[i]);
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        graphicsDevice.Clear(Color.Black);
        var font = content.Load<SpriteFont>("Fonts\\SplashFont2");
        var score = Globals.Score;
        var text = $"Вы набрали {score} опыта\nВаш статус: ";
        string status = null;
        foreach (var st in congr)
            if (score < st.Key)
            {
                status = st.Value;
                break;
            }
        text += status == null ?"\n" + congr.Last().Value : "\n" + status;
        spriteBatch.DrawString(font, text, new Vector2(20, 20), Color.Gold);
        spriteBatch.End();
    }

    public override void PostUpdate(GameTime gameTime)
    {

    }

    public override void Update(GameTime gameTime)
    {
        if (Mouse.GetState().LeftButton == ButtonState.Pressed) game.Exit();
    }
}
