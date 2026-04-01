namespace DungeonCrawlerJam2026.GameComponents;

public class Weapon
{
    protected AttackComponent attackComponent;

    public AttackComponent GetAttackComponent()
    {
        return attackComponent;
    }
}

public class Sword : Weapon
{
    public Sword()
    {
        attackComponent = new AttackComponent(new DefaultTypeComponent(), 5, 1);
    }
}