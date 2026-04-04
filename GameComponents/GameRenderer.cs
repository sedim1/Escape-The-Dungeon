using System.Numerics;
using DungeonCrawlerJam2026.Characters;
using DungeonCrawlerJam2026.Utilties;
using  Raylib_cs;
namespace DungeonCrawlerJam2026.GameComponents;

public static class GameRenderer
{
    private static Texture2D floorTex;
    private static Texture2D wallTex;
    private static Texture2D exitTex;

    private static Model wallModel;
    private static Model floorModel;
    private static Model exitModel;
    
    
    private static Dictionary<EnemyTag, Texture2D> enemySprites;
    private static List<Mesh> mapTiles;

    public static void StartRenderer()
    {
        //Load map sprites
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
    
    private static void DrawCharactersOnMinipap(List<Character> characters)
    {
        foreach (Character character in characters)
        {
            if (character is Enemy)
            {
                Raylib.DrawCircleV(character.worldPosition,Global.GRIDSCALE/2/2,Color.Red);
            }
            else if (character is Player)
            {
                Raylib.DrawCircleV(character.worldPosition,Global.GRIDSCALE/2/2,Color.Blue);
                Raylib.DrawLineEx(character.worldPosition,character.worldPosition+Raymath.Vector2Scale((character as Player).GetDirection(),Global.GRIDSCALE/2),2,Color.Blue);
            }
        }
    }

    public static void RenderWorld2D(GameMap map, List<Character> characters)
    {
        for (int y = 0; y < map.GetHeight(); y++)
        {
            for (int x = 0; x < map.GetWidth(); x++)
            {
                int posX = x * Global.GRIDSCALE;
                int posY = y * Global.GRIDSCALE;

                Rectangle srcRect = new Rectangle(Raymath.Vector2Zero(),new Vector2(floorTex.Width,floorTex.Height));
                Rectangle destRect = new Rectangle(new Vector2(posX,posY),new Vector2(Global.GRIDSCALE,Global.GRIDSCALE));
                
                if (map.GetMap()[y,x] == 1)
                {
                    Raylib.DrawTexturePro(wallTex,srcRect,destRect,Raymath.Vector2Zero(),0.0f,Color.White);
                }
                else if (map.GetMap()[y, x] == 0)
                {
                    Raylib.DrawTexturePro(floorTex,srcRect,destRect,Raymath.Vector2Zero(),0.0f,Color.White);
                }
                else if (map.GetMap()[y,x] == 2)
                {
                    Raylib.DrawTexturePro(exitTex,srcRect,destRect,Raymath.Vector2Zero(),0.0f,Color.White);
                }
            }
        }
        DrawCharactersOnMinipap(characters);
    }
    public static void Render3DWorld(Camera3D camera,GameMap map,List<Character> characters)
    {
        Raylib.DrawGrid(100,Global.GRIDSCALE);
        for (int y = 0; y < map.GetHeight(); y++)
        {
            for (int x = 0; x < map.GetWidth(); x++)
            {
                float posX = x * Global.GRIDSCALE;
                float posZ = y * Global.GRIDSCALE + Global.GRIDSCALE;
                if (map.GetMap()[y, x] == 0)
                {
                    //DrawTexturePlaneTile(floorTex,new Vector3(posX,0.0f,posZ),Vector3.UnitY,Color.White);
                    Raylib.DrawModel(floorModel,new Vector3(posX,0.0f,posZ),1.0f,Color.White);
                    Raylib.DrawModel(wallModel,new Vector3(posX,Global.GRIDSCALE,posZ),1.0f,Color.White);
                }
                else
                {
                    Raylib.DrawModel(wallModel,new Vector3(posX,0.0f,posZ),1.0f,Color.White);
                }
            }
        }
        
        RenderCharactersIn3DWorld(camera,characters);
    }

    private static void RenderCharactersIn3DWorld(Camera3D camera,List<Character> characters)
    {
        foreach (Character character in characters)
        {
            if (character is Player)
                continue;
            Texture2D sprite = enemySprites[(character as Enemy).tag];
            Vector3 position = new Vector3(character.worldPosition.X,Global.GRIDSCALE/2.0f,character.worldPosition.Y);
            Raylib.DrawBillboard(camera,sprite,position,5.0f,Color.White);
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

    private static void DrawTexturePlaneTile(Texture2D texture, Vector3 position, Vector3 normal, Color color)
    {
        Rlgl.SetTexture(texture.Id);
        Rlgl.Begin(DrawMode.Quads);
        Rlgl.Color4ub(color.R, color.G, color.B, color.A);
        Rlgl.Normal3f(normal.X, normal.Y, normal.Z);
        //TopLeft
        Rlgl.TexCoord2f(0.0f,0.0f);
        Rlgl.Vertex3f(position.X, position.Y, position.Z);
        //BottomLeft
        Rlgl.TexCoord2f(0.0f,1.0f);
        Rlgl.Vertex3f(position.X, position.Y, position.Z + Global.GRIDSCALE);
        //BottomRight
        Rlgl.TexCoord2f(1.0f,1.0f);
        Rlgl.Vertex3f(position.X + Global.GRIDSCALE, position.Y, position.Z + Global.GRIDSCALE);
        //TopRight
        Rlgl.TexCoord2f(1.0f,0.0f);
        Rlgl.Vertex3f(position.X + Global.GRIDSCALE, position.Y, position.Z);
        Rlgl.End();
        Rlgl.SetTexture(0);
    }
    
    private static void DrawCubeTexture(
        Texture2D texture,
        Vector3 position,
        float width,
        float height,
        float length,
        Color color
    )
    {
        float x = position.X;
        float y = position.Y;
        float z = position.Z;

        // Set desired texture to be enabled while drawing following vertex data
        Rlgl.SetTexture(texture.Id);
        Rlgl.Begin(DrawMode.Quads);
        Rlgl.Color4ub(color.R, color.G, color.B, color.A);
        // Front Face
        // Normal Pointing Towards Viewer
        Rlgl.Normal3f(0.0f, 0.0f, 1.0f);
        Rlgl.TexCoord2f(0.0f, 0.0f);
        // Bottom Left Of The Texture and Quad
        Rlgl.Vertex3f(x - width / 2, y - height / 2, z + length / 2);
        Rlgl.TexCoord2f(1.0f, 0.0f);
        // Bottom Right Of The Texture and Quad
        Rlgl.Vertex3f(x + width / 2, y - height / 2, z + length / 2);
        Rlgl.TexCoord2f(1.0f, 1.0f);
        // Top Right Of The Texture and Quad
        Rlgl.Vertex3f(x + width / 2, y + height / 2, z + length / 2);
        Rlgl.TexCoord2f(0.0f, 1.0f);
        // Top Left Of The Texture and Quad
        Rlgl.Vertex3f(x - width / 2, y + height / 2, z + length / 2);
        // Back Face
        // Normal Pointing Away From Viewer
        Rlgl.Normal3f(0.0f, 0.0f, -1.0f);
        Rlgl.TexCoord2f(1.0f, 0.0f);
        // Bottom Right Of The Texture and Quad
        Rlgl.Vertex3f(x - width / 2, y - height / 2, z - length / 2);
        Rlgl.TexCoord2f(1.0f, 1.0f);
        // Top Right Of The Texture and Quad
        Rlgl.Vertex3f(x - width / 2, y + height / 2, z - length / 2);
        Rlgl.TexCoord2f(0.0f, 1.0f);
        // Top Left Of The Texture and Quad
        Rlgl.Vertex3f(x + width / 2, y + height / 2, z - length / 2);
        Rlgl.TexCoord2f(0.0f, 0.0f);
        // Bottom Left Of The Texture and Quad
        Rlgl.Vertex3f(x + width / 2, y - height / 2, z - length / 2);
        // Top Face
        // Normal Pointing Up
        Rlgl.Normal3f(0.0f, 1.0f, 0.0f);
        Rlgl.TexCoord2f(0.0f, 1.0f);
        // Top Left Of The Texture and Quad
        Rlgl.Vertex3f(x - width / 2, y + height / 2, z - length / 2);
        Rlgl.TexCoord2f(0.0f, 0.0f);
        // Bottom Left Of The Texture and Quad
        Rlgl.Vertex3f(x - width / 2, y + height / 2, z + length / 2);
        Rlgl.TexCoord2f(1.0f, 0.0f);
        // Bottom Right Of The Texture and Quad
        Rlgl.Vertex3f(x + width / 2, y + height / 2, z + length / 2);
        Rlgl.TexCoord2f(1.0f, 1.0f);
        // Top Right Of The Texture and Quad
        Rlgl.Vertex3f(x + width / 2, y + height / 2, z - length / 2);
        // Bottom Face
        // Normal Pointing Down
        Rlgl.Normal3f(0.0f, -1.0f, 0.0f);
        Rlgl.TexCoord2f(1.0f, 1.0f);
        // Top Right Of The Texture and Quad
        Rlgl.Vertex3f(x - width / 2, y - height / 2, z - length / 2);
        Rlgl.TexCoord2f(0.0f, 1.0f);
        // Top Left Of The Texture and Quad
        Rlgl.Vertex3f(x + width / 2, y - height / 2, z - length / 2);
        Rlgl.TexCoord2f(0.0f, 0.0f);
        // Bottom Left Of The Texture and Quad
        Rlgl.Vertex3f(x + width / 2, y - height / 2, z + length / 2);
        Rlgl.TexCoord2f(1.0f, 0.0f);
        // Bottom Right Of The Texture and Quad
        Rlgl.Vertex3f(x - width / 2, y - height / 2, z + length / 2);
        // Right face
        // Normal Pointing Right
        Rlgl.Normal3f(1.0f, 0.0f, 0.0f);
        Rlgl.TexCoord2f(1.0f, 0.0f);
        // Bottom Right Of The Texture and Quad
        Rlgl.Vertex3f(x + width / 2, y - height / 2, z - length / 2);
        Rlgl.TexCoord2f(1.0f, 1.0f);
        // Top Right Of The Texture and Quad
        Rlgl.Vertex3f(x + width / 2, y + height / 2, z - length / 2);
        Rlgl.TexCoord2f(0.0f, 1.0f);
        // Top Left Of The Texture and Quad
        Rlgl.Vertex3f(x + width / 2, y + height / 2, z + length / 2);
        Rlgl.TexCoord2f(0.0f, 0.0f);
        // Bottom Left Of The Texture and Quad
        Rlgl.Vertex3f(x + width / 2, y - height / 2, z + length / 2);
        // Left Face
        // Normal Pointing Left
        Rlgl.Normal3f(-1.0f, 0.0f, 0.0f);
        Rlgl.TexCoord2f(0.0f, 0.0f);
        // Bottom Left Of The Texture and Quad
        Rlgl.Vertex3f(x - width / 2, y - height / 2, z - length / 2);
        Rlgl.TexCoord2f(1.0f, 0.0f);
        // Bottom Right Of The Texture and Quad
        Rlgl.Vertex3f(x - width / 2, y - height / 2, z + length / 2);
        Rlgl.TexCoord2f(1.0f, 1.0f);
        // Top Right Of The Texture and Quad
        Rlgl.Vertex3f(x - width / 2, y + height / 2, z + length / 2);
        Rlgl.TexCoord2f(0.0f, 1.0f);
        // Top Left Of The Texture and Quad
        Rlgl.Vertex3f(x - width / 2, y + height / 2, z - length / 2);
        Rlgl.End();
        Rlgl.SetTexture(0);
    }
}