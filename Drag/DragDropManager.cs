using System.Linq;
using System.Runtime.CompilerServices;

namespace Quickie011;

public static class DragDropManager
{
    private static readonly List<IDraggable> _draggables = new();
    private static List<ITargetable> _targets = new();
    private static IDraggable _dragItem;
    private static List<IDraggable> _nodrag = new ();
    private static List<IDraggable> _draggables2 = new();

    public static void NoDrag(IDraggable item)
    {
        _nodrag.Add(item);
    }

    public static void Drag(IDraggable item)
    {
        //var ind = _nodrag[_nodrag.IndexOf(item)].Position;

        _nodrag.Remove(item);
        Update2();
        //_dragItem = item;

        //var i = 0;
        //foreach (var z in _nodrag)
        //    if (ind.X < z.Position.X)
        //    {
        //        z.Position = ind + new Vector2(100, 0) * i;
        //        i++;
        //    }

        //CheckTarget();
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
            foreach (var draggable in _draggables2)
            {
                if (draggable.Rectangle.Contains(InputManager.MousePosition) && !_nodrag.Contains(draggable))
                {
                    _dragItem = draggable;
                    break;
                }
            }

            //for (int i = _draggables.Count-1; i >= 0; i--)
            //{
            //    if (_draggables[i].Rectangle.Contains(InputManager.MousePosition))
            //    {
            //        _dragItem = _draggables[i];
            //        break;
            //    }
            //}
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
            if (!_nodrag.Contains(_dragItem) && item.Rectangle.Contains(_dragItem.Position) && item.Position != new Vector2 (1325, 850))//(InputManager.MousePosition))
            {
                if (item.WAR.Contains(_dragItem))
                {
                    _dragItem.Position = item.Position - new Vector2(item.Rectangle.Width / 2 - 100, 0) + new Vector2(100, 0) * (item.WAR.IndexOf(_dragItem)); //!= -1 ? item.WAR.IndexOf(_dragItem) : item.WAR.Count);
                }
                else
                {
                    _dragItem.Position = item.Position - new Vector2(item.Rectangle.Width/ 2 - 100, 0) + new Vector2(100, 0) * item.WAR.Count;
                    item.WAR.Add(_dragItem);
                    //break;
                }
            }
            else if (item.WAR.Contains(_dragItem))
            {
                item.WAR.Remove(_dragItem);
            }
            var zero2 = item.Position - new Vector2(item.Rectangle.Width / 2 - 100, 0);
            for (int j = 0; j < item.WAR.Count; j++)
            {
                item.WAR[j].Position = zero2 + new Vector2(100, 0) * j;
            }
        }
    }

    public static void Update2()
    {
        var z = GameMenu.table._gems;
        var zz = new List<Gem>();
        var d = z.Select(x => x as IDraggable).ToList();
        foreach (var item in _targets)
            for (int j = 0; j < item.WAR.Count; j++)
            {
                zz.Add(z[_draggables.IndexOf(item.WAR[j])]);
            }

        for (int item = _nodrag.Count - 1; item >= 0; item--)
            zz.Add(z[d.IndexOf(_nodrag[item])]);

        //for (int item = 0; item < _nodrag.Count; item++)
        //    zz.Add(z[d.IndexOf(_nodrag[item])]);

        //foreach (var item in z)
        //    if (!zz.Contains(item) && _nodrag.Contains((IDraggable)item)) zz.Add(item);

        foreach (var item in z)
            if (!zz.Contains(item)) zz.Add(item);

        _draggables2 = zz.Select(x => x as IDraggable).ToList();
        _draggables2.Reverse();

        //_draggables = zz.Select(x => x as IDraggable).ToList();

        GameMenu._gems = zz;
    }
//public static void AddDrag (ITargetable tar, IDraggable item)
//{ 
//    tar.WAR.Add(item);
//    item.Position = tar.Position - new Vector2(tar.Rectangle.Width / 2, 0) + new Vector2(100, 0) * tar.WAR.Count;
//}

    public static void Clean()
    {
        _draggables.Clear();
        _targets.Clear();
        _nodrag.Clear();
    }
    public static void CleanT()
    {
        var t = new List<ITargetable>();
        t.Add(_targets[0]);
        t.Add(_targets[1]);
        _targets = t;
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
