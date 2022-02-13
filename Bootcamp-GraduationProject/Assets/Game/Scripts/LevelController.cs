using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelController : MonoBehaviour
{
    public static LevelController Current;
    public bool gameActive = false;
    public GameObject startMenu, gameMenu, gameOverMenu, finishMenu;
    public Text scoreText, finishScoreText, currentLevelText, nextLevelText;
    int currentLevel;
    int score;
    public Slider levelProgressBar;
    private float maxDistance,distance;
    public GameObject finishLine;

    

///-----------e
    [SerializeField] private GameObject player;
    PlayerController playercontroller ;

    private void Awake() 
    {
        //-----e
        playercontroller = player.GetComponent<PlayerController>();
        playercontroller.enabled = false;

    }
    void Start()
    {
        Current = this;
 
        currentLevel = PlayerPrefs.GetInt("currentLevel");
        if(SceneManager.GetActiveScene().name != "Level "+ currentLevel)
        {
            SceneManager.LoadScene("Level " + currentLevel);
        }
        else
        {
            currentLevelText.text = (currentLevel + 1).ToString();
            nextLevelText.text = (currentLevel + 2).ToString();

        }
        
    }

    
    void Update()
    {
        if(gameActive)
        {
            PlayerController player = PlayerController.current;
            distance = finishLine.transform.position.z - PlayerController.current.transform.position.z;
            
            //levelProgressBar.value += (distance/maxDistance);
           // Debug.Log("DİSTANCE" + distance);
           maxDistance = finishLine.transform.position.z - PlayerController.current.transform.position.z;//uzaklıkları bulmak
           // Debug.Log("MaxDİSTANCE" + maxDistance);
           StartCoroutine(slideNumeretor());
        }
    }



    IEnumerator slideNumeretor()
    {
        if(distance > 0)
        {
            yield return new WaitForSeconds(1f);
            levelProgressBar.value += 0.035f;
            
        }
    }



    public void StartLevel()
    {

        if(playercontroller.enabled == true)
        {
        maxDistance = finishLine.transform.position.z - PlayerController.current.transform.position.z;//uzaklıkları bulmak
        
        }

        playercontroller.enabled = true;

        if(playercontroller.enabled == true)
        {

        //playercontroller.Current.ChangeSpeed(playercontroller.Current.characterSpeed);
        startMenu.SetActive(false);
        gameMenu.SetActive(true);
        gameActive = true;
       
        int currentLevel = PlayerPrefs.GetInt("currentLevel");
        if(SceneManager.GetActiveScene().name != "Level "+ currentLevel)
        {
            SceneManager.LoadScene("Level " + currentLevel);
        }
        else
        {
            currentLevelText.text = (currentLevel + 1).ToString();
            nextLevelText.text = (currentLevel + 2).ToString();

        }

        }
        else
        {
            Debug.Log("beklee");
        }
        
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("Level " + (currentLevel + 1));
    }

    public void GameOver()
    {
        gameMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        gameActive = false;
    }

    public void FinishGame()
    {
        PlayerPrefs.SetInt("currentLevel", currentLevel + 1);
        finishScoreText.text = score.ToString();
        gameMenu.SetActive(false);
        finishMenu.SetActive(true);
        gameActive = false;
    }

    public void ChangeScore(int increment)
    {
        score += increment;
        scoreText.text = score.ToString();
    }
}
