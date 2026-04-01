using DungeonCrawlerJam2026.Characters;
using DungeonCrawlerJam2026.Utilties;
using Raylib_cs;
namespace DungeonCrawlerJam2026.GameComponents;

public static class GameRenderer
{
    public static void DrawCharactersOnMinipap(List<Character> characters)
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
    public static void Render3DWorld(Game map,List<Character> characters)
    {
        
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
}