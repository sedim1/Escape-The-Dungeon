using DungeonCrawlerJam2026.GameComponents;
using DungeonCrawlerJam2026.Utilties;

namespace DungeonCrawlerJam2026.Characters;

public class Enemy : Character
{
    public AttackComponent attackComponent;
    public Enemy(Vector2i position)
    {
        this.cellPosition = position;
        attackComponent = new AttackComponent(new DefaultTypeComponent(), 5, 1);
        typeComponent = new DefaultTypeComponent();
        this.healthComponent = new HealthComponent(100, 100);
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

    public void ProcessAction(GameMap map,List<Character> characters,Player player)
    {
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