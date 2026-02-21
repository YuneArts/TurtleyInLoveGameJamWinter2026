using UnityEngine;
using UnityEngine.SceneManagement;

public class StaminaManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;
    void Start()
    {
        DataHolder.Instance.isPlaying = true;
        DataHolder.Instance.staTrain = true;
        ChooseLevel();
        PersistentUI.instance.ToggleStatsHUD();
    }

    private void ChooseLevel()
    {
        int levelIndex = Random.Range(0, levels.Length);
        levels[levelIndex].SetActive(true);
    }

    public void ReloadLevel(string levelName)
    {
        if(DataHolder.Instance.isPlaying)
        {
            SceneManager.LoadScene(levelName);
        }
    }
}
