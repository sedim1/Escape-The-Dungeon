using System.Xml.Schema;
using Raylib_cs;
using DungeonCrawlerJam2026.Utilties.Scenes;

namespace DungeonCrawlerJam2026.GameScenes;

public class GameTitle : Scene
{
    public override void OnEnter()
    {
        Console.WriteLine("GameTitle OnEnter");
    }

    public override void OnExit()
    {
        Console.WriteLine("GameTitle OnExit");
    }

    public override void Update(float deltaTime)
    {
        if(Raylib.IsKeyPressed(KeyboardKey.A))
            SceneManager.TriggerChange("main");
    }

    public override void Draw()
    {
        Raylib.DrawText("Hello from game title scene",0,20,24,Color.Black);
        Raylib.DrawRectangle(200,200,100,200,Color.Green);
    }
}