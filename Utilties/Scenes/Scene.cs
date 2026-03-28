namespace DungeonCrawlerJam2026.Utilties.Scenes;

public abstract class Scene
{
    public abstract void OnEnter();
    public abstract void Update(float deltaTime);
    public abstract void Draw();
    public abstract void OnExit();
    ~Scene()
    {
        OnExit();
    }
}

