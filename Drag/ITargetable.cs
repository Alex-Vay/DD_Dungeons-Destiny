namespace Quickie011;

public interface ITargetable
{
    List<IDraggable> WAR { get; set; }
    int MonsterC { get; set; }
    Rectangle Rectangle { get; }
    Vector2 Position { get; set; }

    void RegisterTargetable()
    {
        DragDropManager.AddTarget(this);
    }
}
