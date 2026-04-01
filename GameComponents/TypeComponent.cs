namespace DungeonCrawlerJam2026.GameComponents;


public enum Types
{
    NONE,
    FIRE,
    WATER,
    GRASS,
}

public abstract class TypeComponent
{
    public Types type;
    public List<Types> strongAgainst;
    public List<Types> weakAgainst;

    public bool isWeakAgainst(TypeComponent typeComponent)
    {
        return weakAgainst.Contains(typeComponent.type);
    }

    public bool isStrongAgainst(TypeComponent typeComponent)
    {
        return strongAgainst.Contains(typeComponent.type);
    }
}

public class DefaultTypeComponent : TypeComponent
{
    public DefaultTypeComponent()
    {
        type = Types.NONE;
        weakAgainst = new List<Types>();
        strongAgainst = new List<Types>();
    }
}

public class FireTypeComponent : TypeComponent
{
    public FireTypeComponent()
    {
        type = Types.FIRE;
        strongAgainst = new List<Types>() { Types.GRASS };
        weakAgainst = new List<Types>() { Types.WATER };
    }
}

public class GrassTypeComponent : TypeComponent
{
    public GrassTypeComponent()
    {
        type = Types.GRASS;
        strongAgainst = new List<Types>() { Types.WATER };
        weakAgainst = new List<Types>() { Types.FIRE };
        
    }
}

public class WaterTypeComponent : TypeComponent
{
    public  WaterTypeComponent()
    {
        type = Types.WATER;
        strongAgainst = new List<Types>() { Types.FIRE };
        weakAgainst = new List<Types>() { Types.GRASS };
    }
}