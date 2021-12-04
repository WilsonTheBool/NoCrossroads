using UnityEngine;
using System.Collections;

public class CameraMovementController : MonoBehaviour
{
    public float cameraSpeed;

    public Rect cameraMoveBounds;

    [SerializeField]
    CameraBoundsController cameraBounds;

    [SerializeField]
    private Camera cam;

    private void Update()
    {

           //Vector3 mousePos = cam.ScreenToViewportPoint(Input.mousePosition);
           // if (!cameraMoveBounds.Contains(mousePos))
           // {
           //     MoveCamera(mousePos);
           // }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        MoveCamera(horizontal, vertical);
    }

    void MoveCamera(Vector3 mousePos)
    {
        Vector3 vel = (mousePos - new Vector3(0.5f, 0.5f))* cameraSpeed * Time.deltaTime;

        cam.transform.Translate(vel);

        cam.transform.position = cameraBounds.GetCamPosition(cam.transform.position);
    }

    void MoveCamera(float horizontalAxis, float verticalAxis)
    {
        Vector3 vel = new Vector3(horizontalAxis, verticalAxis) * cameraSpeed * Time.deltaTime;

        cam.transform.Translate(vel);

        cam.transform.position = cameraBounds.GetCamPosition(cam.transform.position);
    }
}
