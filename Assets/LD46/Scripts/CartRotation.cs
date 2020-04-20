using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartRotation : MonoBehaviour
{

    public float maxSpeedXRotation = 15.0f;
    public float maxSpeedYRotation = 15.0f;
    public float minSpeedXRotation = 5.0f;
    public float minSpeedYRotation = 5.0f;
    // Start is called before the first frame update

    private float xRotation = 0f, yRotation = 0f;
    void Start()
    {
        xRotation = Random.Range(minSpeedXRotation, maxSpeedXRotation);
        yRotation = Random.Range(minSpeedYRotation, maxSpeedYRotation);
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        gameObject.transform.Rotate(Vector3.up, xRotation, Space.World);
        gameObject.transform.Rotate(Vector3.forward, yRotation, Space.World);
    }
}
