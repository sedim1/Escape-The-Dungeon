using System.Numerics;

namespace DungeonCrawlerJam2026.Utilties;

public struct Vector2i
{
    public int X;
    public int Y;
    
    public Vector2i(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
    
    public Vector2 CellToWorld()
    {
        return new Vector2(X * Global.GRIDSCALE + Global.GRIDSCALE/2.0f, Y * Global.GRIDSCALE + Global.GRIDSCALE/2.0f);
    }
    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}