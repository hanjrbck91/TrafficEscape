using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
    [SerializeField] int ReversePowerUp = 5;
    public void OnReverseButtonPressed()
    {
        GameManager.Instance.currentPowerUps = GameManager.PowerUps.ReverseDirection;
        Debug.Log("ReversePowerUp is activated");
    }

    public void OnHelicopterButtonPressed()
    {
        GameManager.Instance.currentPowerUps = GameManager.PowerUps.Helicopter;
        Debug.Log("helicopter PowerUp is activated");
    }
}
