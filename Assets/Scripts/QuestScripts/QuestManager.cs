using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public List<PlayerQuest> quests = new List<PlayerQuest>();
    public GameObject questWindow; // Посилання на вікно квестів в ієрархії об'єктів

    private void Start()
    {
        // Ініціалізація списку квестів
        quests.Add(new PlayerQuest("Quest 1", "Description 1"));
        quests.Add(new PlayerQuest("Quest 2", "Description 2"));
        // Додайте інші квести, які вам потрібні
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // Приклад взаємодії зі світом гри (виглядає, якщо гравець натиснув "Q")
        {
            StartQuest("Quest 1");
        }

        // Оновлення вікна з квестами
        UpdateQuestWindow();
    }

    public void StartQuest(string questTitle)
    {
        PlayerQuest quest = quests.Find(q => q.title == questTitle);
        if (quest != null)
        {
            quest.state = QuestState.Active;
        }
    }

    // Інші методи, які вам можуть знадобитися, наприклад, для завершення квесту

    private void UpdateQuestWindow()
    {
        // Оновлення вікна з квестами на основі стану квестів
        foreach (PlayerQuest quest in quests)
        {
            string questStatus = GetQuestStatus(quest);
            // Відображення questStatus у вікні квестів
        }
    }

    private string GetQuestStatus(PlayerQuest quest)
    {
        switch (quest.state)
        {
            case QuestState.Inactive:
                return "Inactive";
            case QuestState.Active:
                return "Active";
            case QuestState.Completed:
                return "Completed";
            default:
                return "";
        }
    }
}

public class PlayerQuest
{
    public string title;
    public string description;
    public QuestState state;

    public PlayerQuest(string title, string description)
    {
        this.title = title;
        this.description = description;
        this.state = QuestState.Inactive;
    }
}

public enum QuestState
{
    Inactive,
    Active,
    Completed
}

