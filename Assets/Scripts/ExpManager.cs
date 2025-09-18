using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpManager : MonoBehaviour
{
    public static ExpManager Instance;

    public delegate void ExpChangeHandler(int amount);
    public event ExpChangeHandler OnExpChange;
    [SerializeField]
    private int maxLevel = 2; // Максимальний рівень
    private PlayerController playerController;
    public void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddExp(int amount)
    {
        if(playerController.currentLevel < maxLevel)
        OnExpChange?.Invoke(amount);
    }
}


