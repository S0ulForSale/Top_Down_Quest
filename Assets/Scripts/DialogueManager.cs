// using Ink;
// using Ink.Runtime;
// using System.Collections;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

// public class DialogueManager : MonoBehaviour
// {
//     public GameObject dialoguePanel;
//     public PlayerController playerController;
//     public TMP_Text dialogueText;
//     public float wordSpeed;
//     public float readTime;
//     public bool playerClose;

//     private Story inkStory;
//     public TextAsset inkFile;
//     public GameObject startDialogueButton; 
    
//     private bool isTyping;
//     private bool autoPlayDialogues = true;
//     private bool isPlayingDialogue = false;
//     private bool isDialogOngoing = false; // вказуватиме на те, чи вже був розпочатий діалог
//     private bool readyToStartDialogue = false;
//     private bool startDialogue = false;

//     void Start()
//     {
//         inkStory = new Story(inkFile.text); 
//         inkStory.BindExternalFunction("CloseDialoguePanel", () => CloseDialoguePanel());
//         startDialogueButton = GameObject.Find("StartDialogueButton"); //
//         startDialogueButton.SetActive(false);
//     }

//     void Update()
//     {
//         if (startDialogue && Input.GetButtonDown("MyDialogueButton"))
//         {
//             if (!dialoguePanel.activeInHierarchy)
//             {
//                 dialoguePanel.SetActive(true);
//                 if (!isTyping)
//                     StartCoroutine(StartTyping());
//             }
//         }
//         else //блок коду для зупинки автоматичного відтворення
//         {
//             if (dialoguePanel.activeInHierarchy)
//                 CloseDialoguePanel();
//         }
//     }

    
//     public void StartDialogue()
//     {
//         if (!dialoguePanel.activeInHierarchy)
//         {
//             dialoguePanel.SetActive(true);
//             if (!isTyping)
//                 StartCoroutine(StartTyping());
//                 playerController.enabled = false;
//         }
//     }

//     public void NextLine()
//     {
//         if (!isTyping)
//         {
//             if (inkStory.canContinue)
//             {
//                 string line = inkStory.Continue();
//                 StartCoroutine(Typing(line));
//             }
//             else
//             {
//                 CloseDialoguePanel();
//             }
//         }
//     }

//     public void CloseDialoguePanel()
//     {
//         dialoguePanel.SetActive(false);
//         // isTyping = false;
//         inkStory.ResetState();
//         playerController.enabled = true;
//     }

//     IEnumerator StartTyping()
//     {
//         isTyping = true;
//         while (autoPlayDialogues && inkStory.canContinue)
//         {
//             string line = inkStory.Continue();
//             yield return Typing(line);
//             yield return new WaitForSeconds(readTime); //пауза після закінчення речення
//         }
//         CloseDialoguePanel(); // Закриваємо панель після завершення всіх діалогів
//         isTyping = false;
//     }

//     IEnumerator Typing(string line)
//     {
//         dialogueText.text = "";
//         int index = 0;
//         while (index < line.Length)
//         {
//             dialogueText.text += line[index];
//             index++;
//             yield return new WaitForSeconds(wordSpeed);
//         }

//         yield return new WaitForSeconds(readTime); 

//         if (inkStory.canContinue)
//         {
//             NextLine(); //  наступне речення після закінчення поточного
//         }
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             startDialogue = true;
//             // Активуємо кнопку для початку діалогуd 
//             startDialogueButton.SetActive(true);
//         }
//     }

//     private void OnTriggerExit2D(Collider2D other)
//     {
//          if (other.CompareTag("Player"))
//         {
//             startDialogue = false;
//             if (dialoguePanel.activeInHierarchy)
//                 CloseDialoguePanel();
//                 startDialogueButton.SetActive(false);
//         }
//     }

// }