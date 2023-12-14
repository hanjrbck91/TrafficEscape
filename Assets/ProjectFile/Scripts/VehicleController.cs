using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using DG.Tweening;

public class VehicleController : MonoBehaviour
{
    public GameManager gameManager;
    public static event Action carCollided;
    public GameObject directionMarkImage;
    public GameObject changedDirection;

    private Rigidbody rb;
    private SplineAnimate anim;
    //private bool carisMoving = false;

    #region Helicopter referencese
    private Vector3 VehiclePosition;
    #endregion

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
        //if(carisMoving)
        //{
        //    Debug.Log("Car is moving so can't move the other car");
        //    return;

        //}
        // Set the position relative to the helicopter's position
        Vector3 offset = new Vector3(0f, 3f, 0f);
        //Setting up the Vehicle position for helicopter to move
        VehiclePosition = this.transform.position;
        VehiclePosition = VehiclePosition + offset;

       
       

        if (GameManager.Instance.currentPowerUps == GameManager.PowerUps.Helicopter)
        {
            MoveHelicopter();
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
    #region Helicopter Functions
    void MoveHelicopter()
    {

        GameManager.Instance.helicopter.transform.position = new Vector3(28.1f, 81f, 180f);
        //GameManager.instance.isCarMoving = true;
        //GameManager.instance.HelicopterCountNumber(); ;
        GameManager.Instance.helicopter.transform.DOMove(VehiclePosition, 3f, false)
            .OnComplete(MovementComplete);
    }

    void MovementComplete()
    {
        this.transform.tag = "Car";
        this.transform.SetParent(GameManager.Instance.helicopter.gameObject.transform);
        this.GetComponent<Rigidbody>().useGravity = false;
         
        #region Carposition related to Helicopter position SettingUp
        Vector3 locationpos = new Vector3(0f, -0.04f, -0.1947f);
        Vector3 localrot = new Vector3(90f, -90f, -90f);

        // Set the local position of the object
        transform.localPosition = locationpos;

        // Set the local rotation of the object
        transform.localRotation = Quaternion.Euler(localrot);

        #endregion

        GameManager.Instance.helicopter.transform.DORotate(new Vector3(-90f, 210f, 0f), 1f);
        GameManager.Instance.helicopter.transform.DOMove(SetHelicoperPosition(), 6f, false).OnComplete(HeliCopterReachedBack);

        // Code to be executed after the movement is complete
        Debug.Log("Helicopter movement is Completed");
    }

    void HeliCopterReachedBack()
    {
        foreach (Transform child in GameManager.Instance.helicopter.transform.GetChild(0).transform)
        {
            if (child.tag == "Car")
                // Deactivate the child object
                child.gameObject.SetActive(false);
        }
        anim.gameObject.SetActive(false);
        // Additional actions, if any
        GameManager.Instance.currentPowerUps = GameManager.PowerUps.none;
        Debug.Log("Helicopter powerup is used");
        //GameManager.Instance.helicopter.transform.DORotate(new Vector3(0f, 7f, 0f), 2f);
        //isHelicopterON = false;
        LevelManager.Instance.FinishedCars++; // Incrementing the finished car number
        //GameManager.instance.isCarMoving = false;
        //GameManager.instance.isHelicopterModeOn = false;
      
    }

    private Vector3 SetHelicoperPosition()
    {
        // Customize this method based on your sky position requirements
        float randomX = 10.97f; //UnityEngine.Random.Range(0f, 1f);
        float randomY = 95.1f;//UnityEngine.Random.Range(25f, 25f);
        float randomZ = 180.2f; //UnityEngine.Random.Range(50f, 60f);

            return new Vector3(randomX, randomY, randomZ); 
    }

    #endregion

    #region Sign-Reverse Functions
    [SerializeField]
    SplineContainer Reversespline;
    [SerializeField]
    SplineContainer currentspline;
    public void ChangeSplineContainer()
    {
        anim.Container = Reversespline;
        directionMarkImage.SetActive(false);
        changedDirection.SetActive(true);

        GameManager.Instance.currentPowerUps = GameManager.PowerUps.none;
        Debug.Log("Reverse PowerUp is used");   // Assuming you want to reset the power-up

    }
    #endregion

    public void MoveVehicle()
    {
        DriveVehicle();
        CollisionCheck();
    }

    public void DriveVehicle()
    {
        rb.isKinematic = false;
        anim.Restart(true);
        //carisMoving = true;
        Debug.Log("Car is Moving");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CarCounter"))
        {
            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.FinishedCars++;
                Debug.Log(LevelManager.Instance.FinishedCars);
                SplineContainer maincontainer = anim.Container;
                maincontainer.gameObject.SetActive(false);
                gameObject.SetActive(false);
                //carisMoving = false;
                Debug.Log("Car movement finished");
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
            //carisMoving = true;
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
            //carisMoving = false;
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
