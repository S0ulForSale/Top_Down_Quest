using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using PixelCrushers.QuestMachine;

public class UIManager : MonoBehaviour
{   
     public GameObject hpHud; // Посилання на об'єкт HpHud в інспекторі

    void Start()
    {
        // Початкове приховання HpHud
        ToggleHpHud(false);
    }

    void Update()
    {
        // Перевірка, чи ведеться діалог
        if (DialogueManager.IsConversationActive)
        {
            // Якщо ведеться діалог, приховати HpHud
            ToggleHpHud(false);
        }
        else
        {
            // Якщо діалог не ведеться, відображати HpHud
            ToggleHpHud(true);
        }
    }

    // Функція для зміни видимості HpHud
    void ToggleHpHud(bool isVisible)
    {
        if (hpHud != null)
        {
            hpHud.SetActive(isVisible);
        }
    }
}
