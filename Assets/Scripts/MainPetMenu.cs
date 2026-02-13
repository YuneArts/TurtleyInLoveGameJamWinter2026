using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainPetMenu : MonoBehaviour
{
    [SerializeField] private AnimationTrigger animScript;
    void Start()
    {
        StartCoroutine(MainScreenStartup());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MainScreenStartup()
    {
        DataHolder.Instance.ResetVariables();
        PersistentUI.instance.showStats = true;
        PersistentUI.instance.ToggleStatsHUD();
        PersistentUI.instance.UpdateTrainSessions();
        //SessionCheck();
        
        yield return null;
    }

    private void SessionCheck()
    {
        if(DataHolder.Instance.trainSessions <= 0)
        {
            animScript.SlideOut();
        }
    }
}
