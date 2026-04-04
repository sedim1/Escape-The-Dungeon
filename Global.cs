using System.Numerics;
using Raylib_cs;

namespace DungeonCrawlerJam2026;

public static class Global
{
    public static int GRIDSCALE = 8;
    public static Texture2D background;

    public static void DrawBackground()
    {
        Rectangle src =  new Rectangle(0, 0, background.Width, background.Height);
        Rectangle dest = new Rectangle(0, 0, 1024, 768);
        Raylib.DrawTexturePro(background,src,dest,Vector2.Zero,0.0f,Color.White);
    }
}