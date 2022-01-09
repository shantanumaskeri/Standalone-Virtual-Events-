using UnityEngine;

public class CursorConfiguration : MonoBehaviour
{

    public void Initialize()
    {
        ActivateCursor();
    }

    public void Terminate()
    {
        DeactivateCursor();
    }

    private void ActivateCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void DeactivateCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
