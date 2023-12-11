using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Other GameManager code...

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
}
