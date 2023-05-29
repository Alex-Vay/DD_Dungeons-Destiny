using Controls.DragMechanics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace DD_Dungeons_Destiny;
public enum DungeonObjectType
{
    Sk,
    Sl,
    G,
    //Th,
    H,
    //Dem
}

public enum UnitType
{
    Tank,
    Mag,
    Warrior,
    Thief,
    Gurd,
    //Admin
}

public class Table
{
    public Dictionary<DungeonObjectType, int> objectsOnLevel = new Dictionary<DungeonObjectType, int>() 
    { { DungeonObjectType.Sk, 0 }, { DungeonObjectType.Sl, 0 }, { DungeonObjectType.G, 0 }, { DungeonObjectType.H, 0 } }; //{ Dun.Th, 0 }, { Dun.H, 0 },  { Dun.Dem, 0 } };
    private int level = 1;
    public int Score = 0;
    private int Walk = 1;
    private int Res = 0;
    public int Phase = 1;
    int objectsTypesCount = 4;

    //private int Thes = 0;

    public readonly List<Component> Buttons = new();
    public readonly List<Unit> Units = new();
    public readonly List<DragActiveAreas> FightAreas = new();
    public readonly List<Sprite> Objects = new();
    public TextClass[] FightResult = new TextClass[3] { null, null, null };
    public readonly List<DragActiveAreas> UnitsAreas = new();
    //public List<Sprite> _sprites2 = new();

    ContentManager content = Globals.Content;
    Game1 game = Globals.Game;
    GraphicsDevice graphicsDevice = Globals.Game.GraphicsDevice;

    public Table() { }

    public void StartGame()
    {
        var unitsAreaText = content.Load<Texture2D>("11");
        UnitsAreas.Add(new(unitsAreaText, new(425, 850)));
        UnitsAreas.Add(new(unitsAreaText, new(1325, 850)));
        for (int i = 0; i < 7; i++)
        {
            var z = (UnitType)new Random().Next(4);
            Units.Add(new Unit(content.Load<Texture2D>(z.ToString()), UnitsAreas[0].Position - new Vector2(UnitsAreas[0].Rectangle.Width / 2 - 100, 0) + new Vector2(110, 0) * UnitsAreas[0].UnitsList.Count, z));
            UnitsAreas[0].UnitsList.Add(Units.Last());
        }
        GenerateFirstPhase();
    }

    public void GenerateFirstPhase()
    {
        FightResult = new TextClass[3] { null, null, null };
        Buttons.Clear();
        FightAreas.Clear();
        DragDropManager.CleanAreas();
        var buttonTexture = content.Load<Texture2D>("Button");
        var buttonFont = content.Load<SpriteFont>("Fonts\\SplashFont");

        var fight1 = new Button(buttonTexture, buttonFont)
        {
            //Position = new Vector2(960f/ graphicsDevice.DisplayMode.Width * 600 - buttonTexture.Width/2, 360f / graphicsDevice.DisplayMode.Height * 300 - buttonTexture.Height/2),
            Position = new Vector2(400, 900),
            Text = "Драка"
        };
        fight1.Click += (sender, e) => Fight(0);
        Buttons.Add(fight1);
        var fight2 = new Button(buttonTexture, buttonFont)
        {
            Position = new Vector2(400 + 600, 900),
            Text = "Драка"
        };
        fight2.Click += (sender, e) => Fight(1);
        Buttons.Add(fight2);
        var fight3 = new Button(buttonTexture, buttonFont)
        {
            Position = new Vector2(400 + 2 * 600, 900),
            Text = "Драка"
        };
        fight3.Click += (sender, e) => Fight(2);
        Buttons.Add(fight3);

        var doorText = content.Load<Texture2D>("Door");
        for (int i = 0; i < 3; i++)
            FightAreas.Add(new(doorText, new(400 + (i * 600), 500)));
        FillDungeonWithObjects(level);
    }

