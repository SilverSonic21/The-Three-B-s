using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene loading

public class GameUIHandler : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1f; // IMPORTANT: Unfreezes the game
        
        // Reloads the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
