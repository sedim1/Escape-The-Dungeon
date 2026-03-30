using DungeonCrawlerJam2026.Characters;
using Raylib_cs;
using Unglide;

namespace DungeonCrawlerJam2026.GameComponents;

public static class PlayerController
{
    public static void ProcessInput(CharacterManager characterManager)
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
            inputMovement = true;
            player.moveForward();
        }
        else if (Raylib.IsKeyPressed(KeyboardKey.S))
        {
            inputMovement = true;
            player.moveBackward();
        } 
        else if (Raylib.IsKeyPressed(KeyboardKey.A))
        {
            inputMovement = true;
            player.moveLeft();
        } 
        else if (Raylib.IsKeyPressed(KeyboardKey.D))
        {
            inputMovement = true;
            player.moveRight();
        }

        if (inputMovement)
        {
            player.TriggerMovement();
            return;
        }
    }
}