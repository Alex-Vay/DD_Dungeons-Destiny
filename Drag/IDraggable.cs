namespace Quickie011;

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
