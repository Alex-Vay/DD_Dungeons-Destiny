using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD_Dungeons_Destiny;

static class Dungeon
{
    public static int Width, Height;
    public static Random Rand = new Random();

    static public int GetIntRnd (int min, int max)
    {
        return Rand.Next(min, max);
    }
}

//class Star
//{
//    Vector2 Pos;
//    Vector2 Dir;
//    Color color;

//    public static Texture2D Texture2D {  get; set; }

//    public Star(Vector2 pos, Vector2 dir)
//    {
//        Pos = pos;
//        Dir = dir;
//    }

//    public Star (Vector2 dir)
//    {
//        Dir = dir;
//        RandomSet();
//    }

//    public void Update ()
//    {
//        Pos += Dir;
//        if (Pos.X < 0)
//        {
//            RandomSet();
//        }
//    }

//    public void RandomSet ()
//    {
//        Pos = new Vector2(Dungeon.GetIntRnd(Dungeon.Width, Dungeon.Width + 300), Dungeon.GetIntRnd(0, Dungeon.Height));
//        color = Color.FromNonPremultiplied(Dungeon.GetIntRnd(0, 256), Dungeon.GetIntRnd(0, 256), Dungeon.GetIntRnd(0, 256), 255);
//    }
//}
