using System;
using System.Collections.Generic;

namespace DungeonCrawlerJam2026.Utilties.Scenes;

public static class SceneManager
{
    private static Dictionary<string, Scene> scenes = new Dictionary<string, Scene>();
    public static Scene currentScene;
    private static bool scene_change_event = false;
    private static string next_scene_id = ""; 
    
    public static void AddScene(Scene scene, string title)
    {
        Console.WriteLine("Scene added: " + title);
        scenes.Add(title,scene);
    }
    
    public static void TriggerChange(string scene_id)
    {
        Console.WriteLine("Trigger Scene change to: " + scene_id);
        if (scenes.ContainsKey(scene_id))
        {
            next_scene_id = scene_id;
            scene_change_event = true;
        }
        else
        {
            Console.WriteLine("Trigger Scene not found: " + scene_id);
        }
    }

    public static void CheckSceneChange()
    {
        if (scene_change_event)
        {
            ChangeScene();
            scene_change_event = false;
            next_scene_id = "";
        }
    }
    
    private static void ChangeScene()
    {
        Scene next_scene = scenes[next_scene_id];
        if (currentScene != null)
            currentScene.OnExit();
        next_scene.OnEnter();
        currentScene = next_scene;
        scene_change_event = false;
        next_scene_id = "";
    }

    public static void ClearAllScenes()
    {
        foreach (Scene scene in scenes.Values)
        {
            //Free necesary resources if they still exist
            scene.OnExit();
        }
        scenes.Clear();
    }
}