using DungeonCrawlerJam2026.Characters;
using DungeonCrawlerJam2026.GameComponents;
using DungeonCrawlerJam2026.Utilties;
using DungeonCrawlerJam2026.Utilties.Scenes;
using Raylib_cs;

namespace DungeonCrawlerJam2026.GameScenes;

public class MainGameScene : Scene
{

    private GameMap level;
    private CharacterManager characterManager;

    public MainGameScene()
    {
        level = new GameMap();
        characterManager = new CharacterManager();
    }
    
    public override void OnEnter()
    {
        Console.WriteLine("Main Scene OnEnter");
        level.LoadMap("Resources/gamemap.json");
        characterManager.AddCharacter(new Player(new Vector2i(1,1),0));
        characterManager.AddCharacter(new Enemy(new Vector2i(2,3)));
    }

    public override void OnExit()
    {
        Console.WriteLine("Main Scene OnExit");
        characterManager.DeleteAllCharacters();
    }

    public override void Update(float deltaTime)
    {
        PlayerController.ProcessInput(characterManager,level);
        MovementSystem.UpdateCharactersPositions(characterManager.GetCharacters(), deltaTime);
    }

    public override void Draw()
    {
        level.DrawMinimap();
        GameRenderer.DrawCharactersOnMinipap(characterManager.GetCharacters());
    }
}