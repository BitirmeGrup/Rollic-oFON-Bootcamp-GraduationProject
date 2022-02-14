using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip coinSound, cylenderSound;
    
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Coin"))
        {
            transform.GetComponent<AudioSource>().PlayOneShot(coinSound);        
        }    
        else if(other.CompareTag("AddSteelbar"))
        {
            transform.GetComponent<AudioSource>().PlayOneShot(cylenderSound); 
        }
    }
}
