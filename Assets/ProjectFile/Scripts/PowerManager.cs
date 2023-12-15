using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
    public static PowerManager Instance { get; private set; }

    public GameObject ReverseDirectionPanel;
    public GameObject HelicopterPanel;

    [SerializeField] int ReversePowerUp = 5;

    private void Start()
    {
        ReverseDirectionPanel.SetActive(false);
        HelicopterPanel.SetActive(false);
        Instance = this;
    }
    public void OnReverseButtonPressed()
    {
        GameManager.Instance.currentPowerUps = GameManager.PowerUps.ReverseDirection;
        ReverseDirectionPanel?.SetActive(true);
        Debug.Log("ReversePowerUp is activated");
    }

    public void OnHelicopterButtonPressed()
    {
        GameManager.Instance.currentPowerUps = GameManager.PowerUps.Helicopter;
        HelicopterPanel?.SetActive(true);
        Debug.Log("helicopter PowerUp is activated");
    }

   public void DisableHelicopterPanel()
    {
        Debug.Log("Disabling Helicopter Panel");
        HelicopterPanel.SetActive(false);
        GameManager.Instance.currentPowerUps = GameManager.PowerUps.none;
    }
    public void DisableReverseDirectionPanel()
    {
        Debug.Log("Disabling Reverse Direction Panel");
        ReverseDirectionPanel.SetActive(false);
        GameManager.Instance.currentPowerUps = GameManager.PowerUps.none;
    }
}
