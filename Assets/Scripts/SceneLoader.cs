using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadTutorial()
    {
        SceneManager.LoadScene("Leyu_idea"); 
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Leyu_idea"); 
    }
    
    public void LoadMainMenu()
    {
        Debug.Log("Attempting to load");
        SceneManager.LoadScene("MainMenu");  
    }
    
}