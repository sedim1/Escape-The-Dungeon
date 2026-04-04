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

    private RenderTexture2D mapViewport;
    private Camera2D camera2D;
    
    private RenderTexture2D playerViewport;
    private Camera3D  camera3D;
    

    public MainGameScene()
    {
        level = new GameMap();
        characterManager = new CharacterManager();
    }
    
    public override void OnEnter()
    {
        Console.WriteLine("Main Scene OnEnter");
        
        
        GameRenderer.StartRenderer();
        InventoryRenderer.Init();
        
        mapViewport = Raylib.LoadRenderTexture(250, 250);
        camera2D = new Camera2D();
        camera2D.Offset = (new Vector2(mapViewport.Texture.Width/2,mapViewport.Texture.Height/2));
        camera2D.Zoom = 8.0f;
        camera2D.Rotation = 0.0f;
        
        
        playerViewport = Raylib.LoadRenderTexture(725, 750);
        camera3D = new Camera3D();
        camera3D.FovY = 45.0f;
        camera3D.Projection = CameraProjection.Perspective;
        camera3D.Up =  Vector3.UnitY;
        
        level.LoadMap("Resources/gamemap.json");
        characterManager.AddCharacter(new Player(new Vector2i(8,8),270));
        characterManager.AddCharacter(new RedEnemy(new Vector2i(3,5)));
        characterManager.AddCharacter(new BlueEnemy(new Vector2i(1,1)));
        characterManager.AddCharacter(new GreenEnemy(new Vector2i(5,3)));
        characterManager.AddCharacter(new RedEnemy(new Vector2i(8,5)));
        characterManager.AddCharacter(new GreenEnemy(new Vector2i(8,6)));
    }

    public override void OnExit()
    {
        Console.WriteLine("Main Scene OnExit");
        
        InventoryRenderer.End();
        GameRenderer.EndRenderer();
        
        Raylib.UnloadRenderTexture(playerViewport);
        Raylib.UnloadRenderTexture(mapViewport);
        
        characterManager.DeleteAllCharacters();
    }

    public override void Update(float deltaTime)
    {
        characterManager.DeleteDefetaedEnemies();
        PlayerController.ProcessInput(characterManager,level);
        MovementSystem.UpdateCharactersPositions(characterManager.GetCharacters(), deltaTime);
    }

    public override void Draw()
    {
        DrawUIWorld3D();
        DrawUIMap();
        InventoryRenderer.RenderInventory(characterManager.GetPlayer().weapons,750,280);
        GameRenderer.RenderPlayerHealthBar(characterManager.GetPlayer(),750,380,250,50);
    }
    

    private void DrawUIMap()
    {
        camera2D.Target = characterManager.GetPlayer().worldPosition;
        Raylib.BeginTextureMode(mapViewport);
        Raylib.ClearBackground(Color.Black);
        Raylib.BeginMode2D(camera2D);
        GameRenderer.RenderWorld2D(level,characterManager.GetCharacters());
        Raylib.EndMode2D();
        Raylib.EndTextureMode();
        Vector2 screenPos = new Vector2(750, 10);
        Vector2 size = new Vector2(mapViewport.Texture.Width,-mapViewport.Texture.Height);
        Raylib.DrawTextureRec(mapViewport.Texture,new Rectangle(Raymath.Vector2Zero(),size),screenPos,Color.White);
    }

    private void DrawUIWorld3D()
    {
        Vector2i playerDir = characterManager.GetPlayer().GetForwardDirection();
        Vector2 playerWorldPosition = characterManager.GetPlayer().worldPosition;
        Vector3 forward = new Vector3(playerDir.X,-0,playerDir.Y);
        camera3D.Position = new Vector3(playerWorldPosition.X,Global.GRIDSCALE/2,playerWorldPosition.Y);
        camera3D.Target = camera3D.Position + forward;
        Raylib.BeginTextureMode(playerViewport);
        Raylib.ClearBackground(Color.Black);
        Raylib.BeginMode3D(camera3D);
        GameRenderer.Render3DWorld(camera3D,level,characterManager.GetEnemies());
        Raylib.EndMode3D();
        Raylib.DrawText("Health: "+characterManager.GetPlayer().getHealthComponent().getHealth().ToString(),0,0,26,Color.Red);
        Raylib.EndTextureMode();
        Vector2 screenPos = new Vector2(10, 10);
        Vector2 size = new Vector2(playerViewport.Texture.Width,-playerViewport.Texture.Height);
        Raylib.DrawTextureRec(playerViewport.Texture,new Rectangle(Raymath.Vector2Zero(),size),screenPos,Color.White);
    }
}