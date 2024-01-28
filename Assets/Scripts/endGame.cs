using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endGame : MonoBehaviour
{
    public static bool player1win;
    public GameObject p1ImageWin;
    public GameObject p2ImageWin;

    IEnumerator Start()
    {
        p1ImageWin.SetActive(player1win);
        p2ImageWin.SetActive(!player1win);

        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(0);
    }
}