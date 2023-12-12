using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class VehicleController : MonoBehaviour
{
    public GameManager gameManager;
    public static event Action carCollided;
    public GameObject directionMarkImage;
    public GameObject changedDirection;

    private Rigidbody rb;
    private SplineAnimate anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<SplineAnimate>();

        // Find and store the GameManager script reference
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager script not found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.currentPowerUps == GameManager.PowerUps.Helicopter)
        {
            // Handle helicopter power-up logic
        }
        else if (GameManager.Instance.currentPowerUps == GameManager.PowerUps.ReverseDirection)
        {
            ChangeSplineContainer();
            Debug.Log("SplineDirection is Changed");
            // Handle switch direction power-up logic
        }
        else
        {
            // Default logic when no power-up is active
            MoveVehicle();
        }
    }
    [SerializeField]
    SplineContainer Reversespline;
    [SerializeField]
    SplineContainer currentspline;
    public void ChangeSplineContainer()
    {
        // Rotate the car by 180 degrees
        //if(anim.Container != Reversespline)
        //{
        //    Vector3 carRotation = transform.rotation.eulerAngles;
        //    carRotation.y += 180f;
        //    transform.rotation = Quaternion.Euler(carRotation);
        //}
        anim.Container = Reversespline;
        directionMarkImage.SetActive(false);
        changedDirection.SetActive(true);
            //directionMarkImage.GetComponent<SpriteRenderer>().sprite = SymbolManager.GetSymbol(symbolsType);
            GameManager.Instance.currentPowerUps = GameManager.PowerUps.none; // Assuming you want to reset the power-up
      
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
            changedDirection.SetActive(false);
            GetComponent<BoxCollider>().isTrigger = true;
            GetComponent<VehicleController>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        var otherCar = collision.transform.GetComponent<VehicleController>();
        if (otherCar)
        {
            Debug.Log("Collision");
            carCollided?.Invoke();
            //Destroy(carDriveSound);
            directionMarkImage.SetActive(true);

            anim.Pause();
            StartCoroutine(ReverseAnimation());
            gameManager.TakeDamage();
            Debug.Log("20 damage is taken due hitting other car");
        }
        if (collision.transform.name.Contains("Pedestrian"))
        {
            carCollided?.Invoke();
            directionMarkImage.SetActive(true);
            anim.Pause();
            StartCoroutine(ReverseAnimation());
            gameManager.TakeDamage();
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
