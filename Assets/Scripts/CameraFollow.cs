using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTarget;
    [SerializeField] private Vector3 offSetCamera; 
    [SerializeField] private float smoothFactor = 0.5f;
    [SerializeField] private bool lookAtTarget = false;
    // Start is called before the first frame update
    void Start()
    {
        offSetCamera = transform.position - playerTarget.transform.position;
    }

   
    void LateUpdate()
    {
        Vector3 newPosition = playerTarget.transform.position + offSetCamera;
        transform.position = Vector3.Slerp(transform.position, newPosition,smoothFactor);

        if (lookAtTarget)
        {
            transform.LookAt(playerTarget);
        }
    }
}
