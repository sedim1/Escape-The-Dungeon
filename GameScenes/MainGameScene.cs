using DungeonCrawlerJam2026.GameComponents;
using DungeonCrawlerJam2026.Utilties.Scenes;
using Raylib_cs;

namespace DungeonCrawlerJam2026.GameScenes;

public class MainGameScene : Scene
{

    private GameMap level;

    public MainGameScene()
    {
        level = new GameMap();
    }
    
    public override void OnEnter()
    {
        Console.WriteLine("Main Scene OnEnter");
        level.LoadMap("Resources/gamemap.json");
    }

    public override void OnExit()
    {
        Console.WriteLine("Main Scene OnExit");
    }

    public override void Update(float deltaTime)
    {
    }

    public override void Draw()
    {
        level.DrawMinimap();
    }
}