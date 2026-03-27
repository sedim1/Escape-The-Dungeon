using Raylib_cs;

namespace DungeonCrawlerJam2026;


    public class Game
    {
        public static int SW;
        public static int SH;
        private string title;

        public Game(int w, int h, string title)
        {
            this.title = title;
            SW = w;
            SH = h;
        }

        public void Run()
        {
            Init();
            GameLoop();
            End();
        }

        private void Init()
        {
            Raylib.InitWindow(SW, SH, title);
            Raylib.SetTargetFPS(60);
        }

        private void End()
        {
            Raylib.CloseWindow();
        }

        private void GameLoop()
        {
            while (!Raylib.WindowShouldClose())
            {
                float deltaTime = Raylib.GetFrameTime();
                Update(deltaTime);
                Render();
            }
        }

        private void Update(float delta)
        {
        }

        private void Render()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.SkyBlue);
            Raylib.DrawText(Raylib.GetFPS().ToString(), 0, 0, 26, Color.Black);
            Raylib.EndDrawing();
        }
    }
