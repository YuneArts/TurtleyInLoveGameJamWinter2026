using UnityEngine;

public class PauseManager : MonoBehaviour
{
    bool CanPauseInScene(string sceneName)
    {
        bool isMainMenu = sceneName == mainMenuSceneName;      // "TitleScreen"
        bool isMinigame = sceneName.StartsWith("Minigame_");
        return !isMainMenu && !isMinigame;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
