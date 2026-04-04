using System.Numerics;
using DungeonCrawlerJam2026.GameComponents;
using DungeonCrawlerJam2026.Utilties;
using Raylib_cs;

namespace DungeonCrawlerJam2026.Characters;

public class Player : Character
{
    public Inventory weapons;
    private Sound superEffectiveSound;
    private Sound nonEffectiveSound;
    
    public Player(Vector2i position, float angle)
    {
        this.cellPosition = position;
        this.angle = angle;
        this.healthComponent = new HealthComponent(100, 100);
        this.typeComponent = new DefaultTypeComponent();
        weapons =  new Inventory();
        cooldownComponent = new CooldownComponent(6);
    }
    
    public override void Enter()
    {
        Console.WriteLine("Entering Player");
        worldPosition = cellPosition.CellToWorld();
        weapons.AddWeapon(new Sword(new DefaultTypeComponent()));
        normalHit = Raylib.LoadSound("Resources/Audio/normalHit.wav");
        superEffectiveSound = Raylib.LoadSound("Resources/Audio/superEffectiveHit.wav");
        nonEffectiveSound = Raylib.LoadSound("Resources/Audio/notEffectiveHit.wav");
    }
    
    public override void Exit()
    {
        Console.WriteLine("Exit Player");
        weapons.ClearInventory();
        Raylib.UnloadSound(superEffectiveSound);
        Raylib.UnloadSound(nonEffectiveSound);
        Raylib.UnloadSound(normalHit);
    }  
    public Vector2i GetForwardDirection()
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
        if (weapons.getCurrentWeapon() == null)
        {
            Console.WriteLine("Player has no weapon equipped");
            return;
        }
        foreach (Character character in characters)
        {
            if (character is Player)
                continue;
            if(!weapons.getCurrentWeapon().GetAttackComponent().isInRange(this,character))
                continue;
            Console.WriteLine("Attack succes from player");
            Console.WriteLine(cooldownComponent.ToString());
            cooldownComponent.Update();
            if(weapons.getCurrentWeapon().GetAttackComponent().typeComponent.isStrongAgainst(character.GetTypeComonent()))
                Raylib.PlaySound(superEffectiveSound);
            else if(weapons.getCurrentWeapon().GetAttackComponent().typeComponent.isWeakAgainst(character.GetTypeComonent())) 
                Raylib.PlaySound(nonEffectiveSound);
            else
                Raylib.PlaySound(normalHit);
            weapons.getCurrentWeapon().GetAttackComponent().Attack(character);
            if (character.getHealthComponent().isDepleted())
            {
                weapons.getCurrentWeapon().ChangeAttackType(character.GetTypeComonent());
            }
            break;
        }
    }

    public override string ToString()
    {
        return "Character player";
    }

    public Character getEnemyInFront(List<Character> characters)
    {
        Character enemy = null;
        Vector2i forwardPosition = cellPosition.Add(GetForwardDirection());
        foreach (Character character in characters)
        {
            if (character is Player)
                continue;
            if (character.cellPosition.Equals(forwardPosition))
            {
                enemy = character;
                break;
            }
        }
        return enemy;
    }
}