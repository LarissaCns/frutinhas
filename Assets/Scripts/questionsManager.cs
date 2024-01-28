using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class questionsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject questionsOBJ;

    private float maxTime = 10f;
    [SerializeField]
    private float counter = 0;

    public Image[] p1Icons;
    public Image[] p2Icons;

    public int p1Choice = 0;
    public int p2Choice = 0;
    public bool p1Chose = false;
    public bool p2Chose = false;

    private Vector2 moveInput;

    void Start()
    {
        questionsOBJ.SetActive(false);
        counter = maxTime;

        p1Icons[0].enabled = false;
        p1Icons[1].enabled = false;
        p2Icons[0].enabled = false;
        p2Icons[1].enabled = false;
    }

    void Update()
    {
        counter -= Time.deltaTime;

        if(counter < 0)
        {
            counter = maxTime;
            Time.timeScale = 0;
            questionsOBJ.SetActive(true);
        }

        if(p1Chose && p2Chose)
        {
            if(p1Choice == p2Choice)
            {
                StartCoroutine(Unpause());
            }
        }
    }

    IEnumerator Unpause()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        Time.timeScale = 1;
        questionsOBJ.SetActive(false);

        p1Icons[0].enabled = false;
        p1Icons[1].enabled = false;
        p2Icons[0].enabled = false;
        p2Icons[1].enabled = false;

        p1Icons[p1Choice].color = Color.white;
        p2Icons[p2Choice].color = Color.white;

        p1Choice = 0;
        p2Choice = 0;
    }

}
