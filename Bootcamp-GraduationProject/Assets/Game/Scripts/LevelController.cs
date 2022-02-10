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

    
    void Update()
    {
        
    }

    public void StartLevel()
    {
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

}
