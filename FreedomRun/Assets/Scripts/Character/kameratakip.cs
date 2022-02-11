using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kameratakip : MonoBehaviour
{
    [SerializeField]
    Transform targetObject;
    [SerializeField]
    Vector3 cameraOffset;

    [SerializeField]
    float smoothValue;

    Vector3 targetPosition;
    void LateUpdate() 
    {
        targetPosition = targetObject.position + cameraOffset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothValue);
    }
}
