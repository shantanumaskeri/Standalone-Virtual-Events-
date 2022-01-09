using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{

    public GameObject movementPoint;
    public PlayerConfiguration playerConfiguration;
    public CursorConfiguration cursorConfiguration;
    public ApplicationManager applicationManager;

    public void ActivateMovement()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.tag == "Walkable")
            {
                MovePlayerToPoint(hit.point);
            }
        }
    }

    private void FixedUpdate()
    {
        ConfigureMovementPoint();
    }

    private void ConfigureMovementPoint()
    {
        movementPoint.SetActive(!applicationManager.isControlWithPlayer);

        if (!applicationManager.isControlWithPlayer)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.tag == "Walkable")
                {
                    movementPoint.SetActive(true);
                }
                else
                {
                    movementPoint.SetActive(false);
                }
            }
        }
        
        Vector3 temp = Input.mousePosition;
        temp.z = 6f;
       
        movementPoint.transform.position = Camera.main.ScreenToWorldPoint(temp);
    }

    private void MovePlayerToPoint(Vector3 point)
    {
        playerConfiguration.Terminate();

        transform.position = new Vector3(point.x, transform.position.y, point.z);

        playerConfiguration.Initialize();
        cursorConfiguration.Terminate();

        applicationManager.isControlWithPlayer = true;
    }     

}
