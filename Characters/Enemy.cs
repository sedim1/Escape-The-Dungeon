using DungeonCrawlerJam2026.GameComponents;
using DungeonCrawlerJam2026.Utilties;

namespace DungeonCrawlerJam2026.Characters;

public enum EnemyTag
{
    BLUE,
    RED,
    GREEN,
    NONE,
};


public class Enemy : Character
{
    public EnemyTag tag;
    public AttackComponent attackComponent;
  
    public override void Enter()
    {
        Console.WriteLine("Entering Enemy");
        this.worldPosition = cellPosition.CellToWorld();
    }
    public override void Exit()
    {
        Console.WriteLine("Exiting enemy");
    }
    public void ProcessAction(GameMap map,List<Character> characters,Player player)
    {///AFK
        cooldownComponent.Update();
        if (!cooldownComponent.hasFinsished())
        {
            return;
        }
        cooldownComponent.restart();
        bool flagAttack = attackComponent.isInRange(this, player);
        //Attack if possible
        if (flagAttack)
        {
            attackComponent.Attack(player);
        }
        else //Follo player if alerted
        {
            Vector2i nextPosition = AStarSeach.GetNextStep(map, characters, this, player);
            if (nextPosition.Equals(new Vector2i(0, 0)) || nextPosition.Equals(player.cellPosition))
                return;
            t = 0.0f;
            isMoving = true;
            cellPosition = nextPosition;
        }
    }
}

public class BlueEnemy : Enemy
{
    public BlueEnemy(Vector2i position)
    { tag = EnemyTag.BLUE;
        this.cellPosition = position;
        attackComponent = new AttackComponent(new WaterTypeComponent(), 5, 1);
        typeComponent = new WaterTypeComponent();
        this.healthComponent = new HealthComponent(100, 100);
        cooldownComponent = new CooldownComponent(1);
    }
}

public class RedEnemy : Enemy
{
    public RedEnemy(Vector2i position)
    { tag = EnemyTag.RED;
        this.cellPosition = position;
        attackComponent = new AttackComponent(new FireTypeComponent(), 15, 1);
        cooldownComponent = new CooldownComponent(3);
        typeComponent = new FireTypeComponent();
        this.healthComponent = new HealthComponent(100, 100);
    }
}


public class GreenEnemy : Enemy
{
    public GreenEnemy(Vector2i position)
    { tag = EnemyTag.GREEN;
        this.cellPosition = position;
        attackComponent = new AttackComponent(new GrassTypeComponent(), 10, 1);
        typeComponent = new GrassTypeComponent();
        cooldownComponent = new CooldownComponent(2);
        this.healthComponent = new HealthComponent(100, 100);
    }
}
