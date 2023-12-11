using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class VehicleController : MonoBehaviour
{
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
    }

    public void DriveVehicle()
    {
        rb.isKinematic = false;
        anim.Restart(true);
    }


}
