using UnityEngine;

public class CameraOrbit : MonoBehaviour
{

    public GameObject target;
    public ApplicationManager applicationManager;

    private float speedMod = 10.0f;
    private Vector3 point;

    public void PrepareTarget(GameObject instance)
    {
        target = instance;
        point = target.transform.position;
        transform.LookAt(point);
    }

    private void Update()
    {
        OrbitAroundTarget();
    }

    private void OrbitAroundTarget()
    {
        if (!applicationManager.isCameraTweening)
        {
            if (target == null)
                return;

            transform.RotateAround(target.transform.position, Vector3.up, speedMod * Time.deltaTime);
        }
    }

}