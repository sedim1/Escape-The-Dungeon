using DungeonCrawlerJam2026.Characters;

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
}