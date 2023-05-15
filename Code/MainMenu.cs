using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DD_Dungeons_Destiny;
static class MainMenu
{
    public static Texture2D MainBackground { get; set; }
    static int timeCounter = 0;
    static Color color;
    public static SpriteFont Font { get; set; }
    static Vector2 TextPos = new Vector2(960, 360);

    static public void Draw (SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(MainBackground, Vector2.Zero, Color.White);
        //spriteBatch.DrawString(Font, "Старт", TextPos, Color.White);
    }

    //static public void Update()
    //{
    //    color = Color.FromNonPremultiplied(255, 255, 255, timeCounter % 256);
    //    timeCounter++;
    //}

    static public void Update() { }
}
