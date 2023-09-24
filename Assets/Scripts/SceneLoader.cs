using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Leyu_idea"); 
    }
    
    public void LoadMainMenu()
    {
        Debug.Log("Attempting to load");
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        SceneManager.LoadScene("MainMenu");  
    }

    public void LoadHardLevel()
    {
        SceneManager.LoadScene("HardLevel");
    }
}