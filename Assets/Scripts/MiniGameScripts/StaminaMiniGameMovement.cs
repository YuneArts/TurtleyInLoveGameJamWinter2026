using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StaminaMiniGameMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask nonwalkLayer;
    public bool isMoving, levelComplete;
    private Vector2 input;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(!levelComplete)
        {
            if(!isMoving)
            {
                
            }
        }
    }
}
