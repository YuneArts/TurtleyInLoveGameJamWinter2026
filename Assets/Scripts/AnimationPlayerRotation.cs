using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class AnimationPlayerRotation : MonoBehaviour
{
    private InputAction moveInput;

    [SerializeField]
    public Animator animator;
    [SerializeField]
    private PlayerInput playerInput;

    private void Awake()
    {
        moveInput = playerInput.actions.FindAction("Movement");
    }


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        FacingDirection(moveInput);
    }
     
    public void FacingDirection(InputAction moveInput)
    {
        // need to compare four vector2d's so that the player faces the correct direction when moving.
        // Changing the animation parameters to the correct direction will trigger the correct animation to play.
        Vector2 input = moveInput.ReadValue<Vector2>();
        if (input.x > 0)
        {
            animator.SetTrigger("Right");
        }
        else if (input.x < 0)
        {
            animator.SetTrigger("Left");
        }
        else if (input.y > 0)
        {
            animator.SetTrigger("Up");

        }
        else if (input.y < 0)
        {
            animator.SetTrigger("Down");
        }
    } 
}
