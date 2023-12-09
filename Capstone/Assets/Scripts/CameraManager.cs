using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{


    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    public float smoothTime = 0.25f;
    Vector3 velocity = Vector3.zero;


     void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        
    }
}
