using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD_Dungeons_Destiny;

//public enum Dun
//{
//    Sk,
//    Sl,
//    G,
//    Th,
//    Dem
//}

public class Table
{
    public readonly int Sk;
    public readonly int Sl;
    public readonly int G;
    public readonly int Th;
    public readonly int Dem = 0;
    public readonly int H;
    public int Phase;
    public int level;

    private List<Component> components = new();
    private List<Gem> _gems = new();
    private List<Socket> _sockets = new();
    private List<Sprite> _sprites = new();
    private JustText[] text = new JustText[3] { null, null, null };

    public Table (int sk, int sl, int g, int th, int dem)
    {
        Sk = sk;
        Sl = sl;
        G = g;
        Th = th;
        Dem += dem;
        if (Dem >= 3)
        {
            Dem = 0;
            H++;
        }
    }
}
