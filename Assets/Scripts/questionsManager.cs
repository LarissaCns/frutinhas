using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class questionsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject questionsOBJ;

    private float maxTime = 10f;
    [SerializeField]
    private float counter = 0;

    void Start()
    {
        questionsOBJ.SetActive(false);
        counter = maxTime;
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
    }
}
