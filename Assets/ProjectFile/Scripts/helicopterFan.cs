using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helicopterFan : MonoBehaviour
{
    [SerializeField] float fanSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, fanSpeed * Time.deltaTime);
    }
}
