using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class AnimationPlayerRotation : MonoBehaviour
{
    private InputAction moveInput;
    private Animator animator;

    void Start()
    {
        moveInput = GetComponent<PlayerInput>().actions.FindAction("Movement");
        animator = GetComponent<Animator>()
    }
    public void FacingDirection(InputAction moveInput)
    {
        // need to compare four vector2d's so that the player faces the correct direction when moving.
        // Changing the animation parameters to the correct direction will trigger the correct animation to play.
    }
}
