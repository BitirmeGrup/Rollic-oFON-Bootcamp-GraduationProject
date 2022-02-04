using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform sideMovementRoot;
    [SerializeField] private Transform leftLimit, rightLimit;
    [SerializeField] private float forwardMovementSpeed = 1f, sideMomentSensitivity = 1f;
    [SerializeField] private float characterSpeed = 5f;
    [SerializeField] private float sideMovementLerpSpeed = 20f;

    private Vector2 inputDrag;
    private Vector2 previousMousePosition;

    private float leftLimitX => leftLimit.localPosition.x; 
    private float rightLimitX => rightLimit.localPosition.x;
    private float sideMovementTarget = 0f;

    private Vector2 mousePositionCM
    {
        get
        {
            Vector2 pixels = Input.mousePosition;
            var inches = pixels / Screen.dpi;
            var centimeters = inches * 2.54f;

            return centimeters;
        }
    }
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
        //transform.Translate(Vector3.forward * forwardMovementSpeed * Time.deltaTime);
        transform.position += transform.forward * Time.deltaTime * characterSpeed; 
    }

    private void HandleSideMovement()
    {
        sideMovementTarget += inputDrag.x * sideMomentSensitivity;
        sideMovementTarget = Mathf.Clamp(sideMovementTarget, leftLimitX, rightLimitX);

        var localPos = sideMovementRoot.localPosition;

        //localPos += Vector3.right * inputDrag.x * sideMomentSensitivity;
        
        localPos.x = Mathf.Lerp(localPos.x, sideMovementTarget, Time.deltaTime * sideMovementLerpSpeed);
        

        sideMovementRoot.localPosition = localPos;

        /*var moveDirection = Vector3.forward * forwardMovementSpeed * Time.deltaTime;
        moveDirection += sideMovementRoot.right * inputDrag.x * sideMomentSensitivity;

        moveDirection.Normalize();

        sideMovementRoot.rotation = Quaternion.LookRotation(moveDirection, Vector3.up); */
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousMousePosition = mousePositionCM;
        }
        if(Input.GetMouseButton(0))
        {
            var deltaMouse = mousePositionCM - previousMousePosition;
            inputDrag = deltaMouse;
            previousMousePosition = mousePositionCM;
        }
        else 
        {
            inputDrag = Vector2.zero;
        }
    }
}
