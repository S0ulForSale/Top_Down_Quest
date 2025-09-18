// using Ink;
// using System.Collections;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

// public class DialogueManager : MonoBehaviour
// {
//     public GameObject dialoguePanel;
//     public TMP_Text dialogueText;
//     public float wordSpeed;
//     public bool playerClose;

//     private Story inkStory;
//     private bool isTyping;

//     void Start()
//     {
//         inkStory = new Story(inkJsonContent); // Завантажте вміст Ink-файлу в зміну inkJsonContent перед запуском сцени
//         inkStory.BindExternalFunction("CloseDialoguePanel", () => CloseDialoguePanel());
//     }

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.E) && playerClose)
//         {
//             if (dialoguePanel.activeInHierarchy)
//             {
//                 CloseDialoguePanel();
//             }
//             else
//             {
//                 dialoguePanel.SetActive(true);
//                 if (!isTyping)
//                     StartCoroutine(StartTyping());
//             }
//         }
//     }

//     public void NextLine()
//     {
//         if (!isTyping)
//         {
//             if (inkStory.canContinue)
//             {
//                 string line = inkStory.Continue();
//                 dialogueText.text = "";
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
//         isTyping = false;
//         inkStory.ResetState();
//     }

//     IEnumerator StartTyping()
//     {
//         isTyping = true;
//         if (inkStory.canContinue)
//         {
//             string line = inkStory.Continue();
//             yield return StartCoroutine(Typing(line));
//         }
//         isTyping = false;
//     }

//     IEnumerator Typing(string line)
//     {
//         foreach (char letter in line)
//         {
//             dialogueText.text += letter;
//             yield return new WaitForSeconds(wordSpeed);
//         }
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             playerClose = true;
//         }
//     }

//     private void OnTriggerExit2D(Collider2D other)
//     {
//         playerClose = false;
//         CloseDialoguePanel();
//     }
// }