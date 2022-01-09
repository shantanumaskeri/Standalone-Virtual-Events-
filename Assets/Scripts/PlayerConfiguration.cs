using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerConfiguration : MonoBehaviour
{

    public FirstPersonController firstPersonController;
    public ApplicationManager applicationManager;

    public void Initialize()
    {
        ActivatePlayer();
    }

    public void Terminate()
    {
        DeactivatePlayer();
    }

    private void ActivatePlayer()
    {
        firstPersonController.enabled = true;
    }

    private void DeactivatePlayer()
    {
        firstPersonController.enabled = false;
    }

    public void PositionPlayer()
    {
        switch (applicationManager.status)
        {
            case "Login":
                transform.position = new Vector3(74.6f, 1.635f, -111.8f);
                transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
                break;

            case "ObjectDetection":
                transform.position = new Vector3(81.2f, 1.94f, -239.2f);
                transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
                break;

            case "Download":
                break;
        } 
    }

}
