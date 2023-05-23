using Quickie011;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DD_Dungeons_Destiny;

public class JustText
{

    private SpriteFont SF;
    public string Text;
    private Vector2 Pos;
    private Color Color;

    public JustText(SpriteFont sF, string text, Vector2 pos, Color color)
    {
        SF = sF;
        Text = text;
        Pos = pos;
        Color = color;
    }

    public void Draw()
    {
        Globals.SpriteBatch.DrawString(SF, Text, Pos, Color);
    }

    //public void Update(GameTime gameTime)
    //{

    //}
}
