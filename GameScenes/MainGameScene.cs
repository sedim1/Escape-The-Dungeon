using System.Numerics;
using DungeonCrawlerJam2026.Characters;
using DungeonCrawlerJam2026.GameComponents;
using DungeonCrawlerJam2026.Utilties;
using DungeonCrawlerJam2026.Utilties.Scenes;
using Raylib_cs;

namespace DungeonCrawlerJam2026.GameScenes;

public class MainGameScene : Scene
{

    private GameMap level = new GameMap();
    private CharacterManager characterManager;

    private RenderTexture2D mapViewport = new RenderTexture2D();
    private Camera2D camera2D = new Camera2D();
    
    private RenderTexture2D playerViewport = new RenderTexture2D();
    private Camera3D  camera3D = new Camera3D();
    private float axis;

    private Music backgroundMusic = new Music();

    private bool loaded = false;

    public MainGameScene()
    {
        level = new GameMap();
        characterManager = new CharacterManager();
    }
    
    public override void OnEnter()
    {
        Console.WriteLine("Main Scene OnEnter");
        
        backgroundMusic = Raylib.LoadMusicStream("Resources/Audio/MainGameBackgroundMusic.mp3");
        Raylib.SetMusicVolume(backgroundMusic,0.5f);
        
        PlayerController.LoadControllerSouds();
        
        GameRenderer.StartRenderer();
        InventoryRenderer.Init();
        
        mapViewport = Raylib.LoadRenderTexture(250, 250);
        camera2D = new Camera2D();
        camera2D.Offset = (new Vector2(mapViewport.Texture.Width/2,mapViewport.Texture.Height/2));
        camera2D.Zoom = 5.0f;
        camera2D.Rotation = 0.0f;
        
        
        playerViewport = Raylib.LoadRenderTexture(725, 750);
        camera3D = new Camera3D();
        camera3D.FovY = 45.0f;
        camera3D.Projection = CameraProjection.Perspective;
        camera3D.Up =  Vector3.UnitY;
        float angleStart = 0.0f;
        level.LoadMap("Resources/gamemap.json");
        characterManager.AddCharacter(new Player(new Vector2i(1,1),angleStart));
        axis = angleStart;
        characterManager.LoadSounds();
        
        characterManager.AddCharacter(new BlueEnemy(new Vector2i(8,8)));
        characterManager.AddCharacter(new BlueEnemy(new Vector2i(13,23)));
        characterManager.AddCharacter(new BlueEnemy(new Vector2i(22,22)));
        characterManager.AddCharacter(new BlueEnemy(new Vector2i(28,15)));
        characterManager.AddCharacter(new BlueEnemy(new Vector2i(9,25)));
        characterManager.AddCharacter(new BlueEnemy(new Vector2i(32,33)));
        
        characterManager.AddCharacter(new GreenEnemy(new Vector2i(12,13)));
        characterManager.AddCharacter(new GreenEnemy(new Vector2i(20,18)));
        characterManager.AddCharacter(new GreenEnemy(new Vector2i(6,9)));
        characterManager.AddCharacter(new GreenEnemy(new Vector2i(4,16)));
        characterManager.AddCharacter(new GreenEnemy(new Vector2i(13,23)));
        characterManager.AddCharacter(new GreenEnemy(new Vector2i(27,13)));
        
        
        characterManager.AddCharacter(new RedEnemy(new Vector2i(33,33)));
        characterManager.AddCharacter(new RedEnemy(new Vector2i(1,33)));
        characterManager.AddCharacter(new RedEnemy(new Vector2i(11,9)));
        characterManager.AddCharacter(new RedEnemy(new Vector2i(15,13)));
        characterManager.AddCharacter(new RedEnemy(new Vector2i(21,20)));
        characterManager.AddCharacter(new RedEnemy(new Vector2i(8,25)));
        
        Raylib.PlayMusicStream(backgroundMusic);
        loaded = true;
    }

    public override void OnExit()
    {
        Console.WriteLine("Main Scene OnExit");
        if (loaded)
        {
            InventoryRenderer.End();
            GameRenderer.EndRenderer();

            Raylib.UnloadRenderTexture(playerViewport);
            Raylib.UnloadRenderTexture(mapViewport);

            Raylib.StopMusicStream(backgroundMusic);
            Raylib.UnloadMusicStream(backgroundMusic);

            PlayerController.UnloadControllerSounds();

            characterManager.DeleteAllCharacters();
            characterManager.UnloadSounds();
            Console.WriteLine("Main Scene Finished unloaded resources");
        }

        loaded = false;
    }

