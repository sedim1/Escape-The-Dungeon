namespace DungeonCrawlerJam2026.GameComponents;


public enum WeaponTag
{
    SWORD,
    WHIP,
    BOW,
}
public class Weapon
{
    public WeaponTag tag;
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
        tag = WeaponTag.SWORD;
        attackComponent = new AttackComponent(typeComponent, 10, 1);
    }
}

public class Whip : Weapon
{
    public Whip(TypeComponent typeComponent)
    {
        tag = WeaponTag.WHIP;
        attackComponent = new AttackComponent(typeComponent, 20, 1);
    }
}

public class Bow : Weapon
{
    public Bow(TypeComponent typeComponent)
    {
        tag = WeaponTag.BOW;
        attackComponent = new AttackComponent(typeComponent, 15, 5);
    }
}
