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

    public void Attack(Character target)
    {
        int baseDamage = damage;
        Console.WriteLine("Attacking on: " + target.ToString());
        Console.WriteLine("Before: "+target.getHealthComponent().getHealth().ToString());
        //Make damage to the arget
        target.getHealthComponent().decrease(baseDamage);
        Console.WriteLine("After: " + target.getHealthComponent().getHealth().ToString());
    }
    
    public bool isInRange(Character src, Character target)
    {
        bool flag = false;
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
}