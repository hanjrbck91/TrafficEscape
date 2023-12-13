using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterPropeller : MonoBehaviour
{

    [SerializeField] float fanSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, fanSpeed * Time.deltaTime, 0);
    }
}
