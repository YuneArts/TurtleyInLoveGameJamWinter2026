using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerMinigameBar : MonoBehaviour
{
    [SerializeField] private Transform pointA, pointB;
    [SerializeField] private RectTransform safeZone;
    //private bool isPlaying;
    public float moveSpeed;

    private float direction;
    private RectTransform barTransform;
    private Vector3 targetPoisition;
    void Start()
    {
        barTransform = GetComponent<RectTransform>();
        targetPoisition = pointB.position;
        ShufflePowerMinigame();
        DataHolder.Instance.isPlaying = true;
    }

    void Update()
    {
        if (DataHolder.Instance.isPlaying)
        {
            barTransform.position = Vector3.MoveTowards(barTransform.position, targetPoisition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(barTransform.position, pointA.position) < 0.1f)
            {
                targetPoisition = pointB.position;
                direction = 1f;
            }
            else if (Vector3.Distance(barTransform.position, pointB.position) < 0.1f)
            {
                targetPoisition = pointA.position;
                direction = -1f;
            }
        }
    }

    public void PowerButtonClick()
    {
        if (DataHolder.Instance.isPlaying)
        {
            StartCoroutine(CheckSuccess());
        }
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

        if (RectTransformUtility.RectangleContainsScreenPoint(safeZone, barTransform.position, null))
        {
            DataHolder.Instance.petPower += 2;
            Debug.Log("Success! +2 Power");
        }
        else
        {
            DataHolder.Instance.petPower += 1;
            Debug.Log("Failed. +1 Power");
        }
        yield return null;
    } 
}
