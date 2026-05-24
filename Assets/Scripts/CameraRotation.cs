using System;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Transform target;

    public float rotateSpeed;
    public float distance;

    private void Start()
    {
        transform.position = target.position -  transform.forward * distance;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(target.position, Vector3.up, rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(target.position, Vector3.up, -rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.RotateAround(target.position, transform.right, rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.RotateAround(target.position, transform.right, -rotateSpeed * Time.deltaTime);
        }
    }
}
