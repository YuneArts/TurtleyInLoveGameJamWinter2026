using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class StaminaMiniGameMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask nonwalkLayer, pushLayer;
    [SerializeField] private PlayerInput playerInput;
    private InputAction moveInput;
    public bool isMoving, levelComplete;
    private Vector2 moveValue;

    private void Awake()
    {
        moveInput = playerInput.actions.FindAction("Movement");
    }

    private void Update()
    {
        if(!levelComplete)
        {
            if(!isMoving)
            {
                moveValue = moveInput.ReadValue<Vector2>();

                if(moveValue != Vector2.zero)
                {
                    var targetPos = transform.position;
                    targetPos.x += moveValue.x;
                    targetPos.y += moveValue.y;
                    Debug.Log("Horizontal: " + moveValue.x + ", Vertical: " + moveValue.y);
                    if(isWalkable(targetPos))
                    {
                        StartCoroutine(MoveTile(targetPos));
                    }
                } 
            }
        }
    }

    IEnumerator MoveTile(Vector3 targetPos)
    {
        isMoving = true;

        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
    }

    private bool isWalkable(Vector3 targetPos)
    {
        Collider2D blockCheck = Physics2D.OverlapCircle(targetPos, 0.2f, pushLayer);

        if(Physics2D.OverlapCircle(targetPos, 0.2f, nonwalkLayer) != null)
        {
            return false;
        }
        else if(Physics2D.OverlapCircle(targetPos, 0.2f, pushLayer) != null)
        {
            StaminaBlocks block = blockCheck.GetComponent<StaminaBlocks>();
            Debug.Log($"Obtained " + block.name + " on " + blockCheck.gameObject.name);
            block.UpdateBlockDirection(moveValue);
            return false;
        }

        return true;
    }
}
