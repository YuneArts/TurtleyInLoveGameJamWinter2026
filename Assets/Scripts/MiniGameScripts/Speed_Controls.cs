using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Speed_Controls : MonoBehaviour
{
    [SerializeField] private PlayerInput speedInput;
    private float inputDirection;
    private InputAction leftrightInput;
    private bool isRight;
    [SerializeField] private Image leftArrow, rightArrow, progressBar;
    [SerializeField] private float progress, progressDrain, maxProgress;

    private void Awake()
    {
        leftrightInput = speedInput.actions.FindAction("Left/Right");
    }
    void Start()
    {
        DataHolder.Instance.speedTrain = true;
        DataHolder.Instance.isPlaying = true;
        isRight = true;
        UpdateNextArrows();
        PersistentUI.instance.ToggleStatsHUD();
    }

    // Update is called once per frame
    void Update()
    {
        if(DataHolder.Instance.isPlaying)
        {
            inputDirection = leftrightInput.ReadValue<float>();
            if(inputDirection > 0)
            {
                RightCheck();
                Debug.Log("Detected Right Input.");
            }
            else if(inputDirection < 0)
            {
                LeftCheck();
                Debug.Log("Detected Left Input.");
            }

            if(progress > 0)
            {
                DrainProgress();
            }
        }
    }

    private void RightCheck()
    {
        if(isRight)
        {
            //Add progress toward minigame completion.
            UpdateProgress(5);
            //Change isRight boolean to false and update the arrow HUD to match the boolean.
            isRight = false;
            UpdateNextArrows();
        }
    }

    private void LeftCheck()
    {
        if(!isRight)
        {
            //Add progress toward minigame completion.
            UpdateProgress(5);
            //Change isRight boolean to true and update the arrow HUD to match the boolean.
            isRight = true;
            UpdateNextArrows();
        }
    }

    private void DrainProgress()
    {
        progress -= progressDrain * Time.deltaTime;

        progressBar.fillAmount = progress / maxProgress;

        if(progress < 0)
        {
            progress = 0;
        }
    }

    private void UpdateProgress(float prog)
    {
        if(DataHolder.Instance.isPlaying)
        {
            progress += prog;
            progressBar.fillAmount = progress / maxProgress;

            if(progress >= maxProgress)
            {
                progress = maxProgress;
                StartCoroutine(CompleteSpeedMinigame());
            }
        }   
    }

    private void UpdateNextArrows()
    {
        if(isRight)
        {
            SetImageAlpha(rightArrow, 1f);
            SetImageAlpha(leftArrow, 0.3f);
        }
        else if(!isRight)
        {
            SetImageAlpha(rightArrow, 0.3f);
            SetImageAlpha(leftArrow, 1f);
        }
    }

    private void SetImageAlpha(Image img, float imgAlp)
    {
        Color tempColor = img.color;
        tempColor.a = imgAlp;
        img.color = tempColor;
    }

    IEnumerator CompleteSpeedMinigame()
    {
        DataHolder.Instance.isPlaying = false;

        IncreaseSpeed(2);

        PersistentUI.instance.UpdateStatsUI();
        DataHolder.Instance.ReduceTrainSessions();

        yield return new WaitForSeconds(3f);

        PersistentUI.instance.LoadScene("MainPetScreen");

        yield return null;
    }

    private void IncreaseSpeed(int spd)
    {
        DataHolder.Instance.petSpeed += spd;
        Debug.Log($"Speed = {DataHolder.Instance.petSpeed}");
    }
}
