using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Splines;

public class CarTapController : MonoBehaviour
{
    private SplineAnimate carAnimation;

    private void Start()
    {
        // Get the SplineAnimate component attached to the car
        carAnimation = GetComponent<SplineAnimate>();

        if (carAnimation == null)
        {
            Debug.LogError("SplineAnimate component not found on the car GameObject.");
        }
    }

    public void PlayAnimation()
    {
        // Play the spline animation
        carAnimation.Play();
    }
}
