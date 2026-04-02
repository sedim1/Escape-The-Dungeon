using System.Numerics;
using DungeonCrawlerJam2026.Characters;
using DungeonCrawlerJam2026.Utilties;
using  Raylib_cs;
namespace DungeonCrawlerJam2026.GameComponents;

public static class GameRenderer
{
    private static Texture2D floorTex;
    private static Texture2D wallTex;


    public static void StartRenderer()
    {
        string floorPath = "Resources/Sprites/Map/floor.png";
        string wallPath = "Resources/Sprites/Map/wall.png";
        floorTex = Raylib.LoadTexture(floorPath);
        wallTex = Raylib.LoadTexture(wallPath);
    }

    public static void EndRenderer()
    {
        Raylib.UnloadTexture(floorTex);
        Raylib.UnloadTexture(wallTex);
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
                if (map.GetMap()[y,x] > 0)
                {
                    Raylib.DrawRectangle(posX, posY, Global.GRIDSCALE, Global.GRIDSCALE, Color.White);
                    Raylib.DrawRectangleLines(posX, posY, Global.GRIDSCALE, Global.GRIDSCALE, Color.Gray);
                }
                else
                    Raylib.DrawRectangleLines(posX,posY,Global.GRIDSCALE,Global.GRIDSCALE,Color.Gray);
            }
        }
        DrawCharactersOnMinipap(characters);
    }
    public static void Render3DWorld(GameMap map,List<Character> characters)
    {
        Raylib.DrawGrid(100,Global.GRIDSCALE);
        for (int y = 0; y < map.GetHeight(); y++)
        {
            for (int x = 0; x < map.GetWidth(); x++)
            {
                float posX = x * Global.GRIDSCALE;
                float posZ = y * Global.GRIDSCALE;
                if (map.GetMap()[y, x] == 0)
                {
                    //Raylib.DrawPlane(new Vector3(posX,0,posZ),new Vector2(Global.GRIDSCALE,Global.GRIDSCALE),Color.DarkGray);
                    DrawTexturePlaneTile(wallTex,new Vector3(posX,0.0f,posZ),Vector3.UnitY,Color.White);
                    posX += Global.GRIDSCALE/2;
                    posZ += Global.GRIDSCALE / 2;
                    DrawCubeTexture(floorTex, new Vector3(posX,Global.GRIDSCALE + Global.GRIDSCALE/2,posZ),Global.GRIDSCALE,Global.GRIDSCALE,Global.GRIDSCALE,Color.White);
                }
                else
                {
                    posX += Global.GRIDSCALE/2;
                    posZ += Global.GRIDSCALE / 2;
                    DrawCubeTexture(floorTex, new Vector3(posX,Global.GRIDSCALE/2,posZ),Global.GRIDSCALE,Global.GRIDSCALE,Global.GRIDSCALE,Color.White);
                }
            }
        }
        
        RenderCharactersIn3DWorld(characters);
    }

    private static void RenderCharactersIn3DWorld(List<Character> characters)
    {
        foreach (Character character in characters)
        {
            if (character is Player)
                continue;
            Vector3 position = new Vector3(character.worldPosition.X,Global.GRIDSCALE/2,character.worldPosition.Y);
            Raylib.DrawSphere(position,Global.GRIDSCALE/2/2,Color.Red);
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
        //rlPopMatrix();
        Rlgl.SetTexture(0);
    }
}