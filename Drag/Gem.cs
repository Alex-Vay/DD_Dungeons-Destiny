namespace Quickie011;

public class Gem : Sprite, IDraggable
{
    public WAR WAR { get; set; }
    public Gem(Texture2D tex, Vector2 pos, WAR type) : base(tex, pos)
    {
        WAR = type;
        (this as IDraggable).RegisterDraggable();
    }
}
