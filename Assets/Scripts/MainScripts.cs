using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScripts : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {

        SceneManager.LoadScene("Game-1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
