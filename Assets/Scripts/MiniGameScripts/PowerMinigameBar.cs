using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PowerMinigameBar : MonoBehaviour
{
    [SerializeField] private Transform pointA, pointB;
    [SerializeField] private RectTransform safeZone;
    public float moveSpeed;
    //private float direction;
    private RectTransform barTransform;
    private Vector3 targetPoisition;
    [SerializeField] private GameObject button;
    //Add references to results screen. Will start disabled in scene editor and be enabled based on result in CheckResult function.
    void Start()
    {
        barTransform = GetComponent<RectTransform>();
        targetPoisition = pointB.position;
        ShufflePowerMinigame();
        DataHolder.Instance.isPlaying = true;
        DataHolder.Instance.powTrain = true;
        PersistentUI.instance.ToggleStatsHUD();
        button.SetActive(true);
    }

    void Update()
    {
        if (DataHolder.Instance.isPlaying)
        {
            barTransform.position = Vector3.MoveTowards(barTransform.position, targetPoisition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(barTransform.position, pointA.position) < 0.1f)
            {
                targetPoisition = pointB.position;
                //direction = 1f;
            }
            else if (Vector3.Distance(barTransform.position, pointB.position) < 0.1f)
            {
                targetPoisition = pointA.position;
                //direction = -1f;
            }
        }
    }

    public void PowerButtonClick()
    {
        if (DataHolder.Instance.isPlaying)
        {
            StartCoroutine(CheckSuccess());
        }
        Debug.Log("Button Clicked.");
    }


    void ShufflePowerMinigame()
    {
        safeZone.sizeDelta = new Vector2(45f, Random.Range(35f, 80f));
        moveSpeed = Random.Range(150f, 300f);
        safeZone.position = new Vector2(safeZone.position.x, Random.Range(pointB.position.y, pointA.position.y));
    }

    private IEnumerator CheckSuccess()
    {
        DataHolder.Instance.isPlaying = false;

        button.SetActive(false);

        if (RectTransformUtility.RectangleContainsScreenPoint(safeZone, barTransform.position, null))
        {
            PersistentUI.instance.IncreasePower(2);
            //Add results screen once we get that asset in. Can be referenced in here.
        }
        else
        {
            PersistentUI.instance.IncreasePower(1);
            //Add result screen once we get that asset in. Can be referenced in here.
        }
        
        PersistentUI.instance.UpdateStatsUI();
        PersistentUI.instance.EnableResultsPanel();
        DataHolder.Instance.ReduceTrainSessions();

        yield return new WaitForSeconds(3f);

        //PersistentUI.instance.DisableResultsPanel();
        PersistentUI.instance.LoadScene("MainPetScreen");
        yield return null;
    } 
}
