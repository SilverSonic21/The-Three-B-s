using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    private int nextSceneToLoad;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(nextSceneToLoad);
    }
}
