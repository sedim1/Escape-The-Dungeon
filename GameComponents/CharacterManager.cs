using System.Numerics;
using DungeonCrawlerJam2026.Characters;
using DungeonCrawlerJam2026.Utilties;
using DungeonCrawlerJam2026.Utilties.Scenes;
using Raylib_cs;

namespace DungeonCrawlerJam2026.GameComponents;

public class CharacterManager
{
    private List<Character> characters;
    private Sound enemyDead;
    private Sound lowHealth;


    public void LoadSounds()
    {
        enemyDead = Raylib.LoadSound("Resources/Audio/enemyDie.wav");
        lowHealth = Raylib.LoadSound("Resources/Audio/lowHealth.wav");
    }

    public void UnloadSounds()
    {
        Raylib.UnloadSound(enemyDead);
        Raylib.UnloadSound(lowHealth);
    }
    
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
            Raylib.PlaySound(enemyDead);
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

    public void checkPlayerDead()
    {
        Player p = GetPlayer();
        if (p == null)
            return;
        if (p.getHealthComponent().isDepleted())
        {
            SceneManager.TriggerChange("gameOver");
            return;
        }
        if (p.getHealthComponent().getHealth() <= 20 && !Raylib.IsSoundPlaying(lowHealth))
        {
            Raylib.PlaySound(lowHealth);
        }
        
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