    public override void Update(float deltaTime)
    {
        if (characterManager.CheckPlayerWin(level))
        {
            SceneManager.TriggerChange("win");
            return;
        }
        Raylib.UpdateMusicStream(backgroundMusic);
        characterManager.checkPlayerDead();
        if (!Raylib.IsMusicStreamPlaying(backgroundMusic))
        {
            Raylib.StopMusicStream(backgroundMusic);
            Raylib.PlayMusicStream(backgroundMusic);
        }

        characterManager.DeleteDefetaedEnemies();
        PlayerController.ProcessInput(characterManager,level);
        MovementSystem.UpdateCharactersPositions(characterManager.GetCharacters(), deltaTime);
        axis = Global.LerpAngle(axis, characterManager.GetPlayer().angle, deltaTime * 10.0f);
    }

    public override void Draw()
    {
        Global.DrawBackground();
        DrawUIWorld3D();
        DrawUIMap();
        InventoryRenderer.RenderInventory(characterManager.GetPlayer().weapons,750,270);
        InventoryRenderer.renderHealCoolDown(characterManager.GetPlayer().getCooldownComponent(),750+84,270);
        Raylib.DrawText("Health:",750,380-26,26,Color.Yellow);
        GameRenderer.RenderHealthBar(characterManager.GetPlayer(),750,380,250,50);
        if( characterManager.GetPlayer().getEnemyInFront(characterManager.GetCharacters()) != null)
        {
            Raylib.DrawText("Enemy Health:",750,620-26,26,Color.Yellow);
            GameRenderer.RenderHealthBar(characterManager.GetPlayer().getEnemyInFront(characterManager.GetCharacters()) as Character, 750,620,250,50);
        }
        Raylib.DrawText("FIND THE EXIT...",750,730,26,Color.Yellow);
        
    }
    

    private void DrawUIMap()
    {
        camera2D.Target = characterManager.GetPlayer().worldPosition;
        Raylib.BeginTextureMode(mapViewport);
        Raylib.ClearBackground(Color.Black);
        Raylib.BeginMode2D(camera2D);
        GameRenderer.RenderWorld2D(level,characterManager,characterManager.GetPlayer().cellPosition);
        Raylib.EndMode2D();
        Raylib.EndTextureMode();
        Vector2 screenPos = new Vector2(750, 10);
        Vector2 size = new Vector2(mapViewport.Texture.Width,-mapViewport.Texture.Height);
        Raylib.DrawTextureRec(mapViewport.Texture,new Rectangle(Raymath.Vector2Zero(),size),screenPos,Color.White);
        Raylib.DrawRectangleLinesEx(new Rectangle(750,10,mapViewport.Texture.Width,mapViewport.Texture.Height),2.0f,Color.White);
    }

    private void DrawUIWorld3D()
    {
        Vector2i playerDir = characterManager.GetPlayer().GetForwardDirection();
        Vector2 playerWorldPosition = characterManager.GetPlayer().worldPosition;
        Vector3 forward = Raymath.Vector3RotateByAxisAngle(new Vector3(1,0,0),new Vector3(0,-1,0),axis*Raylib.DEG2RAD);
        camera3D.Position = new Vector3(playerWorldPosition.X,Global.GRIDSCALE/2,playerWorldPosition.Y);
        camera3D.Target = camera3D.Position + forward;
        Raylib.BeginTextureMode(playerViewport);
        Raylib.ClearBackground(Color.Black);
        Raylib.BeginMode3D(camera3D);
        GameRenderer.Render3DWorld(camera3D,level,characterManager.GetEnemies(),characterManager.GetPlayer().cellPosition);
        Raylib.EndMode3D();
        Raylib.EndTextureMode();
        Vector2 screenPos = new Vector2(10, 10);
        Vector2 size = new Vector2(playerViewport.Texture.Width,-playerViewport.Texture.Height);
        Raylib.DrawTextureRec(playerViewport.Texture,new Rectangle(Raymath.Vector2Zero(),size),screenPos,Color.White);
        Raylib.DrawRectangleLinesEx(new Rectangle(10,10,playerViewport.Texture.Width,playerViewport.Texture.Height),2.0f,Color.White);
    }
}