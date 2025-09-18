using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenComputer : MonoBehaviour
{
    public GameObject computerButton;
    public GameObject computer;
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            computerButton.SetActive(true);
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            computerButton.SetActive(false);
        }
    }
    public void OpenComp()
    {
        computer.SetActive(true);
        computerButton.SetActive(false);
    }

    public void CloseComp()
    {
        computer.SetActive(false);
    }

}
