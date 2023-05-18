using System.Threading;

namespace Quickie011;

public class Socket : Sprite, ITargetable
{
    public int MonsterC { get; set; }
    public List<IDraggable> WAR { get; set; }
    public Socket(Texture2D tex, Vector2 pos) : base(tex, pos)
    {
        WAR = new List<IDraggable>();
        (this as ITargetable).RegisterTargetable();
    }
}
