using System.Net;
using DungeonCrawlerJam2026.GameComponents;
using DungeonCrawlerJam2026.Utilties;

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
        worldPosition = cellPosition.CellToWorld();
    }
    public override void Exit()
    {
    }
}