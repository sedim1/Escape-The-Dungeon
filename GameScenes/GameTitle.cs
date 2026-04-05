using System.Xml.Schema;
using Raylib_cs;
using DungeonCrawlerJam2026.Utilties.Scenes;

namespace DungeonCrawlerJam2026.GameScenes;

public class GameTitle : Scene
{
    private bool loaded = false;
    private Music titleMusic = new Music();
    public override void OnEnter()
    {
        Console.WriteLine("GameTitle OnEnter");
        titleMusic = Raylib.LoadMusicStream("Resources/Audio/GameTitleBackgroudMusic.mp3");
        Raylib.PlayMusicStream(titleMusic);
        loaded = true;
    }

    public override void OnExit()
    {
        Console.WriteLine("GameTitle OnExit");
        if (loaded)
        {
            Raylib.UnloadMusicStream(titleMusic);
        }
        loaded = false;
    }

    public override void Update(float deltaTime)
    {
        Raylib.UpdateMusicStream(titleMusic);
        if(Raylib.IsKeyPressed(KeyboardKey.Enter))
            SceneManager.TriggerChange("main");
    }

    public override void Draw()
    {
        Global.DrawBackground();
        Raylib.DrawText("ESCAPE \nTHE DUNGEON",183,203,90,Color.White);
        Raylib.DrawText("ESCAPE \nTHE DUNGEON",180,200,90,Color.DarkPurple);
        Raylib.DrawText("ENTER: START GAME", 180, 400, 30, Color.Yellow);
        Raylib.DrawText("Game made by Sedim",850,740,16,Color.Yellow);
    }
}