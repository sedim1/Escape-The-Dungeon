using System.Numerics;
using Raylib_cs;

namespace DungeonCrawlerJam2026.GameComponents;

public static class InventoryRenderer
{
    
    private static int SLOTSCALE = 84;
    private static Dictionary<WeaponTag, Texture2D> weaponSprites;
    private static Dictionary<Types, float[]> colors;
    private static Shader maskingShader;
    
    public static void Init()
    {
        string bowPath = "Resources/Sprites/Weapons/bow.png";
        string swordPath = "Resources/Sprites/Weapons/sword.png";
        string whipPath = "Resources/Sprites/Weapons/whip.png";
        string shaderPath =  "Resources/Shaders/mask.glsl";
        maskingShader = Raylib.LoadShader(null, shaderPath);
        weaponSprites = new Dictionary<WeaponTag, Texture2D>();
        weaponSprites.Add(WeaponTag.SWORD,Raylib.LoadTexture(swordPath));
        weaponSprites.Add(WeaponTag.WHIP,Raylib.LoadTexture(whipPath));
        weaponSprites.Add(WeaponTag.BOW,Raylib.LoadTexture(bowPath));
        colors = new Dictionary<Types, float[]>();
        colors.Add(Types.FIRE,new float[4]{119.0f/255.0f,27.0f/255.0f,9.0f/255.0f,1.0f});
        colors.Add(Types.WATER,new float[4]{20.0f/255.0f,52.0f/255.0f,88.0f/255.0f,1.0f});
        colors.Add(Types.GRASS,new float[4]{60.0f/255.0f,92.0f/255.0f,54.0f/255.0f,1.0f});
    }

    public static void End()
    {
        Raylib.UnloadShader(maskingShader);
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
        int shaderLoc = Raylib.GetShaderLocation(maskingShader, "colorReplacement");
        //DrawInventorBackground
        Raylib.DrawRectangle(x,y,width,height,Color.Black);
        Rectangle src = new Rectangle(0, 0, weaponSprites[WeaponTag.SWORD].Width,weaponSprites[WeaponTag.SWORD].Height);
        //DrawInventoryRect
        foreach ( Weapon weapon in  inventory.getWeapons() )
        {
            Rectangle destRect = new Rectangle(x + (i * SLOTSCALE),y,SLOTSCALE,SLOTSCALE);
            if (weapon != null)
            {
                Raylib.BeginShaderMode(maskingShader);
                Raylib.SetShaderValue(maskingShader,shaderLoc,colors[weapon.GetAttackComponent().typeComponent.type],ShaderUniformDataType.Vec4);
                Raylib.DrawTexturePro(weaponSprites[weapon.tag],src,destRect,Vector2.Zero,0,Color.White);
                Raylib.EndShaderMode();
            }
            if(weapon != null && weapon == inventory.getCurrentWeapon())
                Raylib.DrawRectangleLinesEx(destRect,3.0f,Color.White);
            else
                Raylib.DrawRectangleLinesEx(destRect,3.0f,Color.Red);
            i += 1;
        }
    }
}