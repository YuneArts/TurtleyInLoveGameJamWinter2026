using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PersistentUI : MonoBehaviour
{
    public static PersistentUI instance;

    [Header("Scene Return")]
    [SerializeField] private string lastGameplayScene; // optional: visible for debugging in inspector

    [SerializeField] private TextMeshProUGUI powerNumber,speedNumber, staminaNumber, sessionAmount;
    public bool showStats = false;
    [SerializeField] private GameObject statsHUDObject, trainCountObject;

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

    void Start()
    {
        ToggleStatsHUD();
    }

    // --- Scene loading helpers ---

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadMinigame(string sceneName)
    {
        if(DataHolder.Instance.isPlaying = false && DataHolder.Instance.trainSessions > 0)
        {
            SceneManager.LoadScene(sceneName);
        }
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
            speedNumber.text = $"{DataHolder.Instance.petSpeed}";
            Debug.Log("Speed UI updated.");
        }
        else if(DataHolder.Instance.staTrain)
        {
            staminaNumber.text = $"{DataHolder.Instance.petStamina}";
            Debug.Log("Stamina UI updated.");
        }
        else
        {
            Debug.Log("No training detected. UI will not be updated.");
        }
    }

    public void ToggleStatsHUD()
    {
        if (showStats)
        {
            statsHUDObject.SetActive(true);
            trainCountObject.SetActive(true);
            if(DataHolder.Instance.isPlaying)
            {
                trainCountObject.SetActive(false);
            }
        }
        else if(!showStats)
        {
            statsHUDObject.SetActive(false);
            trainCountObject.SetActive(false);
        }

        Debug.Log($"Stats HUD is {showStats}");
    }

    public void SwitchStatsBool()
    {
        showStats = !showStats;
    }

    public void UpdateTrainSessions()
    {
        sessionAmount.text = $"Training Sessions Left: {DataHolder.Instance.trainSessions}";
    }

    public void ChangeTrainText()
    {
        sessionAmount.text = "Out of sessions. Race in the Prix of Hearts!";
    }
}
