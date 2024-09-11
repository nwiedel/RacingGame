using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] CarController carController;
    [SerializeField] float minFOV, maxFOV; // FOV = field of view
    [SerializeField] float zoomSpeed;

    // Update is called once per frame
    void Update()
    {
        if(virtualCamera != null && carController != null)
        {
            var targetFieldOfView =
                Mathf.Lerp(minFOV, maxFOV,
                carController.currentSpeed / carController.acceleration);
            virtualCamera.m_Lens.OrthographicSize =
                Mathf.MoveTowards(virtualCamera.m_Lens.OrthographicSize,
                targetFieldOfView, zoomSpeed);
        }
    }
}
