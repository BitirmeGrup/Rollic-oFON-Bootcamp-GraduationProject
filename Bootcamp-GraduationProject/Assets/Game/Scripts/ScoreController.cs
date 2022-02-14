using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    int yAxis = 0; 
    //int score;
    [SerializeField] private Text scoreText;

    
    void Start()
    {
        
    }
    void Update()
    {
        transform.rotation = Quaternion.Euler( transform.rotation.x,yAxis++, transform.rotation.z) ;
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            PlayerController.scoreNew += 10;
            scoreText.text = PlayerController.scoreNew.ToString();
        }
    }


}
