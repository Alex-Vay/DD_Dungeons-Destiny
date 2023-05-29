namespace Controls.DragMechanics;

public static class InputManager
{
    public static MouseState LastMouseState;
    public static Vector2 MousePosition => Mouse.GetState().Position.ToVector2();
    public static bool MouseClicked { get; private set; }
    public static bool MouseReleased { get; private set; }

    public static void Update()
    {
        MouseClicked = Mouse.GetState().LeftButton == ButtonState.Pressed
                       && LastMouseState.LeftButton == ButtonState.Released;

        MouseReleased = Mouse.GetState().LeftButton == ButtonState.Released
                       && LastMouseState.LeftButton == ButtonState.Pressed;

        LastMouseState = Mouse.GetState();
    }
}
