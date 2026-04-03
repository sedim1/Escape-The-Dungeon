using System.Numerics;
using DungeonCrawlerJam2026.Characters;
using DungeonCrawlerJam2026.Utilties;

namespace DungeonCrawlerJam2026.GameComponents;

public class CharacterManager
{
    private List<Character> characters;
    
    public CharacterManager()
    {
        characters = new List<Character>();
    }
    
    public void AddCharacter(Character character)
    {
        character.Enter();
        characters.Add(character);
    }

    public void RemoveCharacter(Character character)
    {
        character.Exit();
        characters.Remove(character);
    }

    public void DeleteAllCharacters()
    {
        foreach (Character character in characters)
            character.Exit();
        characters.Clear();
    }

    public void DeleteDefetaedEnemies()
    {
        for (int i = characters.Count -1; i >= 0; i--)
        {
            if (characters[i] is Player)
                continue;
            if(!characters[i].getHealthComponent().isDepleted())
                continue;
            characters[i].Exit();
            characters.RemoveAt(i);
        }
    }
    
    private bool isPlayer(Character character)
    {
        return character is Player;
    }

    public Player GetPlayer()
    {
        return characters.Find(isPlayer) as Player;
    }
    
    public List<Character> GetCharacters()
    {
        return characters;
    }
    
    public List<Character> GetEnemies()
    {
        List<Character> enemies = new List<Character>();
        PriorityQueue<Character, float> orderList = new PriorityQueue<Character, float>();
        Vector2i playerPos = GetPlayer().cellPosition;
        foreach (Character character in characters)
        {
         if(character is Player)
             continue;
         orderList.Enqueue(character, playerPos.distanceFrom(character.cellPosition));
        }
        while (orderList.Count > 0)
        {
            Character character = orderList.Dequeue();
            enemies.Add(character);
        }

        enemies.Reverse();
        return enemies;
    }
}