namespace Extra;

public class TextClass
{

    private SpriteFont spriteFont = Globals.Content.Load<SpriteFont>("Fonts\\SplashFont");
    public string Text;
    private Vector2 Pos;
    private Color Color;

    public TextClass(SpriteFont sF, string text, Vector2 pos, Color color)
    {
        spriteFont = sF;
        Text = text;
        Pos = pos;
        Color = color;
    }

    public TextClass(string text, Vector2 pos, Color color)
    {
        Text = text;
        Pos = pos;
        Color = color;
    }

    public void Draw()
    {
        Globals.SpriteBatch.DrawString(spriteFont, Text, Pos, Color);
    }
}
