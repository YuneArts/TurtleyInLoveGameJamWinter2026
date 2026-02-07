using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReturnManager : MonoBehaviour
{
    public static SceneReturnManager Instance { get; private set; }

    public string LastGameplayScene { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Call this right before loading a minigame
    public void MarkReturnSceneAsCurrent()
    {
        LastGameplayScene = SceneManager.GetActiveScene().name;
    }

    // Call this from the minigame when it ends
    public void ReturnToLastGameplayScene()
    {
        if (string.IsNullOrEmpty(LastGameplayScene))
        {
            Debug.LogWarning("No LastGameplayScene stored. Returning aborted.");
            return;
        }

        SceneManager.LoadScene(LastGameplayScene);
    }
}
