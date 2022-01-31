using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform sideMomentRoot;
    [SerializeField] private Transform leftLimit, rightLimit;
    [SerializeField] private float forwardMovementSpeed = 1f, sideMomentSensitivity = 1f;
    private Vector2 inputDrag;
    private Vector2 previousMousePosition;

    private float leftLimitX => leftLimit.localPosition.x; 
    private float rightLimitX => rightLimit.localPosition.x;
    void Start()
    {
        
    }

    
    void Update()
    {
        HandleForwardMovement();
        HandleInput();
        HandleSideMovement();
    }

    private void HandleForwardMovement()
    {
        transform.Translate(Vector3.forward * forwardMovementSpeed * Time.deltaTime);
    }

    private void HandleSideMovement()
    {
        var localPos = sideMomentRoot.localPosition;
        localPos += Vector3.right * inputDrag.x * sideMomentSensitivity;
        
        localPos.x = Mathf.Clamp(localPos.x, leftLimitX, rightLimitX);
        

        sideMomentRoot.localPosition = localPos;
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousMousePosition = Input.mousePosition;
        }
        if(Input.GetMouseButton(0))
        {
            var deltaMouse = (Vector2)Input.mousePosition - previousMousePosition;
            inputDrag = deltaMouse;
            previousMousePosition = Input.mousePosition;
        }
        else 
        {
            inputDrag = Vector2.zero;
        }
    }
}
