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
    private float t = 0.0f;
    
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
    
    private Vector2i GetForwardDirection()
    {
        Vector2 forward = Raymath.Vector2Rotate(new Vector2(1, 0), angle * Raylib.DEG2RAD);
        return new Vector2i(forward);
    }

    public void TriggerMovement()
    {
        isMoving = true;
        t = 0.0f;
    }

    public Vector2 GetDirection()
    {
        return Raymath.Vector2Rotate(new Vector2(1, 0), angle * Raylib.DEG2RAD);
    }
    public void moveForward()
    {
        Vector2i forward = GetForwardDirection();
        cellPosition.X += forward.X;
        cellPosition.Y += forward.Y;
    }

    public void moveBackward()
    {
        Vector2i backward = GetForwardDirection();
        cellPosition.X -= backward.X;
        cellPosition.Y -= backward.Y;
    }

    public void moveRight()
    {
        Vector2i forward = GetForwardDirection();
        Vector2i right = new Vector2i(-forward.Y, forward.X);
        cellPosition.X += right.X;
        cellPosition.Y += right.Y;
    }

    public void moveLeft()
    {
        Vector2i forward = GetForwardDirection();
        Vector2i left = new Vector2i(forward.Y, -forward.X);
        cellPosition.X += left.X;
        cellPosition.Y += left.Y;
    }

    public void turnRight()
    {
        angle = Raymath.Wrap(angle+90, 0, 360);
    }

    public void turnLeft()
    {
        angle = Raymath.Wrap(angle-90, 0, 360);
    }
}