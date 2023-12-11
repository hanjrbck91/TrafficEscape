using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    [SerializeField] private GameObject GameOver;

    [SerializeField] private Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        // Initialize the UI slider
        UpdateHealthUI();
    }
    // Function to handle taking damage
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        // Ensure health doesn't go below zero
        currentHealth = Mathf.Max(currentHealth, 0f);
        Debug.Log(currentHealth);

        // Update the UI slider
        UpdateHealthUI();

        // Check if the player is dead
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    // Function to update the UI slider
    private void UpdateHealthUI()
    {
        // Ensure the health slider reference is set
        if (healthSlider != null)
        {
            // Normalize the health value between 0 and 1
            float normalizedHealth = currentHealth / maxHealth;

            Debug.Log(normalizedHealth);
            // Update the slider value
            healthSlider.value = normalizedHealth;
        }
    }

    // Function to handle player death (you can customize this)
    private void Die()
    {
        // For now, simply log a message
        Debug.Log("Player has died!");
        GameOver.SetActive(true);
    }
}
