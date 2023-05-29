namespace Controls.DragMechanics;

public static class DragDropManager
{
    private static readonly List<IDraggable> originalDraggables = new();
    private static List<ITargetable> dragActiveAreas = new();
    private static IDraggable dragItem;
    private static List<IDraggable> nodraggables = new();
    private static List<IDraggable> realDraggables = new();

    public static void NoDrag(IDraggable item)
    {
        nodraggables.Add(item);
    }

    public static void Drag(IDraggable item)
    {
        nodraggables.Remove(item);
        Update2();
    }

    public static void AddDraggable(IDraggable item)
    {
        originalDraggables.Add(item);
    }

    public static void AddTarget(ITargetable item)
    {
        dragActiveAreas.Add(item);
    }

    private static void CheckDragStart()
    {
        if (InputManager.MouseClicked)
        {
            foreach (var draggable in realDraggables)
            {
                if (draggable.Rectangle.Contains(InputManager.MousePosition) && !nodraggables.Contains(draggable))
                {
                    dragItem = draggable;
                    break;
                }
            }
        }
    }

    private static void CheckTarget()
    {
        foreach (var item in dragActiveAreas)
        {
            if (!nodraggables.Contains(dragItem) && item.Rectangle.Contains(dragItem.Position) && item != dragActiveAreas[1])
            {
                if (item.UnitsList.Contains(dragItem))
                {
                    var ind = item.UnitsList.IndexOf(dragItem);
                    if (item == dragActiveAreas[0])
                        dragItem.Position = item.Position - new Vector2(item.Rectangle.Width / 2 - 100, 0) + new Vector2(110, 0) * ind;
                    else dragItem.Position = item.Position - new Vector2(item.Rectangle.Width / 2 - 100, 0) + new Vector2(ind % 4 * 125, ind / 4 * 150);
                }
                else
                {
                    var count = item.UnitsList.Count;
                    if (item == dragActiveAreas[0])
                        dragItem.Position = item.Position - new Vector2(item.Rectangle.Width / 2 - 100, 0) + new Vector2(110, 0) * count;
                    else dragItem.Position = item.Position - new Vector2(item.Rectangle.Width / 2 - 100, 0) + new Vector2(count % 4 * 125, count / 4 * 150);
                    item.UnitsList.Add(dragItem);
                }
            }
            else if (item.UnitsList.Contains(dragItem))
            {
                item.UnitsList.Remove(dragItem);
            }
            var zeroPosition = item.Position - new Vector2(item.Rectangle.Width / 2 - 100, 0);
            for (int j = 0; j < item.UnitsList.Count; j++)
            {
                if (item == dragActiveAreas[0]) item.UnitsList[j].Position = zeroPosition + new Vector2(110, 0) * j;
                else item.UnitsList[j].Position = zeroPosition + new Vector2(j % 4 * 110, j / 4 * 150);
            }
        }
    }

    public static void Update2()
    {
        var originalUnitsPosUpd = GameMenu.table.Units;
        var realUnitsPosUpd = new List<Unit>();
        var realDraggabless = originalUnitsPosUpd.Select(x => x as IDraggable).ToList();
        foreach (var item in dragActiveAreas)
            for (int j = 0; j < item.UnitsList.Count; j++)
            {
                realUnitsPosUpd.Add(originalUnitsPosUpd[originalDraggables.IndexOf(item.UnitsList[j])]);
            }
        for (int item = nodraggables.Count - 1; item >= 0; item--)
            realUnitsPosUpd.Add(originalUnitsPosUpd[realDraggabless.IndexOf(nodraggables[item])]);
        foreach (var item in originalUnitsPosUpd)
            if (!realUnitsPosUpd.Contains(item)) realUnitsPosUpd.Add(item);
        realDraggables = realUnitsPosUpd.Select(x => x as IDraggable).ToList();
        realDraggables.Reverse();
        GameMenu.Units = realUnitsPosUpd;
    }

    public static void Clean()
    {
        originalDraggables.Clear();
        dragActiveAreas.Clear();
        nodraggables.Clear();
    }
    public static void CleanAreas()
    {
        var onlyGameAreas = new List<ITargetable>();
        onlyGameAreas.Add(dragActiveAreas[0]);
        onlyGameAreas.Add(dragActiveAreas[1]);
        dragActiveAreas = onlyGameAreas;
    }


    private static void CheckDragStop()
    {
        if (InputManager.MouseReleased)
        {
            CheckTarget();
            dragItem = null;
        }
    }

    public static void Update()
    {
        CheckDragStart();

        if (dragItem is not null)
        {
            dragItem.Position = InputManager.MousePosition;
            CheckDragStop();
        }
    }
}
