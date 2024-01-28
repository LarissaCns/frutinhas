using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class charSelect : MonoBehaviour
{
    [SerializeField] GameObject P1Icon;
    [SerializeField] GameObject P2Icon;
    private int level;
    [SerializeField] Button[] buttons;

    public void Start()
    {
        P1Icon.SetActive(false);
        P2Icon.SetActive(false);
    }

    public void Select(InputAction.CallbackContext value)
    {

    }

    public void Confirm(InputAction.CallbackContext value)
    {

    }

    public void Level1()
    {
        level = 2;
        buttons[0].enabled = false;
        buttons[0].GetComponent<Transform>().GetChild(0).gameObject.SetActive(true);
    }
    public void Level2()
    {
        level = 3;
        buttons[1].enabled = false;
        buttons[1].GetComponent<Transform>().GetChild(1).gameObject.SetActive(true);
    }
}
