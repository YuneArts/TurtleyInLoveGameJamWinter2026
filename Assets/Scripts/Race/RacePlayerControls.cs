using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class RacePlayerControls : MonoBehaviour
{
    private int speed, power, stamina;
    [SerializeField] private PlayerInput raceInput;
    private Vector2 moveDirection;
    private bool raceStart = false, isDashing;

    [SerializeField] private Rigidbody2D rb2d;
    
    [SerializeField] private float jumpHeight;
    private float castDistance = 0.5f;

    private float dashStamina; 
    private float maxStamina = 100f; 
    
    private float lastDirection;
    
    [SerializeField] private Image staminaBar;
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private Vector2 boxSize;
    private float raceTime;
    [SerializeField] private float goalTime;
    [SerializeField] private TextMeshProUGUI timerText, goalTimerText, countdownText;
    [SerializeField] private GameObject countdownObject;

    private int countdownTime;

    void Start()
    {
        StartCoroutine(SetupRaceScene());
        //Set this boolean to true once countdown gets introduced. Possibly move this to DataHolder.
        dashStamina = maxStamina;
        //Sets direction in case the player dashes without moving left or right.
        lastDirection = 1f;
    }

    void Update()
    {
        if(raceStart)
        {
            //Detects if Movement InputAction reads any changes to the Vector2 tied to the bindings.
            if(moveDirection != Vector2.zero)
            {
                if(!isDashing)
                {
                    //Normal player movement.
                    rb2d.linearVelocity = new Vector2(moveDirection.x * speed, rb2d.linearVelocity.y);
                    //Records X axis/direction player last registered to line up dash.
                    lastDirection = moveDirection.x;
                }
                else
                {
                    //Allows for dashing while holding forward/back direction. Doesn't allow for turning mid-air.
                    rb2d.linearVelocity = new Vector2(lastDirection * (speed + (power + 3)), 0f);
                }
            }
            else if(!isDashing)
            {
                //Turns horizontal velocity off while letting vertical movement persist. Allows more percise left/right movement in the air.
                rb2d.linearVelocity = new Vector2(0, rb2d.linearVelocity.y);
            }
            else
            {
                //Uses last registered X value from player inputs to determine direction while forward/back direction isn't being pressed. 3 being added with Power can be changed.
                rb2d.linearVelocity = new Vector2(lastDirection * (speed + (power + 3)), 0f);
            }

            if(dashStamina < maxStamina)
            {
                ChargeDashStamina();
            }

            if(raceStart)
            {
                UpdateRaceTimer();
                SetRaceTimer();
            }
        }
    }

    IEnumerator SetupRaceScene()
    {
        raceStart = false;
        DataHolder.Instance.isRacing = true;
        DataHolder.Instance.TogglePersistentHUD();
        GetPetStats();
        SetGoalTimer();
        SetRaceTimer();
        countdownTime = 3;
        //Start countdown for beginning of race.
        StartCoroutine(StartCountdown());
        //Make sure float in this return matches Countdown Time set above.
        yield return new WaitForSeconds(3f);
        //Enable bool to allow controls and start time after countdown ends.
        raceStart = true;
        yield return new WaitForSeconds(1f);
        countdownObject.SetActive(false);
    }

    IEnumerator StartCountdown()
    {
        while(countdownTime > 0)
        {
            SetCountdownTimer();

            yield return new WaitForSeconds(1f);

            countdownTime --;
        }

        SetCountdownTimer();
    }

    private void SetCountdownTimer()
    {
        if(countdownTime > 0)
        {
            countdownText.text = $"{countdownTime}";
        }
        else if(countdownTime <= 0)
        {
            countdownText.text = "Go!";
        }
    }

    private void GetPetStats()
    {
        speed = DataHolder.Instance.petSpeed;
        power = DataHolder.Instance.petPower;
        stamina = DataHolder.Instance.petStamina;
    }

    private void SetRaceTimer()
    {
        timerText.text = string.Format("{0:#0.00}", raceTime);
    }

    private void SetGoalTimer()
    {
        //Add code here to determine new goal timer if multiple races are implemented into game.
        goalTimerText.text = string.Format("Time to Beat: {0:#0.00}", goalTime);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(IsGrounded())
            {
                rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpHeight);
            }
        }
    }

    public void Movement(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(!isDashing && dashStamina == maxStamina && raceStart)
            {
                StartCoroutine(PerformDash());
            }  
        }
    }

    private bool IsGrounded()
    {
        if(Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, floorLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator PerformDash()
    {
        //Deplete stamina bar
        UseDashStamina();
        isDashing = true;
        //Record gravity setting of Rigidbody2D to reset it to original value later.
        float originalGravity = rb2d.gravityScale;
        rb2d.gravityScale = 0f;
        //Dash duration, can plug in a float variable if desired.
        yield return new WaitForSeconds(0.5f);
        rb2d.gravityScale = originalGravity;
        isDashing = false;
        yield return null;
    }

    private void ChargeDashStamina()
    {
        //Recharges based on stamina stat from DataHolder. 5 in parentheses can be changed, chose not to make a variable for the time being.
        dashStamina += (stamina * 5) * Time.deltaTime;
        staminaBar.fillAmount = dashStamina / maxStamina;

        if(dashStamina > maxStamina)
        {
            dashStamina = maxStamina;
        }
    }

    private void UseDashStamina()
    {
        dashStamina = 0f;
    }

    private void UpdateRaceTimer()
    {
        raceTime += Time.deltaTime;

        if(raceTime >= goalTime)
        {
            raceTime = goalTime;
            raceStart = false;
            StartCoroutine(GameOver());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Finish"))
        {
            Debug.Log("Touched Finish Collider.");
            StartCoroutine(Victory());
        }
        else
        {
            Debug.Log("Touched non Finish Collider.");
        }
    }

    private IEnumerator Victory()
    {
        raceStart = false;
        Debug.Log("You Win! Restarting game and returning to MainPetScreen");
        yield return new WaitForSeconds(3f);
        DataHolder.Instance.StartGame();
        PersistentUI.instance.ResetStatsUI();
        PersistentUI.instance.LoadScene("MainPetScreen");
    }
    private IEnumerator GameOver()
    {
        Debug.Log("Game Over. Returning to MainPetScreen.");
        yield return new WaitForSeconds(3f);
        PersistentUI.instance.LoadScene("MainPetScreen");
    }
}
