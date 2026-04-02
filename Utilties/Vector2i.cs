using System.Diagnostics.CodeAnalysis;
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

    public Vector2i(Vector2 vector)
    {
        this.X = (int)vector.X;
        this.Y = (int)vector.Y;
    }

    public Vector2i negative()
    {
        return new Vector2i(-X, -Y);
    }
    
    public Vector2 CellToWorld()
    {
        return new Vector2(X * Global.GRIDSCALE + Global.GRIDSCALE/2.0f, Y * Global.GRIDSCALE + Global.GRIDSCALE/2.0f);
    }

    public Vector2i Add(Vector2i vector)
    {
        return new Vector2i(X + vector.X, Y + vector.Y);
    }

    public float distanceFrom(Vector2i vector)
    {
        int dx = X - vector.X;
        int dy = Y - vector.Y;
        return (float)Math.Sqrt(dx * dx + dy * dy);
    }

    public override bool Equals(Object obj)
    {
        if (obj == null)
            return false;
        if (!(obj is Vector2i))
            return false;
        Vector2i other = (Vector2i)obj;
        return X == other.X && Y == other.Y;
    }
    
    
    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}