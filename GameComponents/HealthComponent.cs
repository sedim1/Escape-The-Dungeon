namespace DungeonCrawlerJam2026.GameComponents;

public class HealthComponent
{
    private int health;
    private int maxHealth;
    public HealthComponent(int health, int maxHealth)
    {
        this.health = health;
        this.maxHealth = maxHealth;
    }

    public void increase(int amount)
    {
        health += amount;
        if (health >= maxHealth)
            health = maxHealth;
    }

    public void decrease(int amount)
    {
        health -= amount;
        if (health <= 0)
            health = 0;
    }

    public bool isDepleted()
    {
        return health <= 0;
    }

    public int getHealth()
    {
        return health;
    }
}