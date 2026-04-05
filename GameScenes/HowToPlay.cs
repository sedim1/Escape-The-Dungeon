using System.Numerics;
using Raylib_cs;

using DungeonCrawlerJam2026.Utilties.Scenes;

namespace DungeonCrawlerJam2026.GameScenes;

public class HowToPlay : Scene
{
    private bool loaded = false;
    private Texture2D keyboard;
    private Texture2D keyboardAttack;
    private Texture2D typesReference;
    
    public override void OnEnter()
    {
        Console.WriteLine("Entered HowToPlay Scene");
        keyboard = Raylib.LoadTexture("Resources/Sprites/Extras/keyboardMovement.png");
        keyboardAttack = Raylib.LoadTexture("Resources/Sprites/Extras/keyboardAttackt.png");
        typesReference = Raylib.LoadTexture("Resources/Sprites/typesReference.png");
    }
    public override void OnExit(){
        Console.WriteLine("Exiting HowToPlay Scene");
        if (loaded)
        {
            Raylib.UnloadTexture(keyboard);
            Raylib.UnloadTexture(keyboardAttack);
            Raylib.UnloadTexture(typesReference);
        }
        loaded = false;
    }
    public override void Update(float deltaTime)
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            SceneManager.TriggerChange("title");
        }
    }
    public override void Draw()
    {
        Global.DrawBackground();
        Raylib.DrawText("PRESS ENTER TO RETURN",0,0,20,Color.White);
        Raylib.DrawText("MOVEMENT:",55,200,50,Color.White);
        Raylib.DrawTexturePro(keyboard,new Rectangle(0,0,keyboard.Width,keyboard.Height),new Rectangle(50,250,300,300),Vector2.Zero,0.0f,Color.White);
        Raylib.DrawText("HEAL/\nATTACK:",380,180,50,Color.White);
        Raylib.DrawTexturePro(keyboardAttack,new Rectangle(0,0,keyboard.Width,keyboard.Height),new Rectangle(350,250,300,300),Vector2.Zero,0.0f,Color.White);
        Raylib.DrawText("ENEMY\nTYPES:",650,180,50,Color.White);
        Raylib.DrawTexturePro(typesReference,new Rectangle(0,0,keyboard.Width,keyboard.Height),new Rectangle(650,250,300,300),Vector2.Zero,0.0f,Color.White);
        Raylib.DrawText("BLUE -> RED\nRED -> GREEN\nGREEN -> BLUE\n",
            650,550,30,Color.White);
    }
}