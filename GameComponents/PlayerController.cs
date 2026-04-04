using DungeonCrawlerJam2026.Characters;
using DungeonCrawlerJam2026.Utilties;
using Raylib_cs;

namespace DungeonCrawlerJam2026.GameComponents;

public static class PlayerController
{
    private static Sound healSound;
    private static Sound slashSound;

    public static void LoadControllerSouds()
    {
        healSound = Raylib.LoadSound("Resources/Audio/heal.wav");
        slashSound = Raylib.LoadSound("Resources/Audio/slash.wav");
    }

    public static void UnloadControllerSounds()
    {
        Raylib.UnloadSound(healSound);
        Raylib.UnloadSound(slashSound);
    }
    
    public static void ProcessInput(CharacterManager characterManager,GameMap map)
    {
        Player player = characterManager.GetPlayer();
        if (player == null)
            return;
        //Dont process any sort of input if player is moving
        if (player.isMoving)
            return;
        bool inputMovement = false;
        //Movement Input
        if (Raylib.IsKeyPressed(KeyboardKey.Q))
        {
            player.turnLeft();
        }
        if (Raylib.IsKeyPressed(KeyboardKey.E))
        {
            player.turnRight();
        }
        
        if (Raylib.IsKeyPressed(KeyboardKey.W))
        {
            Console.WriteLine("Input movement");
            Vector2i nextPosition = player.cellPosition.Add(player.GetForwardDirection());
            if (!MovementSystem.isValidMove(map, player, characterManager.GetCharacters(), nextPosition))
            {
                MovementSystem.ProcessEnemiesActions(map,player,characterManager.GetCharacters());
                return;
            }
            inputMovement = true;
            player.moveForward();
        }
        else if (Raylib.IsKeyPressed(KeyboardKey.S))
        {
            Console.WriteLine("Input movement");
            Vector2i nextPosition = player.cellPosition.Add(player.GetBackDirection());
            if (!MovementSystem.isValidMove(map, player, characterManager.GetCharacters(), nextPosition))
            {
                MovementSystem.ProcessEnemiesActions(map,player,characterManager.GetCharacters());
                return;
            }
            inputMovement = true;
            player.moveBackward();
        } 
        else if (Raylib.IsKeyPressed(KeyboardKey.A))
        {
            Console.WriteLine("Input movement");
            Vector2i nextPosition = player.cellPosition.Add(player.GetLeftDirection());
            if (!MovementSystem.isValidMove(map, player, characterManager.GetCharacters(), nextPosition))
            {
                MovementSystem.ProcessEnemiesActions(map,player,characterManager.GetCharacters());
                return;
            }

            inputMovement = true;
            player.moveLeft();
        } 
        else if (Raylib.IsKeyPressed(KeyboardKey.D))
        {
            Console.WriteLine("Input movement");
            Vector2i nextPosition = player.cellPosition.Add(player.GetRightDirection());
            if (!MovementSystem.isValidMove(map, player, characterManager.GetCharacters(), nextPosition))
            {
                MovementSystem.ProcessEnemiesActions(map,player,characterManager.GetCharacters());
                return;
            }

            inputMovement = true;
            player.moveRight();
        }
        if (inputMovement)
        {
            player.TriggerMovement();
            MovementSystem.ProcessEnemiesActions(map,player,characterManager.GetCharacters());
            return;
        }
        //Attack Input
        if (Raylib.IsKeyPressed(KeyboardKey.J))
        {
            Raylib.PlaySound(slashSound);
            player.TriggerAttack(characterManager.GetCharacters());
            MovementSystem.ProcessEnemiesActions(map,player,characterManager.GetCharacters());
        }
        //HealInput
        if (Raylib.IsKeyPressed(KeyboardKey.K) && player.getCooldownComponent().hasFinsished())
        {
            Raylib.PlaySound(healSound);
            player.getHealthComponent().increase(50);
            player.getCooldownComponent().restart();
        }
        
    }
}