using System.Text.Json;

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
    private int[][] map;
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
        map = new int[parser._height][];
        for (int y = 0; y < parser._height; y++)
        {
            map[y] = new int[parser._width];
            for (int x = 0; x < parser._width; x++)
            {
                map[y][x] = parser._map[y][x];
                Console.Write(parser._map[y][x] + " ");
            }
        }
    }

    public int GetWidth()
    {
        return map[0].Length;
    }

    public int GetHeight()
    {
        return map.Length;
    }

    public void DrawMinimap()
    {
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[0].Length; x++)
            {
            }
        }
    }
}