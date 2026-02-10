using UnityEngine;

public class StaminaManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;
    void Start()
    {
        DataHolder.Instance.isPlaying = true;
        DataHolder.Instance.staTrain = true;
        ChooseLevel();
    }

    private void ChooseLevel()
    {
        int levelIndex = Random.Range(0, levels.Length);
        levels[levelIndex].SetActive(true);
    }
}
