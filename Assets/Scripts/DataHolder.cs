using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataHolder : MonoBehaviour
{
    public static DataHolder Instance;

    public int petSpeed, petPower, petStamina, trainSessions;

    public bool isPlaying, isRacing, powTrain, speedTrain, staTrain;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ResetVariables()
    {
        isPlaying = false;
        isRacing = false;
        powTrain = false;
        speedTrain = false;
        staTrain = false;
    }

    public void ReduceTrainSessions()
    {
        trainSessions -= 1;
        //PersistentUI.instance.UpdateSessionCount();
    }
}
