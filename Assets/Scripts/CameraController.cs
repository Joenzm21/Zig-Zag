using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset;
    public float SmoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.Windowed);
        Offset = transform.position - Target.position;
    }
    private void LateUpdate()
    {
        Vector3 targetPosition = Target.position + Offset;
        targetPosition.y = transform.position.y;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);
    }
}
