using DungeonCrawlerJam2026.Utilties;
using Raylib_cs;
namespace DungeonCrawlerJam2026.GameComponents;

public static class GameRenderer
{
    public static void DrawCharactersOnMinipap(List<Character> characters)
    {
        foreach (Character character in characters)
        {
            Raylib.DrawCircleV(character.worldPosition,Global.GRIDSCALE/2/2,Color.Red);
            Raylib.DrawLineEx(character.worldPosition,character.worldPosition+Raymath.Vector2Scale(character.GetDirection(),Global.GRIDSCALE/2),2,Color.Red);
        }
    }
}