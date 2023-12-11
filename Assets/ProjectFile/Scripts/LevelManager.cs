// LevelManager.cs
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance {  get; private set; }

    public LevelData CarCount;
    private int finishedCars;
    private int TotalCarToFinish;

    // Property to access finishedCars with both getter and setter
    public int FinishedCars
    {
        get => finishedCars;
        set => finishedCars = value;
    }
    [SerializeField] private GameObject LevelWonWindow;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        finishedCars = 0;
        TotalCarToFinish = CarCount.maxCarCount;
    }

    private void Update()
    {
        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        if(finishedCars >= TotalCarToFinish)
        {
            ActivateLevelWonPanel();
        }
    }

    private void ActivateLevelWonPanel()
    {
        LevelWonWindow.SetActive(true);
    }
}
