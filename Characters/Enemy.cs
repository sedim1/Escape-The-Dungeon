using DungeonCrawlerJam2026.GameComponents;
using DungeonCrawlerJam2026.Utilties;

namespace DungeonCrawlerJam2026.Characters;

public class Enemy : Character
{

    public Enemy(Vector2i position)
    {
        this.cellPosition = position;
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
        bool flagAttack = false;
        if(isMoving)
            return;
        //Attack if possible
        if (flagAttack)
        {
            
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