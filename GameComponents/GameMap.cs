using System.Text.Json;
using Raylib_cs;

namespace DungeonCrawlerJam2026.GameComponents;


class GameMapParser
{
    public int _width { get; set; }
    public int _height { get; set; }
    public List<List<int>> _map { get; set; }
    public GameMapParser()
    {}
}

public class GameMap
{
    private int[,] map;
    private int width, height;
    public void LoadMap(string fileName)
    {
        GameMapParser parser = new GameMapParser();
        using StreamReader reader = new(fileName);
        string json =  reader.ReadToEnd();
        parser = JsonSerializer.Deserialize<GameMapParser>(json);
        SetUpMap(parser);
    }

    private void SetUpMap(GameMapParser parser)
    {
        width = parser._width;
        height = parser._height;
        map = new int[parser._width,parser._height];
        for(int y = 0; y < parser._height; y++)
            for(int x = 0; x < parser._width; x++)
                map[y,x] = parser._map[y][x];
            
    }

    public bool positionIsExit(int x, int y)
    {
        return map[y, x] == 2;
    }

    public bool positionIsFloor(int x, int y)
    {
        return map[y,x] == 0 || map[y,x] == 2;
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public int[,] GetMap()
    {
        return map;
    }
}