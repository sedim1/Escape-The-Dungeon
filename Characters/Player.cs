using DungeonCrawlerJam2026.GameComponents;
using DungeonCrawlerJam2026.Utilties;
using Raylib_cs;
using Unglide;

namespace DungeonCrawlerJam2026.Characters;

public class Player : Character
{
    
    public Player(Vector2i position, float angle)
    {
        this.cellPosition = position;
        this.angle = angle;
    }
   

    public override void Enter()
    {
        Console.WriteLine("Entering Player");
        worldPosition = cellPosition.CellToWorld();
    }
    public override void Exit()
    {
        Console.WriteLine("Player");
    }
}