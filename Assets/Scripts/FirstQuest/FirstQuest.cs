using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirstQuest : MonoBehaviour
{
    public TMP_Text Text;
    private int codeLength = 4; // Довжина коду, яку можна ввести
    public LvlManager lvlManager;

    public void One()
    {
        Text.text = Text.text + "1 ";
        CheckCode();
    }
    public void Two()
    {
        Text.text = Text.text + "2 ";
        CheckCode();
    }
    public void Tree()
    {
        Text.text = Text.text + "3 ";
        CheckCode();
    }
    public void Four()
    {
        Text.text = Text.text + "4 ";
        CheckCode();
    }
    public void Five()
    {
        Text.text = Text.text + "5 ";
        CheckCode();
    }
    public void Six()
    {
        Text.text = Text.text + "6 ";
        CheckCode();
    }
    public void Seven()
    {
        Text.text = Text.text + "7 ";
        CheckCode();
    }
    public void Eight()
    {
        Text.text = Text.text + "8 ";
        CheckCode();
    }
    public void Nine()
    {
        Text.text = Text.text + "9 ";
        CheckCode();
    }
    private void AddDigit(string digit)
    {
        if (Text.text.Length < codeLength * 2) // Кожна цифра має пробіл після себе, тому ми використовуємо множимо на 2 
        {
            Text.text += digit + " ";
            CheckCode();
        }
    }
    void CheckCode()
    {
        if (Text.text.Length == codeLength * 2) // Перевірка, чи введено не більше 4 цифр
        {
            string enteredCode = Text.text.Replace(" ", ""); // Видаляємо пробіли для порівняння

            if (enteredCode == "1234") // Змініть "1234" на свій код
            {
                Debug.Log("Quest Complete");
                UnlockDoor();
            }
            else
            {
                Debug.Log("Ne tei kod");
            }

           // Text.text = ""; // Очистка поля після перевірки
           Invoke("ClearText", 0.3f);
        }
    }
    void UnlockDoor()
    {
        if (lvlManager != null)
        {
            lvlManager.isLocked = false; // Звертаємося до компонента через посилання lvlManager
            // Додайте код для відображення повідомлення гравцю про те, що двері розблоковано
        }
    }
    void ClearText()
    {
        Text.text = "";
    }
}
