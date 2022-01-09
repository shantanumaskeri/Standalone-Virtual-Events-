using UnityEngine;

public class CameraConfiguration : MonoBehaviour
{

    public GameObject firstPersonCamera;
    public CursorConfiguration cursorConfiguration;
    public PlayerConfiguration playerConfiguration;
    public ApplicationManager applicationManager;

    public void SwitchCameraTo(string value)
    {
        switch (value)
        {
            case "UI":

                PositionCamera();
                playerConfiguration.PositionPlayer();
                playerConfiguration.Terminate();
                cursorConfiguration.Initialize();
                
                break;

            case "FPS":
                
                playerConfiguration.Initialize();
                cursorConfiguration.Terminate();
                
                break;
        }
    }

    private void PositionCamera()
    {
        switch (applicationManager.status)
        {
            case "Login":
                firstPersonCamera.transform.localRotation = Quaternion.identity;
                break;

            case "ObjectDetection":
                firstPersonCamera.transform.localPosition = new Vector3(-0.1615449f, 2.103412f, 1.482559f);
                firstPersonCamera.transform.localEulerAngles = new Vector3(67.6f, -0.378f, 0.0f);
                break;

            case "Download":
                firstPersonCamera.transform.localRotation = Quaternion.identity;
                break;
        }
    }

}
