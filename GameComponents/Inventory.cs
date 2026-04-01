namespace DungeonCrawlerJam2026.GameComponents;

public class Inventory
{
    private int index = 0;
    private Weapon[] weapons;

    public Inventory()
    {
        index = 0;
        weapons = new Weapon[3] { null, null, null};
    }

    public void ClearInventory()
    {
        weapons = null;
        index = 0;
    }
    public void AddWeapon(Weapon weapon)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] == null)
            {
                weapons[i] = weapon;
                return;
            }
        }
    }
    public void removeWeapon()
    {
        weapons[index] = null;
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] != null)
            {
                index = i;
                break;
            }
        }
    }
    public void changeWeapon(int x)
    {
        if (weapons == null)
        {
            return;
        }
        if (weapons[x] == null || x >= weapons.Length)
            return;
        index = x;
        Console.WriteLine("Changing weapon to: " + weapons[x].ToString());
    }
    public Weapon getCurrentWeapon()
    {
        return weapons[index];
    }
}