    public void GenerateSecondPhase()
    {
        var buttonTexture = content.Load<Texture2D>("Button");
        var buttonFont = content.Load<SpriteFont>("Fonts\\SplashFont");
        FightResult = new TextClass[1] { null };
        Buttons.Clear();
        FightAreas.Clear();
        DragDropManager.CleanAreas();
        var drink = new Button(buttonTexture, buttonFont)
        {
            Position = new Vector2(400 + 200, 900),
            Text = "Выпить"
        };
        drink.Click += (sender, e) => Drink(3);
        Buttons.Add(drink);
        FightAreas.Add(new(content.Load<Texture2D>("Door"), new(920, 500)));

        //var thret = new Button(buttonTexture, buttonFont)
        //{
        //    //Position = new Vector2(960f/ graphicsDevice.DisplayMode.Width * 600 - buttonTexture.Width/2, 360f / graphicsDevice.DisplayMode.Height * 300 - buttonTexture.Height/2),
        //    Position = new Vector2(400 + 800, 900),
        //    Text = "Open"
        //};
        //thret.Click += (sender, e) => Open(3);
        //components.Add(start5);
    }

    #region GameMech
    public void Fight(int i)
    {
        if (FightAreas[i].MonsterCount <= FightAreas[i].UnitsList.Count || FightAreas[i].UnitsList.Select(x => x.UnitType).Contains((UnitType)i))
            FightResult[i] = (new(content.Load<SpriteFont>("Fonts\\SplashFont"), "Победа", new Vector2(400 + 600 * (i), 50), Color.Gold));
        else
            FightResult[i] = (new(content.Load<SpriteFont>("Fonts\\SplashFont"), "Поражение", new Vector2(400 + 600 * (i), 50), Color.Gold));
    }

    public void Drink(int i)
    {
        FightAreas[0].MonsterCount = objectsOnLevel[(DungeonObjectType)i];
        if (FightAreas[0].UnitsList.Count >= 1 && Res == 0) Res = FightAreas[0].MonsterCount;
        FightResult[0] = (new(content.Load<SpriteFont>("Fonts\\SplashFont"), "Победа", new Vector2(400 + 600, 50), Color.Gold));
    }

    //public void Open(int i)
    //{
    //    _sockets[1].MonsterC = monsters[(Dun)i];
    //    if (_sockets[1].WAR.Select(x => x.WAR).Contains((WAR)i))
    //        Thes = _sockets[1].MonsterC;
    //    else
    //        Thes = Math.Max(_sockets[1].MonsterC - _sockets[1].WAR.Count, _sockets[1].WAR.Count);
    //    text[1] = (new(content.Load<SpriteFont>("Fonts\\SplashFont"), "Win", new Vector2(400 + 800, 50), Color.Gold));
    //}

    public void CheckTrueRevival()
    {
        if (Res == 0 && FightResult[0] != null && FightResult[0].Text == "Победа" && Phase == 2)
        {
            LevelClean();
            Phase = 1;
            GenerateFirstPhase();
        }
        if (InputManager.MouseClicked && Res > 0)
            foreach (var item in UnitsAreas[1].UnitsList)
            {
                if (item.Rectangle.Contains(InputManager.MousePosition))
                {
                    DragDropManager.Drag(item);
                    DragDropManager.Update();
                    UnitsAreas[1].UnitsList.Remove(item);
                    Res--;
                    var zero = UnitsAreas[1].Position - new Vector2(UnitsAreas[1].Rectangle.Width / 2 - 100, 0);
                    for (int j = 0; j < UnitsAreas[1].UnitsList.Count; j++)
                    {
                        UnitsAreas[1].UnitsList[j].Position = zero + new Vector2(110, 0) * j;
                    }
                    break;
                }
                
            }
        if (UnitsAreas[1].UnitsList.Count == 0 && Res > 0)
            Res = 0;
    }

