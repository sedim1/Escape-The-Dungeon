using System.Numerics;
using Raylib_cs;
using DungeonCrawlerJam2026.Utilties;

namespace DungeonCrawlerJam2026.GameComponents;

public abstract class Character
{
    public Vector2i cellPosition;
    public Vector2 worldPosition;
    public float angle;
    public abstract void Enter();
    public abstract void Exit();

    private Vector2i GetForwardDirection()
    {
        Vector2 forward = Raymath.Vector2Rotate(new Vector2(1, 0), angle * Raylib.DEG2RAD);
        return new Vector2i(forward);
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
        Vector2i right = new Vector2i(forward.Y, -forward.X);
        cellPosition.X += right.X;
        cellPosition.Y += right.Y;
    }

    public void moveLeft()
    {
        Vector2i forward = GetForwardDirection();
        Vector2i left = new Vector2i(-forward.Y, forward.X);
        cellPosition.X += left.X;
        cellPosition.Y += left.Y;
    }

}