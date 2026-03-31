using DungeonCrawlerJam2026.Characters;
using DungeonCrawlerJam2026.Utilties;

namespace DungeonCrawlerJam2026.GameComponents;

public static class MovementSystem
{ 
    public static void UpdateCharactersPositions(List<Character> characters,float delta)
    {
        foreach (Character character in characters)
            character.UpdateWorldPosition(delta);
    }

    public static bool isValidMove(GameMap map, Character character, List<Character> characters, Vector2i position)
    {
        if (OutOfBounds(map, position))
        {
            Console.WriteLine("position: " + position.ToString() + " is out of bounds");
            return false;
        }

        if (!map.positionIsFloor(position.X, position.Y))
        {
            Console.WriteLine("position: " + position.ToString() + " is a wall");
            return false;
        }

        foreach (Character c in characters)
        {
            if (Object.ReferenceEquals(c, character))
                continue;
            if (position.Equals(c.cellPosition))
            {
                Console.WriteLine("position: " + position.ToString() + " is already occupied by another character");
                return false;
            }
        }
        return true;
    }
    public static bool OutOfBounds(GameMap map,Vector2i position)
    {
        bool outX = position.X < 0 || position.X >= map.GetWidth();
        bool outY = position.Y < 0 || position.Y >= map.GetHeight();
        return (outX || outY);
    }

    public static void ProcessEnemiesActions(GameMap map,Player player, List<Character> characters)
    {
        foreach (Character enemy in characters)
        {
            if(enemy is Player)
                continue;
            (enemy as Enemy).ProcessAction(map,characters,player);
        }
    }
}