using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class openDoor : MonoBehaviour
{
    public GameObject Instruction;
    public GameObject AnimeObject;
    public GameObject ThisTrigger;
    public bool Action = false;
    public Button Door;

    void Start()
    {
        Instruction.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Instruction.SetActive(true);
            Action = true;
            Debug.Log("onTriger");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Instruction.SetActive(false);
        Action = false;
    }

    public void click()
    {
        if (Action == true)
        {
            Instruction.SetActive(false);
            AnimeObject.GetComponent<Animator>().Play("door_open");
            ThisTrigger.SetActive(false);
            Action = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Action == true)
            {
                Instruction.SetActive(false);
                AnimeObject.GetComponent<Animator>().Play("door_open");
                ThisTrigger.SetActive(false);
                Action = false;
            }
        }

    }
}