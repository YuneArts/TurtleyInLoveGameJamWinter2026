using UnityEngine;
using System.Collections;

public class StaminaBlocks : MonoBehaviour
{
    [SerializeField] private Vector2 pushDirection;
    [SerializeField] private bool beingPushed;

    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask wallsLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!beingPushed)
        {
            if(pushDirection != Vector2.zero)
            {
                var blockMove = transform.position;
                blockMove.x += pushDirection.x;
                blockMove.y += pushDirection.y;
                if(CanBePushed(blockMove))
                {
                    StartCoroutine(PushBlock(blockMove));
                }
            }
        }
        
    }

    public void UpdateBlockDirection(Vector2 moveDir)
    {
        pushDirection.x = moveDir.x;
        pushDirection.y = moveDir.y;
    }

    IEnumerator PushBlock(Vector3 blockMove)
    {
        beingPushed = true;

        while((blockMove - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, blockMove, moveSpeed * Time.deltaTime);
            Debug.Log("Block is being pushed.");
            yield return null;
        }

        transform.position = blockMove;

        beingPushed = false;

        yield return null;
    }

    private bool CanBePushed(Vector3 blockMove)
    {
        if(Physics2D.OverlapCircle(blockMove, 0.2f, wallsLayer) != null)
        {
            return false;
        }

        return true;
    }
}
