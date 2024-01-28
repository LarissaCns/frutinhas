using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
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
        LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
