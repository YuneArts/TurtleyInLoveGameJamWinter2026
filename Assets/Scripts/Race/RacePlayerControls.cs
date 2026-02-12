using UnityEngine;
using UnityEngine.InputSystem;

public class RacePlayerControls : MonoBehaviour
{
    private int speed, power, stamina;
    [SerializeField] private PlayerInput raceInput;
    private InputAction moveInput;
    private Vector2 moveDirection;
    private bool raceStart;

    private void Awake()
    {
        moveInput = raceInput.actions.FindAction("Movement");
    }
    void Start()
    {
        GetPetStats();
    }

    // Update is called once per frame
    void Update()
    {
        if(raceStart)
        {
            moveDirection = moveInput.ReadValue<Vector2>();

            if(moveDirection != Vector2.zero)
            {
                
            }
        }
    }

    private void GetPetStats()
    {
        speed = DataHolder.Instance.petSpeed;
        power = DataHolder.Instance.petPower;
        stamina = DataHolder.Instance.petStamina;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log("Input detected");

        if(context.performed)
        {
            //Add jump code here.
            Debug.Log("Jump performed");
        }
    }

    public void Movement(InputAction.CallbackContext context)
    {
        Debug.Log("Input detected");
    }

    public void Dash(InputAction.CallbackContext context)
    {
        //Debug.Log("Input detected");

        if(context.performed)
        {
            //Add dash code here.
            Debug.Log("Dash performed");
        }
    }
}
