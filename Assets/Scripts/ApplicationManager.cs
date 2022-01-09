using UnityEngine;

public class ApplicationManager : MonoBehaviour
{

    [HideInInspector]
    public bool isControlWithPlayer;
    [HideInInspector]
    public bool isCameraTweening;
    [HideInInspector]
    public string status;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        isControlWithPlayer = true;
        isCameraTweening = false;
        status = "Wayfinder";
    }

}