    //Рефлексия
    public void FillDungeonWithObjects(int x)
    {
        for (int i = 0; i < objectsOnLevel.Count; i++)
        {
            //if ((Dun)i == Dun.Dem) continue;
            objectsOnLevel[(DungeonObjectType)i] = 0;
        }
        if (x > 8) x = 8;
        for (int i = 0; i < x; i++)
        {
            objectsOnLevel[(DungeonObjectType)(new Random().Next(objectsTypesCount))]++;
        }
        for (int i = 0; i < 3; i++)
        {
            FightAreas[i].MonsterCount = objectsOnLevel[(DungeonObjectType)i];
            var gemTexture = content.Load<Texture2D>(((DungeonObjectType)i).ToString());
            for (int j = 0; j < objectsOnLevel[(DungeonObjectType)i]; j++)
                Objects.Add(new(gemTexture, FightAreas[i].Position - FightAreas[i].origin + new Vector2(100, 100) + new Vector2(j % 3 * 200, j / 3 * 150)));
        }
    }

    public void LevelEnd()
    {
        CheckTrueRevival();
        if (FightResult.All(x => x != null && x.Text == "Победа") && Res == 0 && Phase == 1)
        {
            LevelClean();
            Phase = 2;
            level++;
            GenerateSecondPhase();
        }
        WalkEnd();
    }


    public void LevelClean()
    {
        Objects.Clear();
        for (int i = 0; i < FightAreas.Count; i++)
        {
            foreach (var unit in FightAreas[i].UnitsList)
            {
                unit.Position = UnitsAreas[1].Position - new Vector2(UnitsAreas[1].Rectangle.Width / 2 - 100, 0) + new Vector2(100, 0) * UnitsAreas[1].UnitsList.Count;
                DragDropManager.NoDrag(unit);
                UnitsAreas[1].UnitsList.Add(unit);
                UnitsAreas[0].UnitsList.Remove(unit);
            }
            FightAreas[i].UnitsList = new List<IDraggable>();
        }
        UpdateUnitsPos();
    }

    public void UpdateUnitsPos()
    {
        var zeroPos1 = UnitsAreas[0].Position - new Vector2(UnitsAreas[0].Rectangle.Width / 2 - 100, 0);
        for (int j = 0; j < UnitsAreas[0].UnitsList.Count; j++)
        {
            UnitsAreas[0].UnitsList[j].Position = zeroPos1 + new Vector2(100, 0) * j;
        }
        var zeroPos2 = UnitsAreas[1].Position - new Vector2(UnitsAreas[1].Rectangle.Width / 2 - 100, 0);
        for (int j = 0; j < UnitsAreas[1].UnitsList.Count; j++)
        {
            UnitsAreas[1].UnitsList[j].Position = zeroPos2 + new Vector2(100, 0) * j;
        }
        
    }

    public bool CheckMistake()
    {
        var unitsOnField = 0;
        for (int i = 0; i < FightAreas.Count; i++)
            unitsOnField += FightAreas[i].UnitsList.Count;
        var allMonsters = 0;
        for (int i = 0; i < FightAreas.Count; i++)
            allMonsters += FightAreas[i].MonsterCount;
        return unitsOnField >= allMonsters;
    }

    public void WalkEnd()
    {
        if (UnitsAreas[0].UnitsList.Count < 1 && FightResult.Any(x => x != null && x.Text == "Поражение") && !CheckMistake())
        {
            Score += level - 1;
            level = 1;
            Walk++;
            //monsters[Dun.Dem] = 0;
            DragDropManager.Clean();
            Units.Clear();
            Objects.Clear();
            FightAreas.Clear();
            UnitsAreas.Clear();
            FightResult = new TextClass[3];
            GameEnd();
            for (int i = 0; i < objectsOnLevel.Count; i++)
            {
                //if ((Dun)i == Dun.Dem) continue;
                objectsOnLevel[(DungeonObjectType)i] = 0;
            }
            StartGame();
        }
    }

    public void GameEnd()
    {
        if (Walk > 3)
        {
            Walk = 1;
            LevelClean();
            game.ChangeState(new MainMenu(game, graphicsDevice, content));
        }
    }

    #endregion

    public void Quit(object sender, EventArgs e) => game.Exit();
}