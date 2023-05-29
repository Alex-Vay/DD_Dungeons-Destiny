using System.Threading;

namespace Controls.DragMechanics;

public class DragActiveAreas : Sprite, ITargetable
{
    public int MonsterCount { get; set; }
    public List<IDraggable> UnitsList { get; set; }
    public DragActiveAreas(Texture2D tex, Vector2 pos) : base(tex, pos)
    {
        UnitsList = new List<IDraggable>();
        (this as ITargetable).RegisterTargetable();
    }
}
