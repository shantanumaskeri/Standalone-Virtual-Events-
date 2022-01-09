using UnityEngine;

public class MouseConfiguration : MonoBehaviour
{

    public WayfinderMap wayfinderMap;
    public PlayerTeleport playerTeleport;
    public ObjectDetection objectDetection;
    public CursorConfiguration cursorConfiguration;
    public ApplicationManager applicationManager;

    private void FixedUpdate()
    {
        CheckMouseInput();
    }

    private void CheckMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (applicationManager.status)
            {
                case "ObjectDetection":
                    objectDetection.ActivateTableDetection();
                    break;

                default:
                    playerTeleport.ActivateMovement();
                    break;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (applicationManager.status != "Wayfinder" && applicationManager.status != "Login" && applicationManager.status != "ObjectDetection" && applicationManager.status != "Download")
            {
                applicationManager.isControlWithPlayer = false;

                if (!applicationManager.isControlWithPlayer)
                {
                    cursorConfiguration.Initialize();
                }
            }

            if (applicationManager.status == "ObjectDetection")
            {
                objectDetection.DeactivateTableDetection();
            }

            if (applicationManager.status == "Wayfinder")
            {
                wayfinderMap.CompleteWayfinder();
            }
        }
    }

}
