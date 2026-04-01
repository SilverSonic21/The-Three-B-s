using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameUIHandler : MonoBehaviour
{
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartingScreen");
    }
        
    
}
