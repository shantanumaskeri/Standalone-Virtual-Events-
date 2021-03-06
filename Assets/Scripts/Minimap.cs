using UnityEngine;

public class Minimap : MonoBehaviour
{

    public Transform player;

    private void LateUpdate()
    {
        UpdateMinimap();
    }

    private void UpdateMinimap()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90.0f, player.eulerAngles.y, 0.0f);
    }

}
