namespace Controls.DragMechanics;

public interface IDraggable
{
    UnitType UnitType { get; set; }
    Rectangle Rectangle { get; }
    Vector2 Position { get; set; }

    void RegisterDraggable()
    {
        DragDropManager.AddDraggable(this);
    }
}
