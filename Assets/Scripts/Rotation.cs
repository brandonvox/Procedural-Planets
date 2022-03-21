using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private Transform _transform;
    public float amountRotationEachFrame = 10f;
    private void Awake()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        _transform.Rotate(Vector3.up, amountRotationEachFrame * Time.deltaTime); 
    }
}
