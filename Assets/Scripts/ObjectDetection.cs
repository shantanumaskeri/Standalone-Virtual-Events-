using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ObjectDetection : MonoBehaviour
{

    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public GameObject youtubePlayer1;
    public GameObject youtubePlayer2;
    public GameObject youtubePlayer3;
    public GameObject videoPlane;
    public CameraConfiguration cameraConfiguration;

    private Vector3 obj1InitialPosition;
    private Vector3 obj2InitialPosition;
    private Vector3 obj3InitialPosition;

    private List<string> objectList = new List<string>();
    private List<GameObject> playerList = new List<GameObject>();

    private void Start()
    {
        Init();  
    }

    private void Init()
    {
        InitializeVariables();
        PopulateLists();
    }

    private void InitializeVariables()
    {
        obj1InitialPosition = object1.transform.position;
        obj2InitialPosition = object2.transform.position;
        obj3InitialPosition = object3.transform.position;
    }

    private void PopulateLists()
    {
        objectList.Add(object1.name);
        objectList.Add(object2.name);
        objectList.Add(object3.name);

        playerList.Add(youtubePlayer1);
        playerList.Add(youtubePlayer2);
        playerList.Add(youtubePlayer3);
    }

    public void ActivateTableDetection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.tag == "Object")
            {
                ResetPositions();
                ResetVideoPlayers();
                PlaceObjectOnTable(hit.collider.gameObject);
                ShowVideoPlane();
                PlayVideoByObject(hit.collider.gameObject);   
            }
        }
    }

    private void ResetPositions()
    {
        object1.transform.position = obj1InitialPosition;
        object2.transform.position = obj2InitialPosition;
        object3.transform.position = obj3InitialPosition;
    }

    private void ResetVideoPlayers()
    {
        youtubePlayer1.SetActive(false);
        youtubePlayer2.SetActive(false);
        youtubePlayer3.SetActive(false);
    }

    private void PlaceObjectOnTable(GameObject instance)
    {
        instance.transform.position = new Vector3(82.2f, 1.912f, -241.0f);
    }

    private void ShowVideoPlane()
    {
        videoPlane.SetActive(true);
    }

    private void HideVideoPlane()
    {
        videoPlane.SetActive(false);
    }

    private void PlayVideoByObject(GameObject instance)
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            if (instance.name == objectList[i])
            {
                playerList[i].SetActive(true);
            }
        }
    }

    private void StopVideoPlayback()
    {
        youtubePlayer1.GetComponent<VideoPlayer>().Stop();
        youtubePlayer2.GetComponent<VideoPlayer>().Stop();
        youtubePlayer3.GetComponent<VideoPlayer>().Stop();
    }

    public void DeactivateTableDetection()
    {
        ResetPositions();
        ResetVideoPlayers();
        HideVideoPlane();
        StopVideoPlayback();

        cameraConfiguration.SwitchCameraTo("FPS");
    }

}
