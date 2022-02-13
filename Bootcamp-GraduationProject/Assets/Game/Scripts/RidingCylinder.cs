using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidingCylinder : MonoBehaviour
{
   private bool filled;
   private float _value;

<<<<<<< Updated upstream
    public void IncrementCylinderVolume(float value)
=======
<<<<<<< HEAD
public void IncrementCylinderVolume(float value)
{
    _value += value;

    if(_value >= 1)
    {
        float leftValue = _value - 1;
        int cylinderCount = PlayerController.current.cylinders.Count;
        transform.localPosition = new Vector3(transform.localPosition.x, -0.5f * (cylinderCount - 1) -0.25f, transform.localPosition.z);
        transform.localScale = new Vector3(0.5f, transform.localScale.y, 0.5f);
        PlayerController.current.CreateCylinder(leftValue);
    }
    else if(_value < 0)
=======
    public void IncrementCylinderVolume(float value)
>>>>>>> 4b423b30cd8906d37426144098f437d4ba2099be
>>>>>>> Stashed changes
    {
        _value += value;

        if(_value >= 1)
        {
            filled = true;
            float leftValue = _value - 1;
            int cylinderCount = PlayerController.current.cylinders.Count;
            transform.localPosition = new Vector3(transform.localPosition.x, -0.5f * (cylinderCount - 1) -0.25f, transform.localPosition.z);
            transform.localScale = new Vector3(0.5f, transform.localScale.y, 0.5f);
            PlayerController.current.CreateCylinder(leftValue);
            Debug.Log("A Cylinder has been filled");
        }
        else if(_value < 0)
        {
            PlayerController.current.DestroyCylinder(this.gameObject);
            Debug.Log("Cylinder has been destroyed");
        }
        else
        {
            int cylinderCount = PlayerController.current.cylinders.Count;
            transform.localPosition = new Vector3(transform.localPosition.x, -0.5f * (cylinderCount - 1) -0.25f * _value, transform.localPosition.z);
            transform.localScale = new Vector3(0.5f * _value, transform.localScale.y, 0.5f * _value);
        }
        
    }

}
