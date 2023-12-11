using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class VehicleController : MonoBehaviour
{
    public static event Action carCollided;
    public GameObject directionMarkImage;

    private Rigidbody rb;
    private SplineAnimate anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<SplineAnimate>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        MoveVehicle();
    }

    public void MoveVehicle()
    {
        DriveVehicle();
        CollisionCheck();
    }

    public void DriveVehicle()
    {
        rb.isKinematic = false;
        anim.Restart(true);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CarCounter"))
        {
            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.FinishedCars++;
                SplineContainer maincontainer = anim.Container;
                maincontainer.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("LevelManager.Instance is null. Make sure it's properly instantiated.");
            }
        }
    }

    void CollisionCheck()
    {
        if (anim.Container.gameObject.GetComponent<VehicleDetector>().CheckIfPathIsClear())
        {
            directionMarkImage.SetActive(false);
            GetComponent<BoxCollider>().isTrigger = true;
            GetComponent<VehicleController>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        var otherCar = collision.transform.GetComponent<VehicleController>();
        if (otherCar)
        {
            carCollided?.Invoke();
            //Destroy(carDriveSound);
            directionMarkImage.SetActive(true);

            anim.Pause();
            StartCoroutine(ReverseAnimation());
            GameManager.Instance.TakeDamage();
            Debug.Log("20 damage is taken due hitting other car");
        }
        if (collision.transform.name.Contains("Pedestrian") || collision.transform.name.Contains("Bus"))
        {
            carCollided?.Invoke();
            directionMarkImage.SetActive(true);
            anim.Pause();
            StartCoroutine(ReverseAnimation());
            GameManager.Instance.TakeDamage();
            Debug.Log("20 damage is taken");
        }
    }

    private IEnumerator ReverseAnimation()
    {
        float duration = anim.ElapsedTime;
        float elapsedTime = 0.001f;

        while (elapsedTime < duration && anim.ElapsedTime > 0f)
        {
            anim.ElapsedTime -= Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
    }
}
