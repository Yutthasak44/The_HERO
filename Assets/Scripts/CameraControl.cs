using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float smoothFactorf;
    public Vector3 MinVal, MaxVal;
    public Vector3 offset;
    public Transform target;
    Camera maincamera;

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 targetPos = target.position + offset;
            Vector3 boundPos = new Vector3(
                                Mathf.Clamp(targetPos.x, MinVal.x, MaxVal.x),
                                Mathf.Clamp(targetPos.y, MinVal.y, MaxVal.y),
                                Mathf.Clamp(targetPos.z, MinVal.z, MaxVal.z));
            Vector3 smoothPos = Vector3.Lerp(transform.position, boundPos, smoothFactorf * Time.fixedDeltaTime);
            transform.position = smoothPos;
        }
    }
}
