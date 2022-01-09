using UnityEngine;

public class DoorAnimation : MonoBehaviour
{

    public float speed = 5.0f;

    private bool isDoorOpen;

    private void Start()
    {
        isDoorOpen = false;
    }

    private void Update()
    {
        if (isDoorOpen)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(-90.0f, 0.0f, 180.0f)), speed);
        }
    }

    public void OpenDoor()
    {
        isDoorOpen = true;
    }

}
