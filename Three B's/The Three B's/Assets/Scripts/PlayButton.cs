using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class PlayButton : MonoBehaviour
{
 
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Level1");
    }


    
}
