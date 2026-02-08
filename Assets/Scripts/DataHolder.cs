using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataHolder : MonoBehaviour
{
    public static DataHolder Instance;

    public int petSpeed, petPower, petStamina;

    public bool isPlaying, powTrain, speedTrain, staTrain;
    //[SerializeField] private bool speedTrain, powTrain, staTrain;

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
        powTrain = false;
        speedTrain = false;
        staTrain = false;
    }
}
