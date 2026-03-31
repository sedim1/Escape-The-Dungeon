using System.Diagnostics.Tracing;
using System.Numerics;
using Raylib_cs;
using DungeonCrawlerJam2026.Utilties;
using Unglide;

namespace DungeonCrawlerJam2026.GameComponents;


public abstract class Character
{
    public Vector2i cellPosition;
    public Vector2 worldPosition;
    public float angle;
    public bool isMoving = false;
    protected float t = 0.0f;
    
    public abstract void Enter();
    public abstract void Exit();

    public void UpdateWorldPosition(float delta)
    {
        if (!isMoving)
            return;
        float speedRate = 5.0f;
        t += speedRate * delta;
        worldPosition = Raymath.Vector2Lerp(worldPosition, cellPosition.CellToWorld(), t);
        if (t < 1.0f)
            return;
        worldPosition = cellPosition.CellToWorld();
        isMoving = false;
    }
}