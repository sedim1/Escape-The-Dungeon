using System.Numerics;
using Raylib_cs;

namespace DungeonCrawlerJam2026.GameComponents;

public static class InventoryRenderer
{
    
    private static int SLOTSCALE = 84;
    private static Dictionary<WeaponTag, Texture2D> weaponSprites;
    
    public static void Init()
    {
        string bowPath = "Resources/Sprites/Weapons/bow.png";
        string swordPath = "Resources/Sprites/Weapons/sword.png";
        string whipPath = "Resources/Sprites/Weapons/whip.png";
        weaponSprites = new Dictionary<WeaponTag, Texture2D>();
        weaponSprites.Add(WeaponTag.SWORD,Raylib.LoadTexture(swordPath));
        weaponSprites.Add(WeaponTag.WHIP,Raylib.LoadTexture(whipPath));
        weaponSprites.Add(WeaponTag.BOW,Raylib.LoadTexture(bowPath));
    }

    public static void End()
    {
        foreach (var element in weaponSprites)
        {
            Raylib.UnloadTexture(element.Value);
        }
        weaponSprites.Clear();
    }
    
    public static void RenderInventory(Inventory inventory,int x,int y)
    {
        int height = SLOTSCALE;
        int width = SLOTSCALE * 3;
        int i = 0;
        //DrawInventorBackground
        Raylib.DrawRectangle(x,y,width,height,Color.Black);
        Rectangle src = new Rectangle(0, 0, weaponSprites[WeaponTag.SWORD].Width,weaponSprites[WeaponTag.SWORD].Height);
        //DrawInventoryRect
        foreach ( Weapon weapon in  inventory.getWeapons() )
        {
            Rectangle destRect = new Rectangle(x + (i * SLOTSCALE),y,SLOTSCALE,SLOTSCALE);
            if (weapon != null)
            {
                Raylib.DrawTexturePro(weaponSprites[weapon.tag],src,destRect,Vector2.Zero,0,Color.White);
            }
            if(weapon != null && weapon == inventory.getCurrentWeapon())
                Raylib.DrawRectangleLinesEx(destRect,3.0f,Color.White);
            else
                Raylib.DrawRectangleLinesEx(destRect,3.0f,Color.Red);
            i += 1;
        }
    }
}