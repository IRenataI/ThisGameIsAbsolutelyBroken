using UnityEngine;

public class FollowMouseWithAngle : MonoBehaviour
{
    private Camera mainCamera;
    private float zPosition;

    void Start()
    {
        mainCamera = Camera.main;
        zPosition = transform.position.z;
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        mousePosition.z = Mathf.Abs(zPosition - mainCamera.transform.position.z);

        Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        targetPosition.z = zPosition;
        transform.position = targetPosition;
    }
}
