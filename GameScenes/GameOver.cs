using DungeonCrawlerJam2026.Utilties.Scenes;
using Raylib_cs;

namespace DungeonCrawlerJam2026.GameScenes;

public class GameOver : Scene
{
    private Sound gameOverSound = new Sound();
    private bool loaded = false;
    public GameOver()
    {}

    public override void OnEnter()
    {
     Console.WriteLine("Entered GameOver Scene");   
     gameOverSound = Raylib.LoadSound("Resources/Audio/GameOver.wav");
     Raylib.PlaySound(gameOverSound);
     loaded = true;
    }

    public override void OnExit()
    {
        if (loaded)
        {
            Console.WriteLine("Exiting GameOver Scene");
            Raylib.UnloadSound(gameOverSound);
            Console.WriteLine("GameOver finished unloading resorces");
        }

        loaded = false;
    }

    public override void Update(float deltaTime)
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            
            SceneManager.TriggerChange("title");
        }

        if (Raylib.IsKeyPressed(KeyboardKey.R))
        {
            SceneManager.TriggerChange("main");
        }
    }

    public override void Draw()
    {
        Global.DrawBackground();
        Raylib.DrawText("GAME OVER",183,203,120,Color.White);
        Raylib.DrawText("GAME OVER",180,200,120,Color.Red);
        Raylib.DrawText("ENTER: GameTitle",180,400,30,Color.White);
        Raylib.DrawText("R: Restart",180,464,30,Color.White);
    }
}