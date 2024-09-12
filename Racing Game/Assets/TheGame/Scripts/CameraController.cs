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

    private void Start()
    {
        virtualCamera.m_Lens.OrthographicSize = maxFOV;
    }

    // Update is called once per frame
    void Update()
    {
        if(virtualCamera != null && carController != null)
        {
            var t = Mathf.Abs(carController.currentSpeed / carController.acceleration);
            var targetFieldOfView =
                Mathf.Lerp(minFOV, maxFOV,
                t);
            virtualCamera.m_Lens.OrthographicSize =
                Mathf.MoveTowards(virtualCamera.m_Lens.OrthographicSize,
                targetFieldOfView, zoomSpeed * Time.deltaTime);
        }
    }
}
