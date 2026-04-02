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
    public Sword(TypeComponent typeComponent)
    {
        attackComponent = new AttackComponent(typeComponent, 5, 1);
    }
}

public class Whip : Weapon
{
    public Whip(TypeComponent typeComponent)
    {
        attackComponent = new AttackComponent(typeComponent, 5, 1);
    }
}

public class Bow : Weapon
{
    public Bow(TypeComponent typeComponent)
    {
        attackComponent = new AttackComponent(typeComponent, 5, 5);
    }
}
