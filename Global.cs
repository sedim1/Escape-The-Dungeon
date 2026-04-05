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

    private static float shortAngleDist(float from, float to)
    {
        float turn = 360.0f;
        float deltaAngle = (to - from) % turn;
        return ((2 * deltaAngle) % turn) - deltaAngle;
    }

    public static float LerpAngle(float from, float to, float t)
    {
        return from + shortAngleDist(from,to) * t;
    }

    public static Color calculateFogColor(float dist)
    {
        float maxDist =4.0f; 
        float intensity = 1.0f - Math.Clamp(dist / maxDist, 0.0f, 1.0f);
        byte brightness = (byte)(intensity * 255);
        Color c = new Color(brightness, brightness, brightness, (byte)255);
        return c;
    }
}