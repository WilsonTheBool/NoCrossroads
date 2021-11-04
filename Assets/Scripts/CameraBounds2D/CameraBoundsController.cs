using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraBoundsController : MonoBehaviour
{
    [HideInInspector]
    public CameraBounds2D CameraBounds2D;

    private void Start()
    {
        CameraBounds2D = FindObjectOfType<CameraBounds2D>();

        CameraBounds2D.Initialize(GetComponent<Camera>());    
    }

    public Vector3 GetCamPosition(Vector3 camPos)
    {
        Vector3 pos = new Vector3(Mathf.Clamp(camPos.x, CameraBounds2D.maxXlimit.x, CameraBounds2D.maxXlimit.y),
            Mathf.Clamp(camPos.y, CameraBounds2D.maxYlimit.x, CameraBounds2D.maxYlimit.y), camPos.z);
        

        return pos;
    }
}
