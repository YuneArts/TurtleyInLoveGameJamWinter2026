using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public enum TrainingType {TrainSpeed, TrainPower, TrainStamina, None}

public class PetStats : MonoBehaviour
{
    [SerializeField] private int speed, power, stamina;
    [SerializeField] private bool speedTrain, powerTrain, staminaTrain;
    [SerializeField] private Text spdText, powText, staText;
    private TrainingType trType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trType = TrainingType.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void IncreaseSpeed(int spd)
    {
        speed += spd;
        trType = TrainingType.TrainSpeed;
        Debug.Log($"Speed = {speed}");
        StartCoroutine(StatGainPopup());
    }

    private void IncreasePower(int pwr)
    {
        power += pwr;
        trType = TrainingType.TrainPower;
        Debug.Log($"Power = {power}");
        StartCoroutine(StatGainPopup());
    }

    private void IncreaseStamina(int stm)
    {
        stamina += stm;
        trType = TrainingType.TrainStamina;
        Debug.Log($"Stamina = {stamina}");
        StartCoroutine(StatGainPopup());
    }

    private IEnumerator StatGainPopup()
    {
        switch (trType)
        {
            case TrainingType.TrainSpeed:
                spdText.text = $"{speed}";
                //Instantiate speed UI popup that increases by 1 or 2, depending on how much the stat is gained from training minigame.
                break;
            case TrainingType.TrainPower:
                powText.text = $"{power}";
                //Instantiate power UI popup that increases by 1 or 2, depending on how much the stat is gained from training minigame. 
                break;
            case TrainingType.TrainStamina:
                staText.text = $"{stamina}";
                //Instantiate stamina UI popup that increases byt 1 or 2, depending on how much the stat is gained from training minigame.
                break;
            /*default:
                Debug.Log("No training performed.");
                yield return null;*/
        }
        yield return null;
    }
}
