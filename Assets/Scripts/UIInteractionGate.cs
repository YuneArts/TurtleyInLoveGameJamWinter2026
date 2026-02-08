using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInteractionGate : MonoBehaviour
{
    [Header("Assign the CanvasGroup on your MenuRoot")]
    [SerializeField] private CanvasGroup menuCanvasGroup;

    [Header("Scene name where UI should NOT be interactable")]
    [SerializeField] private string mainMenuSceneName = "TitleScreen";

    private void Awake()
    {
        if (menuCanvasGroup == null)
            Debug.LogError($"{nameof(UIInteractionGate)}: Menu CanvasGroup not assigned.", this);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        // Apply immediately for the starting scene (important on boot).
        ApplyForScene(SceneManager.GetActiveScene().name);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplyForScene(scene.name);
    }

    // Adjusts the menu's interactivity based on the scene name.
    private void ApplyForScene(string sceneName)
    {
        if (menuCanvasGroup == null) return;

        bool isMainMenu = sceneName == mainMenuSceneName;
        bool isMinigame = sceneName.StartsWith("Minigame_");

        bool shouldInteract = !isMainMenu && !isMinigame;

        menuCanvasGroup.alpha = shouldInteract ? 1f : 0.5f;
        SetMenuInteractable(shouldInteract);
    }

    // Sets the interactability of the menu.
    private void SetMenuInteractable(bool interactable)
    {
        if (menuCanvasGroup == null) return;

        menuCanvasGroup.interactable = interactable;
        menuCanvasGroup.blocksRaycasts = interactable;
    }
}
