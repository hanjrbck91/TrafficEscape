using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public LevelData CarCount;
    private int finishedCars;
    private int TotalCarToFinish;
    [SerializeField] private GameObject confetteprefab;
    private bool levelwon = false;
    public ParticleSystem confette;

    // Property to access finishedCars with both getter and setter
    public int FinishedCars
    {
        get => finishedCars;
        set => finishedCars = value;
    }

    [SerializeField] private GameObject LevelWonWindow;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        levelwon = false;
    }

    void Start()
    {
        finishedCars = 0;
        TotalCarToFinish = CarCount.maxCarCount;
    }

    private void Update()
    {
        Invoke("StopParticleeffect", 5);
        CheckWinCondition();
        if (levelwon)
        {
            confette.Play();
            //confette.Emit(200);
        }

    }

    private void StopParticleeffect()
    {
        levelwon = false;
        confette.Stop();
    }

    private void CheckWinCondition()
    {
        if (finishedCars >= TotalCarToFinish)
        {
            ActivateLevelWonPanel();
        }
    }

    private void ActivateLevelWonPanel()
    {
        LevelWonWindow.SetActive(true);
        levelwon = true;

    }
}