using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PersistentUI : MonoBehaviour
{
    public static PersistentUI instance;

    [Header("Scene Return")]
    [SerializeField] private string lastGameplayScene; // optional: visible for debugging in inspector

    [SerializeField] private TextMeshProUGUI powerNumber; //Add Speed and Stamina later once those numbers are in the UI.

    private void Awake()
    {
        // Singleton + persistence (ONLY here)
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // --- Scene loading helpers ---

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Call this when launching a minigame FROM gameplay
    public void LoadMinigame(string minigameSceneName)
    {
        lastGameplayScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(minigameSceneName);
    }

    // Call this from the minigame when it ends
    public void ReturnToLastGameplayScene()
    {
        if (string.IsNullOrEmpty(lastGameplayScene))
        {
            Debug.LogWarning("No last gameplay scene stored. Return aborted.", this);
            return;
        }

        SceneManager.LoadScene(lastGameplayScene);
    }

    // Optional debug hook
    public void OnButtonClick()
    {
        Debug.Log("Button was clicked!");
    }

    public void UpdateStatsUI()
    {
        if(DataHolder.Instance.powTrain)
        {
            powerNumber.text = $"{DataHolder.Instance.petPower}";
            Debug.Log("Power UI updated.");
        }
        else if(DataHolder.Instance.speedTrain)
        {
            Debug.Log("Speed UI updated.");
        }
        else if(DataHolder.Instance.staTrain)
        {
            Debug.Log("Stamina UI updated.");
        }
        else
        {
            Debug.Log("No training detected. UI will not be updated.");
        }
    }
}
