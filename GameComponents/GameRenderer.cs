using System.Numerics;
using DungeonCrawlerJam2026.Characters;
using DungeonCrawlerJam2026.Utilties;
using  Raylib_cs;
namespace DungeonCrawlerJam2026.GameComponents;

public static class GameRenderer
{
    private static Texture2D floorTex = new Texture2D();
    private static Texture2D wallTex = new Texture2D();
    private static Texture2D exitTex = new Texture2D();

    private static Model wallModel = new Model();
    private static Model floorModel = new Model();
    private static Model exitModel = new Model();

    private static Dictionary<EnemyTag, Texture2D> enemySprites = new Dictionary<EnemyTag, Texture2D>();
    private static List<Mesh> mapTiles;


    public  static void StartRenderer()
    {
        //Load map assets
        string floorPath = "Resources/Sprites/Map/floor";
        string wallPath = "Resources/Sprites/Map/wall1";
        string exitTexPath = "Resources/Sprites/Map/wall2";
        floorTex = Raylib.LoadTexture(floorPath+".png");
        wallTex = Raylib.LoadTexture(wallPath+".png");
        exitTex = Raylib.LoadTexture(exitTexPath+".png");
        wallModel = Raylib.LoadModel(wallPath + ".glb");
        floorModel = Raylib.LoadModel(floorPath + ".glb");
        exitModel = Raylib.LoadModel(exitTexPath + ".glb");
        //Load enemysprites
        enemySprites = new Dictionary<EnemyTag, Texture2D>();
        enemySprites.Add(EnemyTag.BLUE,Raylib.LoadTexture("Resources/Sprites/Enemies/Blue.png"));
        enemySprites.Add(EnemyTag.GREEN,Raylib.LoadTexture("Resources/Sprites/Enemies/Green.png"));
        enemySprites.Add(EnemyTag.RED,Raylib.LoadTexture("Resources/Sprites/Enemies/Red.png"));
        
    }

    public static void EndRenderer()
    {
        //Unload map sprites
        Raylib.UnloadTexture(floorTex);
        Raylib.UnloadTexture(wallTex);
        Raylib.UnloadTexture(exitTex);
        Raylib.UnloadModel(floorModel);
        Raylib.UnloadModel(wallModel);
        Raylib.UnloadModel(exitModel);
        //Unload enemy sprites
            foreach (var element in enemySprites)
            {
                Raylib.UnloadTexture(element.Value);
            }

            enemySprites.Clear();
    }
    
    private static void DrawCharactersOnMinipap(CharacterManager characterManager,Vector2i playerPos)
    {
        Raylib.DrawCircleV(characterManager.GetPlayer().worldPosition,Global.GRIDSCALE/2/2,Color.DarkBlue);
        foreach (Character character in characterManager.GetEnemies())
        {
            if(playerPos.distanceFrom(character.cellPosition) > 2.5f)
                continue;
            Raylib.DrawCircleV(character.worldPosition,Global.GRIDSCALE/2/2,Color.DarkPurple);
        }
    }

    public static void RenderWorld2D(GameMap map, CharacterManager characterManager,Vector2i playerPos)
    {
        Rectangle srcRect = new Rectangle(Raymath.Vector2Zero(),new Vector2(floorTex.Width,floorTex.Height));

        for (int y = playerPos.Y - 2; y <= playerPos.Y + 2; y++)
        {
            for (int x = playerPos.X - 2; x <= playerPos.X + 2; x++)
            {
                if (MovementSystem.OutOfBounds(map, new Vector2i(x, y)))
                    continue;
                Rectangle destRect = new Rectangle(x * Global.GRIDSCALE, y * Global.GRIDSCALE, Global.GRIDSCALE,
                    Global.GRIDSCALE);
                if (map.GetMap()[y, x] == 0)
                {
                    Raylib.DrawTexturePro(floorTex,srcRect,destRect,Vector2.Zero,0.0f,Color.White);
                }
                if (map.GetMap()[y, x] == 1)
                {
                    Raylib.DrawTexturePro(wallTex,srcRect,destRect,Vector2.Zero,0.0f,Color.White);
                }
                if (map.GetMap()[y, x] == 2)
                {
                    Raylib.DrawTexturePro(exitTex,srcRect,destRect,Vector2.Zero,0.0f,Color.White);
                }
            }
        }
        
        DrawCharactersOnMinipap(characterManager,playerPos);
    }
    

    public static void Render3DWorld(Camera3D camera, GameMap map, List<Character> characters,Vector2i playerPos)
    {
        for (int y = 0; y < map.GetHeight(); y++)
        {
            for (int x = 0; x < map.GetWidth(); x++)
            {
                float posX = x * Global.GRIDSCALE;
                float posZ = y * Global.GRIDSCALE + Global.GRIDSCALE;
                Vector2i tilePos = new Vector2i(x,y);
                Color c = Global.calculateFogColor(playerPos.distanceFrom(tilePos));
                if (map.GetMap()[y, x] == 0)
                {
                    //DrawTexturePlaneTile(floorTex,new Vector3(posX,0.0f,posZ),Vector3.UnitY,Color.White);
                    Raylib.DrawModel(floorModel,new Vector3(posX,0.0f,posZ),1.0f,c);
                    Raylib.DrawModel(wallModel,new Vector3(posX,Global.GRIDSCALE,posZ),1.0f,c);
                }
                else if (map.GetMap()[y, x] == 2)
                {
                    Raylib.DrawModel(exitModel,new Vector3(posX,0.0f,posZ),1.0f,c);
                }
                else
                {
                    Raylib.DrawModel(wallModel,new Vector3(posX,0.0f,posZ),1.0f,c);
                }
            }
        }
        RenderCharactersIn3DWorld(camera,characters,playerPos);
    }

    private static void RenderCharactersIn3DWorld(Camera3D camera,List<Character> characters,Vector2i playerPos)
    {
        foreach (Character character in characters)
        {
            if (character is Player)
                continue;
            Texture2D sprite = enemySprites[(character as Enemy).tag];
            Vector2i enemyTilePos = character.cellPosition;
            Color c = Global.calculateFogColor(playerPos.distanceFrom(enemyTilePos));
            Vector3 position = new Vector3(character.worldPosition.X,Global.GRIDSCALE/2.0f,character.worldPosition.Y);
            Raylib.DrawBillboard(camera,sprite,position,5.0f,c);
        }
    }
    
    public static void DebugEnemyPathFinding(GameMap map, CharacterManager manager)
    {
        Player player = manager.GetPlayer();
        if (player == null)
            return;
        foreach (Character character in manager.GetCharacters())
        {
            if (Object.ReferenceEquals(player, character))
                continue;
            AStarSeach.DebugPath(map,manager.GetCharacters(),character,player);
        }
    }

    public static void RenderHealthBar(Character character, int x, int y, int w, int h)
    {
        int green_w = (character.getHealthComponent().getHealth() * w)/character.getHealthComponent().getMaxHealth();
        Raylib.DrawRectangle(x,y,w,h,Color.Red);
        Raylib.DrawRectangle(x,y,green_w,h,Color.Green);
        Raylib.DrawRectangleLinesEx(new Rectangle(x,y,w,h),3.0f,Color.White);
    }
}