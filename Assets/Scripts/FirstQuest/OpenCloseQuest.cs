using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCloseQuest : MonoBehaviour
{
    public GameObject quest;
    public GameObject password;

    public void OpenQust()
    {
        quest.SetActive(true);
    }

    public void CloseQuest()
    {
        quest.SetActive(false);
    }

    public void ShowPassword()
    {
       password.SetActive(true);
    }

    public void ClosePassword()
    {
       password.SetActive(false);
    }
}
