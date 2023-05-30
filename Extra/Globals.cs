using Microsoft.Xna.Framework.Content;

namespace Extra;

public static class Globals
{
    public static GameTime GameTime { get; set; }
    public static ContentManager Content { get; set; }
    public static SpriteBatch SpriteBatch { get; set; }
    public static Game1 Game { get; set; }
    public static int Score { get; set; }
}
