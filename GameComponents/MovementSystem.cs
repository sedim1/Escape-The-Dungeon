namespace DungeonCrawlerJam2026.GameComponents;

public static class MovementSystem
{ 
    public static void UpdateCharactersPositions(List<Character> characters,float delta)
    {
        foreach (Character character in characters)
            character.UpdateWorldPosition(delta);
    }
}