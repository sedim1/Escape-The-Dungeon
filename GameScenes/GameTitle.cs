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
        Raylib.DrawText("GAME TITLE",183,203,120,Color.White);
        Raylib.DrawText("GAME TITLE",180,200,120,Color.DarkGreen);
        Raylib.DrawText("ENTER: START GAME",180,400,30,Color.Yellow);
    }
}