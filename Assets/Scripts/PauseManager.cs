using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private string mainMenuSceneName = "TitleScreen";

    bool CanPauseInScene(string sceneName)
    {
        bool isMainMenu = sceneName == mainMenuSceneName;
        bool isMinigame = sceneName.StartsWith("Minigame_");
        return !isMainMenu && !isMinigame;
    }
}
