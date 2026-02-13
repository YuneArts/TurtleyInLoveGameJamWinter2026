using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(TitleScreenSetup());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator TitleScreenSetup()
    {
        DataHolder.Instance.ResetVariables();
        DataHolder.Instance.StartGame();
        DataHolder.Instance.TogglePersistentHUD();
        PersistentUI.instance.showStats = false;
        PersistentUI.instance.ToggleStatsHUD();
        yield return null;
    }

    public void StartButton()
    {
        StartCoroutine(TitleStartGame());
    }

    IEnumerator TitleStartGame()
    {
        PersistentUI.instance.SwitchStatsBool();
        PersistentUI.instance.LoadScene("MainPetScreen");
        yield return null;
    }
}
