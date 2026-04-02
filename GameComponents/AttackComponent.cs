using DungeonCrawlerJam2026.Characters;
using DungeonCrawlerJam2026.Utilties;

namespace DungeonCrawlerJam2026.GameComponents;

public class AttackComponent
{
    public TypeComponent typeComponent;
    private int damage;
    private int range;

    public AttackComponent(TypeComponent typeComponent, int baseDamage, int range)
    {
        this.typeComponent = typeComponent;
        this.damage = baseDamage;
        this.range = range;
    }

    public void setTypeComponent(TypeComponent typeComponent)
    {
        this.typeComponent = typeComponent;
    }

    public void Attack(Character target)
    {
        int baseDamage = damage;
        //Make damage to the arget
        if (typeComponent.isStrongAgainst(target.GetTypeComonent()))
        {
            Console.WriteLine("Its super effecive!!");
            baseDamage = damage * 2;
        }
        else if (typeComponent.isWeakAgainst(target.GetTypeComonent()))
        {
            Console.WriteLine("Its not very effective....");
            baseDamage = damage / 2;
        }

        target.getHealthComponent().decrease(baseDamage);
        if (!(target is Player))
        {
            Console.WriteLine("Attacking on: " + target.ToString());
            Console.WriteLine("Health remaining: " + target.getHealthComponent().getHealth().ToString());
        }
    }

    private bool playerInRange(Character src, Character target)
    {bool flag = false;
        Vector2i[] directions = new Vector2i[]
        {
            new Vector2i(1,0),
            new Vector2i(0,1),
            new Vector2i(-1,0),
            new Vector2i(0,-1),
        };
        Vector2i startPos = src.cellPosition;

        for (int i = 1; i <= range; i++)
        {
            if (flag)
                break;
            foreach (Vector2i dir in directions)
            {
                Vector2i currentPos = new Vector2i(startPos.X + (dir.X * range), startPos.Y + (dir.Y * range));
                if (currentPos.Equals(target.cellPosition))
                {
                    flag = true;
                    break;
                }
            }
        }
        return flag;
    }

    private bool enemyIsInRange(Character src, Character target)
    {
        bool flag = false;
        Vector2i direction = (src as Player).GetForwardDirection();
        Vector2i startPos = src.cellPosition;
        for(int i = 1; i <= range; i++)
        {
            Vector2i currentPos = new Vector2i(startPos.X + (direction.X * i), startPos.Y + (direction.Y * i));
            if (currentPos.Equals(target.cellPosition))
            {
                flag = true;
                break;
            }
        }
        return flag;
    }
    
    public bool isInRange(Character src, Character target)
    {
        if (src is Player)
            return enemyIsInRange(src, target);
        else
            return playerInRange(src, target);
    }
}