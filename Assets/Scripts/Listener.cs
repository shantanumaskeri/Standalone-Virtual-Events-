using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using Affdex;

public class Listener : ImageResultsListener
{

    public Image previewImage;
    public Image previewImage2;
    public GameObject instructions;
    public CameraInput cameraInput;
    public Registration registration;
    public ApplicationManager applicationManager;

    private bool isSmilingOn;
    private float smileNumber;

    public void ActivateListener()
    {
        isSmilingOn = true;
    }

    public void DeactivateListener()
    {
        isSmilingOn = false;
    }

    public override void onFaceFound(float timestamp, int faceId)
    {
        if (applicationManager.status == "Login")
        {
            Debug.Log("Found the face");

            StartCoroutine(CaptureScreenshot());
        }
    }

    public override void onFaceLost(float timestamp, int faceId)
    {
        Debug.Log("Lost the face");
    }
    
    public override void onImageResults(Dictionary<int, Face> faces)
    {
        if (applicationManager.status == "PhotoBooth")
        {
            //Debug.Log("onImageResults " + isSmilingOn + " : " + faces.Count);
            if (isSmilingOn)
            {
                if (faces.Count > 0)
                {
                    smileNumber = faces[0].Expressions[Affdex.Expressions.Smile];
                    if (smileNumber > 50)
                    {
                        StartCoroutine(CaptureScreenshot());
                    }
                }
            }
        }
    }

    private IEnumerator CaptureScreenshot()
    {
        if (applicationManager.status == "PhotoBooth")
        {
            previewImage.gameObject.SetActive(true);
            instructions.SetActive(false);
        }
        if (applicationManager.status == "Login")
        {
            previewImage2.gameObject.SetActive(true);
        }

        Texture2D snap = cameraInput.SavePhotoToDisk();

        DeactivateListener();

        yield return new WaitForSeconds(2.0f);

        if (applicationManager.status == "PhotoBooth")
        {
            previewImage.gameObject.SetActive(false);
            instructions.SetActive(true);

            ActivateListener();
        }
        if (applicationManager.status == "Login")
        {
            previewImage2.gameObject.SetActive(false);

            registration.LoginByFace(snap);
        }
    }

}