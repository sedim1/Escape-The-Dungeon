using DungeonCrawlerJam2026.Utilties.Scenes;
using Raylib_cs;

namespace DungeonCrawlerJam2026.GameScenes;

public class MainGameScene : Scene
{
    public override void OnEnter()
    {
        Console.WriteLine("Main Scene OnEnter");
    }

    public override void OnExit()
    {
        Console.WriteLine("Main Scene OnExit");
    }

    public override void Update(float deltaTime)
    {
        if(Raylib.IsKeyPressed(KeyboardKey.D))
            SceneManager.TriggerChange("title");
    }

    public override void Draw()
    {
        Raylib.DrawText("Hello from main game scene",0,20,24,Color.Black);
        Raylib.DrawRectangle(90,90,100,200,Color.Red);
    }
}