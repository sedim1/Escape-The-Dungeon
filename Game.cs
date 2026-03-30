using DungeonCrawlerJam2026.GameScenes;
using DungeonCrawlerJam2026.Utilties.Scenes;
using Raylib_cs;

namespace DungeonCrawlerJam2026;


    public class Game
    {
        private int sw;
        private int sh;
        private string title;

        public Game(int w, int h, string title)
        {
            this.title = title;
            this.sw = w;
            this.sh = h;
        }

        public void Run()
        {
            Init();
            GameLoop();
            End();
        }

        private void Init()
        {
            Raylib.InitWindow(sw, sh, title);
            Raylib.SetTargetFPS(60);
        }

        private void End()
        {
            SceneManager.ClearAllScenes();
            Raylib.CloseWindow();
        }

        private void GameLoop()
        {
            //Add scenes the game will run throughout its lifetime
            SceneManager.AddScene(new MainGameScene(),"main");
            SceneManager.TriggerChange("main");
            //Start Game loop
            while (!Raylib.WindowShouldClose())
            {
                float deltaTime = Raylib.GetFrameTime();
                SceneManager.CheckSceneChange();
                Update(deltaTime);
                Render();
            }
        }

        private void Update(float delta)
        {
            if (SceneManager.currentScene == null)
                return;
            SceneManager.currentScene.Update(delta);
        }

        private void Render()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            if (SceneManager.currentScene != null)
                SceneManager.currentScene.Draw();
            Raylib.DrawText(Raylib.GetFPS().ToString(), 0, 0, 26, Color.Yellow);
            Raylib.EndDrawing();
        }
    }
