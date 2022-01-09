using Affdex;
using SimpleJSON;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Registration : MonoBehaviour
{

	public InputField codeField;
	public Text codeError;
    public Text faceError;
    public Text formMessage;
    public Image eventPass;
    public Text passDetails;
    public GameObject loginTypePanel;
    public GameObject faceLoginPanel;
    public GameObject codeLoginPanel;
    public GameObject welcomePanel;
    public GameObject cameraObject;
    public GameObject dummyObject;
    public CameraInput cameraInput;
    public WebcamView webcamView;
    public CameraConfiguration cameraConfiguration;
    public Listener listener;
    public BoxCollider doorTrigger;

    private string userCode;
    private string hostURL = "http://rayqube.com/projects/virtual_event_backend/";
  
    public void ShowLoginTypePanel()
    {
        loginTypePanel.SetActive(true);
    }

    private void HideLoginTypePanel()
    {
        loginTypePanel.SetActive(false);
    }

    private void ShowWelcomePanel()
    {
        welcomePanel.SetActive(true);
    }

    public void HideWelcomePanel()
    {
        welcomePanel.SetActive(false);
    }

    public void InitializeCodeLogin()
    {
        HideLoginTypePanel();
        ShowCodeLoginPanel();
    }

    public void InitializeFaceLogin()
    {
        HideLoginTypePanel();
        ShowFaceLoginPanel();
        ActivateCameraComponents();
    }

    private void ActivateCameraComponents()
    {
        cameraInput.ActivateWebcam();
        webcamView.ActivateView();
        listener.ActivateListener();
    }

    private void DeactivateCameraComponents()
    {
        cameraInput.DeactivateWebcam();
        webcamView.DeactivateView();
        listener.DeactivateListener();
    }

    private void ShowFaceLoginPanel()
    {
        faceLoginPanel.SetActive(true);
        webcamView.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    private void HideFaceLoginPanel()
    {
        faceLoginPanel.SetActive(false);
        webcamView.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void ShowCodeLoginPanel()
    {
        codeLoginPanel.SetActive(true);
    }

    private void HideCodeLoginPanel()
    {
        codeLoginPanel.SetActive(false);
    }

    public void LoginByUniqueCode()
    {
        userCode = codeField.text;

        if (IsValidUniqueCode(userCode))
        {
            codeError.text = "";

            StartLogin("code", null);
        }
        else
        {
            codeError.text = "Please enter a valid code.";

            StartCoroutine(ClearMessage(codeError));
        }
    }

    public void LoginByFace(Texture2D snap)
    {
        StartLogin("face", snap);
	}

    private IEnumerator ClearMessage(Text message)
    {
        yield return new WaitForSeconds(2.0f);

        message.text = "";
    }

    private bool IsValidUniqueCode(string code)
    {
        if (code.Length != 13)
            return false;

        if (code.IndexOf("@") >= 0 || code.IndexOf(".") >= 0 || code.IndexOf("!") >= 0 || code.IndexOf("#") >= 0 || code.IndexOf("$") >= 0 || code.IndexOf("%") >= 0 || code.IndexOf("^") >= 0 || code.IndexOf("&") >= 0 || code.IndexOf("*") >= 0 || code.IndexOf("(") >= 0 || code.IndexOf(")") >= 0 || code.IndexOf("-") >= 0 || code.IndexOf("=") >= 0 || code.IndexOf("+") >= 0 || code.IndexOf("[") >= 0 || code.IndexOf("{") >= 0 || code.IndexOf("]") >= 0 || code.IndexOf("}") >= 0 || code.IndexOf(":") >= 0 || code.IndexOf(";") >= 0 || code.IndexOf("'") >= 0 || code.IndexOf("|") >= 0 || code.IndexOf(",") >= 0 || code.IndexOf("<") >= 0 || code.IndexOf(">") >= 0 || code.IndexOf("/") >= 0 || code.IndexOf("?") >= 0)
            return false;

        return true;
    }

    private void StartLogin(string type, Texture2D snap)
    {
        switch (type)
        {
            case "code":
                StartCoroutine(CheckUniqueCode());
                break;

            case "face":
                StartCoroutine(CheckFace(snap));
                break;
        }
    }

    private IEnumerator CheckUniqueCode()
    {
        yield return new WaitForEndOfFrame();
        
        WWWForm form = new WWWForm();
        form.AddField("code", userCode);

        using (var w = UnityWebRequest.Post(hostURL + "/api/user/identify_qr", form))
        {
            w.SetRequestHeader("Accept", "/");
            w.SetRequestHeader("Accept-Encoding", "*");
            w.SetRequestHeader("User-Agent", "runscope/0.1");
            
            yield return w.SendWebRequest();
            
            if (w.isNetworkError || w.isHttpError)
            {
                codeError.text = "Login Failed due to " + w.error;

                StartCoroutine(RetryLogin("code"));
            }
            else
            {
                string response = w.downloadHandler.text;
                JSONNode data = JSON.Parse(response);
                string status = data["STATUS"].Value;
                string message = data["MESSAGE"].Value;
                string name = data["data"]["NAME"].Value;
                string entity = data["data"]["ENTITY"].Value;
                string position = data["data"]["POSITION"].Value;

                if (status == "SUCCESS")
                {
                    HideCodeLoginPanel();
                    ShowWelcomePanel();
                    GetEventVirtualPass(name, entity, position);
                    StartCoroutine(CompleteLoginProcess());
                }
                else
                {
                    codeError.text = "Login Failed due to " + message;

                    StartCoroutine(RetryLogin("code"));
                }
            }
        }
    }

    private IEnumerator CheckFace(Texture2D snap)
    {
        yield return new WaitForEndOfFrame();

        byte[] bytes = snap.EncodeToJPG();
        
        WWWForm form = new WWWForm();
        form.AddBinaryData("image", bytes, Application.dataPath + "/../Photos/Login/Snap_Login.jpg", "image/jpg");

        using (var w = UnityWebRequest.Post(hostURL + "/api/user/identify_face", form))
        {
            Debug.Log("request sent");
            w.SetRequestHeader("Accept", "/");
            w.SetRequestHeader("Accept-Encoding", "*");
            w.SetRequestHeader("User-Agent", "runscope/0.1");
            
            yield return w.SendWebRequest();
            
            if (w.isNetworkError || w.isHttpError)
            {
                faceError.text = "Login Failed due to " + w.error;

                StartCoroutine(RetryLogin("face"));
            }
            else
            {
                string response = w.downloadHandler.text;
                JSONNode data = JSON.Parse(response);
                string status = data["STATUS"].Value;
                string message = data["MESSAGE"].Value;
                string name = data["data"]["NAME"].Value;
                string entity = data["data"]["ENTITY"].Value;
                string position = data["data"]["POSITION"].Value;

                if (status == "SUCCESS")
                {
                    DeactivateCameraComponents();
                    HideFaceLoginPanel();
                    ShowWelcomePanel();
                    GetEventVirtualPass(name, entity, position);
                    StartCoroutine(CompleteLoginProcess());
                }
                else
                {
                    faceError.text = "Login Failed due to " + message;

                    StartCoroutine(RetryLogin("face"));
                }
            }
        }
    }

    private IEnumerator RetryLogin(string loginType)
    {
        yield return new WaitForSeconds(2.0f);

        switch (loginType)
        {
            case "code":

                codeError.text = "";

                HideCodeLoginPanel();
                ShowLoginTypePanel();

                break;

            case "face":

                faceError.text = "";

                DeactivateCameraComponents();
                HideFaceLoginPanel();
                ShowLoginTypePanel();

                break;
        }
    }

    private void GetEventVirtualPass(string name, string entity, string position)
    {
        eventPass.gameObject.SetActive(true);
        passDetails.text = "Name: " + name + "\nCompany: " + entity + "\nDesignation: " + position;

        formMessage.text = "Welcome " + name + "\n\nHere's your pass.\nYou may now proceed towards the exhibition stalls\n\nThank You!";
    }

    private IEnumerator CompleteLoginProcess()
    {
        cameraObject.GetComponent<MeshRenderer>().enabled = true;
        cameraObject.transform.localPosition = dummyObject.transform.localPosition;
        cameraObject.transform.localRotation = dummyObject.transform.localRotation;
        cameraObject.transform.localScale = dummyObject.transform.localScale;

        doorTrigger.enabled = true;

        yield return new WaitForSeconds(2.0f);

        dummyObject.SetActive(false);

        cameraConfiguration.SwitchCameraTo("FPS");
    }
}
