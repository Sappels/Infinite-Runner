using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera[] virtualCameras; // Array to hold all virtual cameras

    // Call this function with the desired camera index
    public void SwitchCamera(int cameraIndex)
    {
        // Ensure the index is within bounds
        if (cameraIndex < 0 || cameraIndex >= virtualCameras.Length)
        {
            Debug.LogWarning("Camera index out of range!");
            return;
        }

        // Disable all cameras first
        foreach (CinemachineVirtualCamera cam in virtualCameras)
        {
            cam.enabled = false;
        }

        // Enable the selected camera
        virtualCameras[cameraIndex].enabled = true;
    }
}
