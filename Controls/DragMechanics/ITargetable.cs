namespace Controls.DragMechanics;

public interface ITargetable
{
    List<IDraggable> UnitsList { get; set; }
    int MonsterCount { get; set; }
    Rectangle Rectangle { get; }
    Vector2 Position { get; set; }

    void RegisterTargetable()
    {
        DragDropManager.AddTarget(this);
    }
}
