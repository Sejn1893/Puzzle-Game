using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int currentScene;
    
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    

    public void LoadLevelMenu()
    {
        SceneManager.LoadScene("Level Menu");
        
    }
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level 3");
    }
    public void QuitApp()
    {
        PlayerPrefs.SetInt("levelAt", 2);
        Application.Quit();
    }

    //Level Lock
    public void SetPrefs()
    {
        if (currentScene >= PlayerPrefs.GetInt("levelAt"))
        {
            PlayerPrefs.SetInt("levelAt", currentScene +1);
        }
    }
   
}
