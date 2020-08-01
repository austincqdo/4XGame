using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Func<Vector3> GetCameraFollowPosition;

    public void Setup(Func<Vector3> GetCameraFollowPosition)
    {
    	this.GetCameraFollowPosition = GetCameraFollowPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraFollowPosition = GetCameraFollowPosition();
        cameraFollowPosition.z = transform.position.z;
        
        Vector3 cameraMoveDir = (cameraFollowPosition - transform.position).normalized;
        float distance = Vector3.Distance(cameraFollowPosition, transform.position);
        float cameraMoveSpeed = 5f;

        transform.position = transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;
    }
}
