using System.Numerics;
using DungeonCrawlerJam2026.Characters;
using DungeonCrawlerJam2026.GameComponents;
using DungeonCrawlerJam2026.Utilties;
using DungeonCrawlerJam2026.Utilties.Scenes;
using Raylib_cs;

namespace DungeonCrawlerJam2026.GameScenes;

public class MainGameScene : Scene
{

    private GameMap level;
    private CharacterManager characterManager;

    private RenderTexture2D playerViewport;
    private RenderTexture2D mapViewport;
    private Camera2D camera2D;

    public MainGameScene()
    {
        level = new GameMap();
        characterManager = new CharacterManager();
    }
    
    public override void OnEnter()
    {
        Console.WriteLine("Main Scene OnEnter");
        mapViewport = Raylib.LoadRenderTexture(250, 250);
        camera2D = new Camera2D();
        camera2D.Offset = (new Vector2(mapViewport.Texture.Width/2,mapViewport.Texture.Height/2));
        camera2D.Zoom = 4.0f;
        camera2D.Rotation = 0.0f;
        level.LoadMap("Resources/gamemap.json");
        characterManager.AddCharacter(new Player(new Vector2i(8,8),0));
        characterManager.AddCharacter(new Enemy(new Vector2i(3,5)));
    }

    public override void OnExit()
    {
        Console.WriteLine("Main Scene OnExit");
        Raylib.UnloadRenderTexture(mapViewport);
        characterManager.DeleteAllCharacters();
    }

    public override void Update(float deltaTime)
    {
        PlayerController.ProcessInput(characterManager,level);
        MovementSystem.UpdateCharactersPositions(characterManager.GetCharacters(), deltaTime);
    }

    public override void Draw()
    {
        DrawUIMap();
    }

    private void DrawUIMap()
    {
        camera2D.Target = characterManager.GetPlayer().worldPosition;
        Raylib.BeginTextureMode(mapViewport);
        Raylib.BeginMode2D(camera2D);
        Raylib.ClearBackground(Color.Black);
        GameRenderer.RenderWorld2D(level,characterManager.GetCharacters());
        Raylib.EndMode2D();
        Raylib.EndTextureMode();
        Vector2 screenPos = new Vector2(750, 10);
        Vector2 size = new Vector2(mapViewport.Texture.Width,-mapViewport.Texture.Height);
        Raylib.DrawTextureRec(mapViewport.Texture,new Rectangle(Raymath.Vector2Zero(),size),screenPos,Color.White);
    }
}