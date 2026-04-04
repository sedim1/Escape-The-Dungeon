namespace DungeonCrawlerJam2026.GameComponents;

public class CooldownComponent
{
    private  int  waitTime;
    private  int current;

    public CooldownComponent(int x)
    {
        waitTime = x;
        current = 0;
    }

    public void Update()
    {
        current += 1;
    }

    public void restart()
    {
        current = 0;
    }

    public bool hasFinsished()
    {
        if (current < waitTime )
        {
            return false;
        }
        return true;
    }

    public override string ToString()
    {
        return $"WaitTime:{waitTime}\n Cooldown: {current}";
    }
}