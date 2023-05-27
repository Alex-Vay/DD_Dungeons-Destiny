using System.Linq;
using System.Runtime.CompilerServices;

namespace Quickie011;

public static class DragDropManager
{
    private static readonly List<IDraggable> _draggables = new();
    private static readonly List<ITargetable> _targets = new();
    private static IDraggable _dragItem;
    private static List<IDraggable> _nodrag = new ();

    public static void NoDrag(IDraggable item)
    {
        _draggables.Remove(item);
        _nodrag.Add(item);
    }

    public static void Drag(IDraggable item)
    {
        //var ind = _nodrag[_nodrag.IndexOf(item)].Position;
        _nodrag.Remove(item);
        _draggables.Add(item);
        _dragItem = item;
        //var i = 0;
        //foreach (var z in _nodrag)
        //    if (ind.X < z.Position.X)
        //    {
        //        z.Position = ind + new Vector2(100, 0) * i;
        //        i++;
        //    }
        CheckTarget();
    }

    public static void AddDraggable(IDraggable item)
    {
        _draggables.Add(item);
    }

    public static void AddTarget(ITargetable item)
    {
        _targets.Add(item);
    }

    private static void CheckDragStart()
    {
        if (InputManager.MouseClicked)
        {
            for (int i = _draggables.Count-1; i >= 0; i--)
            {
                if (_draggables[i].Rectangle.Contains(InputManager.MousePosition))
                {
                    _dragItem = _draggables[i];
                    break;
                }
            }
            
        }
    }

    private static void CheckTarget()
    {
        //var flag = new Rectangle();
        //foreach (var item in _targets)
        //    if (item.Rectangle.Contains(_dragItem.Position))
        //        flag = item.Rectangle;
        foreach (var item in _targets)
        {
            if (item.Rectangle.Contains(_dragItem.Position) && item != _targets.Last())//(InputManager.MousePosition))
            {
                if (item.WAR.Contains(_dragItem))
                {
                    _dragItem.Position = item.Position - new Vector2(item.Rectangle.Width / 2 - 100, 0) + new Vector2(100, 0) * (item.WAR.IndexOf(_dragItem)); //!= -1 ? item.WAR.IndexOf(_dragItem) : item.WAR.Count);
                }
                else
                {
                    _dragItem.Position = item.Position - new Vector2(item.Rectangle.Width/ 2 - 100, 0) + new Vector2(100, 0) * item.WAR.Count;
                    item.WAR.Add(_dragItem);
                }
                break;
            }
            else if (item.WAR.Contains(_dragItem))
            {
                item.WAR.Remove(_dragItem);
                break;
            }
        }
    }

    public static void AddDrag (ITargetable tar, IDraggable item)
    { 
        tar.WAR.Add(item);
        item.Position = tar.Position - new Vector2(tar.Rectangle.Width / 2, 0) + new Vector2(100, 0) * tar.WAR.Count;
    }



    private static void CheckDragStop()
    {
        if (InputManager.MouseReleased)
        {
            CheckTarget();
            _dragItem = null;
        }
    }

    public static void Update()
    {
        CheckDragStart();

        if (_dragItem is not null)
        {
            _dragItem.Position = InputManager.MousePosition;
            CheckDragStop();
        }
    }
}
