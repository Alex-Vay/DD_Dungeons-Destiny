namespace Controls.DragMechanics;

public class Unit : Sprite, IDraggable
{
    public UnitType UnitType { get; set; }

    public Unit(Texture2D tex, Vector2 pos, UnitType type) : base(tex, pos)
    {
        UnitType = type;
        (this as IDraggable).RegisterDraggable();
    }
}
