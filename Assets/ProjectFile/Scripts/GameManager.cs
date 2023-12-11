using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public HealthManager healthmanager;

    // Ensure GameManager is a proper singleton
    private void Awake()
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

    // Called when the application is about to quit
    private void OnApplicationQuit()
    {
        // Reset the game state or perform cleanup here
        ResetGameState();
    }

    // Custom method to reset the game state
    private void ResetGameState()
    {
        // Add your code here to reset the game state to its initial state
        // For example, reset scores, positions, or any other game-related data

        Debug.Log("Game state reset before quitting.");
    }

    public void NextLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void RestartLevel()
    {
        // Get the current scene index and reload it
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void TakeDamage()
    {
        healthmanager.TakeDamage(5);
    }
}
