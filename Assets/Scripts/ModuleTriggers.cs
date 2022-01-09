using Affdex;
using UnityEngine;

public class ModuleTriggers : MonoBehaviour
{

    public GameObject youtubePlayer;
    public GameObject triggerPoint;
    public ApplicationManager applicationManager;
    public WebcamView webcamView;
    public Listener listener;
    public CameraConfiguration cameraConfiguration;
    public CameraInput cameraInput;
    public Registration registration;
    public DoorAnimation doorAnimation;
    
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        triggerPoint.SetActive(true);

        if (youtubePlayer == null)
            return;

        youtubePlayer.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            triggerPoint.SetActive(false);
            applicationManager.status = gameObject.tag;
            
            if (gameObject.tag == "VideoStream")
            {
                youtubePlayer.SetActive(true);
            }
            else if (gameObject.tag == "PhotoBooth")
            {
                cameraInput.ActivateWebcam();
                webcamView.ActivateView();
                listener.ActivateListener();
            }
            else if (gameObject.tag == "Login")
            {
                registration.ShowLoginTypePanel();
                cameraConfiguration.SwitchCameraTo("UI");
            }
            else if (gameObject.tag == "Door")
            {
                doorAnimation.OpenDoor();
            }
            else if (gameObject.tag == "ObjectDetection")
            {
                cameraConfiguration.SwitchCameraTo("UI");
            }
            else if (gameObject.tag == "Download")
            {
                // show ui for downloading PDF's
                cameraConfiguration.SwitchCameraTo("UI");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (gameObject.tag == "VideoStream")
            {
                triggerPoint.SetActive(true);
                triggerPoint.GetComponent<Animation>().Play();

                youtubePlayer.SetActive(false);
            }
            else if (gameObject.tag == "PhotoBooth")
            {
                triggerPoint.SetActive(true);
                triggerPoint.GetComponent<Animation>().Play();

                cameraInput.DeactivateWebcam();
                webcamView.DeactivateView();
                listener.DeactivateListener();
            }
            else if (gameObject.tag == "ObjectDetection" || gameObject.tag == "Download")
            {
                applicationManager.status = "";
                triggerPoint.SetActive(true);
                triggerPoint.GetComponent<Animation>().Play();
            }
            else if (gameObject.tag == "Login")
            {
                registration.HideWelcomePanel();
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
            else if (gameObject.tag == "Door")
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

}
