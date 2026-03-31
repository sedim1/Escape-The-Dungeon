using DungeonCrawlerJam2026.GameComponents;
using DungeonCrawlerJam2026.Utilties;

namespace DungeonCrawlerJam2026.Characters;

public class Enemy : Character
{

    public Enemy(Vector2i position)
    {
        this.cellPosition = position;
    }

    public void SetPosition(Vector2i position)
    {
        this.cellPosition = position;
        isMoving = true;
    }
    
    public override void Enter()
    {
        Console.WriteLine("Entering Enemy");
        this.worldPosition = cellPosition.CellToWorld();
    }

    public override void Exit()
    {
        Console.WriteLine("Exiting enemy");
    }
    
}