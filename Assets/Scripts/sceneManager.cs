using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void PlayGame()
    {
        LoadScene(1);
    }

    public void Credits()
    {
        LoadScene(5);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
