using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController current;

    //--------------------------------------------------------------------------------------------
    [SerializeField] private Transform sideMovementRoot;
    [SerializeField] private Transform leftLimit, rightLimit;
    [SerializeField] private float forwardMovementSpeed = 1f, sideMomentSensitivity = 1f;
    public float characterSpeed = 5f;
    [SerializeField] private float sideMovementLerpSpeed = 20f;

    private Vector2 inputDrag;
    private Vector2 previousMousePosition;

    private float leftLimitX => leftLimit.localPosition.x; 
    private float rightLimitX => rightLimit.localPosition.x;
    private float sideMovementTarget = 0f;
    //--------ee

    Vector3 localPos;

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
//-------------------------------------------------------------------------------------------

    public GameObject ridingCylinderPrefab;
    public List<GameObject> cylinders;
    public static int scoreNew;
    void Start()
    {
        current = this;
        
    }

    
    void Update()
    {
        /*----------------------------
        if(LevelController.Current == null || !LevelController.Current.gameActive)
        {
            return;
        }
        */
        HandleForwardMovement();
        HandleInput();
        HandleSideMovement();

        AllignCylinderWithPlayer();
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

        // asıl olan var localPos = sideMovementRoot.localPosition;
        localPos = sideMovementRoot.localPosition;

        //localPos += Vector3.right * inputDrag.x * sideMomentSensitivity;

        localPos.x = Mathf.Lerp(localPos.x, sideMovementTarget, Time.deltaTime * sideMovementLerpSpeed);




        sideMovementRoot.localPosition = localPos;

        ////----e------
        //if(cylinders.Count >= 0)
        //{
        //    for(int i = 0; i<cylinders.Count; i++)
        //    {
        //    var localPosCylider1 = cylinders[i].gameObject.transform.localPosition;
        //    localPosCylider1.x = Mathf.Lerp(localPos.x+ 1.5f, sideMovementTarget, Time.deltaTime * sideMovementLerpSpeed);
        //    cylinders[i].transform.localPosition = localPosCylider1;
        //    }
        //}

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

//-------------------------------------------------------------------------------------------------
   

    private void OnTriggerEnter(Collider other) 
    {
        //Debug.Log(other.tag.ToString());
        if (other.tag == "AddSteelbar")
        {
            IncrementCylinderVolume(0.1f);
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if(other.tag == "ObstructRoadcone")
        {
            IncrementCylinderVolume(-Time.fixedDeltaTime);
        }    
        if(other.tag == "ObstructBrick")
        {
            IncrementCylinderVolume(-Time.fixedDeltaTime);
        }
        if(other.tag == "ObstructBarrier")
        {
            IncrementCylinderVolume(-Time.fixedDeltaTime);
        } 
    }

    public void IncrementCylinderVolume(float value)
    {
        if(cylinders.Count == 0)
        {
            if(value > 0)
            {
                CreateCylinder(value);
            }
            else
            {
                //gameover
            }
        }
        else
        {
            cylinders[cylinders.Count - 1].GetComponent<RidingCylinder>().IncrementCylinderVolume(value);
        }
    }

    public void AllignCylinderWithPlayer()
    {
        foreach (GameObject cylinder in cylinders)
        {

            float xOfCylinder = sideMovementRoot.position.x;
            float yOfCylinder = cylinder.transform.position.y;
            float zOfCylinder = cylinder.transform.position.z;


            cylinder.transform.position = new Vector3(xOfCylinder, yOfCylinder, zOfCylinder);
        }
    }


    public void CreateCylinder(float value)
    {
        GameObject createdCylinder = Instantiate(ridingCylinderPrefab, transform);
        cylinders.Add(createdCylinder);
        createdCylinder.GetComponent<RidingCylinder>().IncrementCylinderVolume(value);
       // Debug.Log(cylinders.Count);
    }

    public void DestroyCylinder(GameObject cylinder)
    {
        cylinders.Remove(cylinder);
        Destroy(cylinder.gameObject);
    }

    //--------------------------------------------------------------------------------------------------

    public void ChangeSpeed(float value)
    {
        characterSpeed = value;
    }

    
}
