using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class PositionLockCameraController : MonoBehaviour
{
    public Transform target; // Reference to the target object

    private Camera managedCamera;

    private void Awake()
    {
        managedCamera = gameObject.GetComponent<Camera>();
    }

    // Use the LateUpdate message to avoid setting the camera's position before
    // GameObject locations are finalized.
    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target object not assigned to PositionLockCameraController!");
            return;
        }

        // Get the target position and set the camera position accordingly
        Vector3 targetPosition = target.position;
        managedCamera.transform.position = targetPosition;
    }

}