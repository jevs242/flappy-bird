//Jose Velazquez
//GameManager

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void Pause(bool Pause) //Function -> Pause the game
    {
        if(Pause)
        {
            Time.timeScale = 0; //Pause
        }
        else
        {
            Time.timeScale = 1; //Resume
        }
    }

    public void Exit() //Function -> Quit the game
    {
        Application.Quit();
    }
    
    public void Reset() //Function -> Reset the level
    {
        SceneManager.LoadScene("FlappyBird");
    }
}
