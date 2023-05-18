namespace Quickie011;

public enum WAR
{
    Warrior,
    Archer,
    Tank
}
public interface IDraggable
{
    WAR WAR { get; set; }
    Rectangle Rectangle { get; }
    Vector2 Position { get; set; }

    void RegisterDraggable()
    {
        DragDropManager.AddDraggable(this);
    }
}
