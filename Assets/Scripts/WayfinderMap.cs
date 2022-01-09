using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WayfinderMap : MonoBehaviour
{

    public GameObject[] exhibitionModules;
    public GameObject[] dialogBoxes;
    public Button[] moduleButtons;
    public Transform[] stallViews;
    public GameObject wayfinderUI;
    public GameObject minimapUI;
    public GameObject listener;
    public GameObject loginDesk;
    public GameObject photoBooth;
    public GameObject showReel;
    public GameObject videoStream;
    public GameObject webAR;
    public GameObject instructions;
    public GameObject playerWaypoint;
    public Camera wayfinderCamera;
    public CameraOrbit cameraOrbit;
    public PlayerConfiguration playerConfiguration;
    public CursorConfiguration cursorConfiguration;
    public ApplicationManager applicationManager;
    public float transitionSpeed;

    private Transform currentView;
    
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        DisableControls();
        UpdateWayfinderMap(true);
        UpdateDialogBoxes(false);

        currentView = wayfinderCamera.transform;
        cameraOrbit.PrepareTarget(exhibitionModules[exhibitionModules.Length - 1]);
    }

    private void EnableControls()
    {
        playerConfiguration.Initialize();
        cursorConfiguration.Terminate();
    }

    private void DisableControls()
    {
        playerConfiguration.Terminate();
        cursorConfiguration.Initialize();
    }

    public void CompleteWayfinder()
    {
        EnableControls();
        UpdateWayfinderMap(false);
        UpdateDialogBoxes(false);
        ToggleUIComponents();

        applicationManager.status = "Login";
    }

    public void HighlightSelectedModule(int id)
    {
        UpdateButtonColors();
        UpdateDialogBoxes(false);

        currentView = stallViews[id];
        applicationManager.isCameraTweening = true;

        StopAllCoroutines();
        StartCoroutine(CameraAnimationComplete(id));
    }

    private void UpdateButtonColors()
    {
        for (int j = 0; j < moduleButtons.Length; j++)
        {
            moduleButtons[j].interactable = true;
        }

        GameObject instance = EventSystem.current.currentSelectedGameObject;
        instance.GetComponent<Button>().interactable = false;
    }

    private void UpdateDialogBoxes(bool value)
    {
        for (int i = 0; i < dialogBoxes.Length; i++)
        {
            dialogBoxes[i].SetActive(value);
        }
    }

    private void UpdateWayfinderMap(bool value)
    {
        wayfinderCamera.gameObject.SetActive(value);
        instructions.SetActive(value);
    }

    private void ToggleUIComponents()
    {
        wayfinderUI.SetActive(false);
        minimapUI.SetActive(true);
        playerWaypoint.SetActive(true);
    }

    private void LateUpdate()
    {
        AnimateCamera();
    }

    private void AnimateCamera()
    {
        if (applicationManager.isCameraTweening)
        {
            wayfinderCamera.transform.position = Vector3.Lerp(wayfinderCamera.transform.position, currentView.position, Time.deltaTime * transitionSpeed);

            Vector3 currentAngle = new Vector3(Mathf.LerpAngle(wayfinderCamera.transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed), Mathf.LerpAngle(wayfinderCamera.transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed), Mathf.LerpAngle(wayfinderCamera.transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed));

            wayfinderCamera.transform.eulerAngles = currentAngle;
        }
    }

    private IEnumerator CameraAnimationComplete(int id)
    {
        yield return new WaitForSeconds(transitionSpeed);

        applicationManager.isCameraTweening = false;
        dialogBoxes[id].SetActive(true);

        cameraOrbit.PrepareTarget(exhibitionModules[id]);
    }

}
