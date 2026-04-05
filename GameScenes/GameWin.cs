using Raylib_cs;
using DungeonCrawlerJam2026.Utilties.Scenes;

namespace DungeonCrawlerJam2026.GameScenes;

public class GameWin : Scene
{
    public override void OnEnter()
    {
        Console.WriteLine("Entered GameWin Scene");   
    }

    public override void OnExit(){
        Console.WriteLine("Exiting GameWin Scene");
    }

    public override void Update(float deltaTime)
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            
            SceneManager.TriggerChange("title");
        }
    }

    public override void Draw()
    {
        Global.DrawBackground();
        Raylib.DrawText("YOU ESCAPED\nALIVE...",183,203,90,Color.White);
        Raylib.DrawText("YOU ESCAPED\nALIVE...",180,200,90,Color.Red);
        Raylib.DrawText("THANK YOU FOR PLAYING MY GAME!",180,500,30,Color.White);
        Raylib.DrawText("PRESS ENTER TO CONTINUE",180,564,30,Color.White);
    }
}