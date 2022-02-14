using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLevelController : MonoBehaviour
{
    int finishCounterLevel = 0;
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Finish"))
        {
            
            Debug.Log("level "+ finishCounterLevel);
            PlayerPrefs.SetInt("level", finishCounterLevel);
            StartCoroutine(finishDance());
            
        }

    }

    IEnumerator finishDance()
    {
        ++finishCounterLevel ;
        SceneManager.LoadScene(1);
        yield return new WaitForSeconds(4f);
    }

    void Start()
    {
        
        finishCounterLevel = PlayerPrefs.GetInt("level");
    }
}
