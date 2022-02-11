using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformHandler : MonoBehaviour
{
    private GameObject mainObject;
    // Start is called before the first frame update
    void Start()
    {
        mainObject = gameObject.GetComponentInParent<PlayerMovements>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mainObject.transform.position;
    }
}
