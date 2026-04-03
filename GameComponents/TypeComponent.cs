using System.Security;

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
        bool flag = false;
        if (weakAgainst == null)
        {
            Console.WriteLine("weakAgainst null");
            return flag;
        }
        foreach (Types t in weakAgainst)
        {
            if (typeComponent.type == t)
            {
                flag = true;
                break;
            }
        }
        return flag;
    }

    public bool isStrongAgainst(TypeComponent typeComponent)
    {
        bool flag = false;
        if (strongAgainst == null)
        {
            Console.WriteLine("strongAgainst null");
            return flag;
        }
        foreach (Types t in strongAgainst)
        {
            if (t == typeComponent.type)
            {
                flag = true;
                break;
            }
        }
        return flag;
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
        strongAgainst = new List<Types>();
        strongAgainst.Add(Types.GRASS);
        weakAgainst = new List<Types>();
        weakAgainst.Add(Types.WATER);
    }
}

public class GrassTypeComponent : TypeComponent
{
    public GrassTypeComponent()
    {
        type = Types.GRASS;
        strongAgainst = new List<Types>();
        strongAgainst.Add(Types.WATER);
        weakAgainst = new List<Types>();
        weakAgainst.Add(Types.FIRE);
        
    }
}

public class WaterTypeComponent : TypeComponent
{
    public  WaterTypeComponent()
    {
        type = Types.WATER;
        strongAgainst = new List<Types>();
        strongAgainst.Add(Types.FIRE);
        weakAgainst = new List<Types>();
        weakAgainst.Add(Types.GRASS);
    }
}