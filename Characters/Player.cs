using System.Numerics;
using DungeonCrawlerJam2026.GameComponents;
using DungeonCrawlerJam2026.Utilties;
using Raylib_cs;

namespace DungeonCrawlerJam2026.Characters;

public class Player : Character
{
    private Weapon currentWeapon;
    public Player(Vector2i position, float angle)
    {
        this.cellPosition = position;
        this.angle = angle;
        this.healthComponent = new HealthComponent(100, 100);
        currentWeapon = new Sword();
    }
    
    public override void Enter()
    {
        Console.WriteLine("Entering Player");
        worldPosition = cellPosition.CellToWorld();
    }
    public override void Exit()
    {
        Console.WriteLine("Player");
    }  public Vector2i GetForwardDirection()
    {
        Vector2 forward = Raymath.Vector2Rotate(new Vector2(1, 0), angle * Raylib.DEG2RAD);
        return new Vector2i(forward);
    }

    public Vector2i GetLeftDirection()
    {
        Vector2i forward = GetForwardDirection();
        Vector2i left = new Vector2i(forward.Y, -forward.X);
        return left;
    }

    public Vector2i GetRightDirection()
    {
        
        Vector2i forward = GetForwardDirection();
        Vector2i right = new Vector2i(-forward.Y, forward.X);
        return right;
    }

    public Vector2i GetBackDirection()
    {
        Vector2i forward = GetForwardDirection();
        return forward.negative();
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

    public void TriggerAttack(List<Character> characters)
    {
        foreach (Character character in characters)
        {
            if (character is Player)
                continue;
            if(!currentWeapon.GetAttackComponent().isInRange(this,character))
                continue;
            currentWeapon.GetAttackComponent().Attack(character);
        }
    }

    public override string ToString()
    {
        return "Character player";
    }
}