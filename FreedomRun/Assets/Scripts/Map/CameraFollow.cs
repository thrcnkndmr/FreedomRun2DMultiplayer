using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float CameraSpeed = 13f;
    public GameObject Camera;
    public GameObject Player;

    Vector3 newPosition;

    
    
    //Vector3 CameraPosition;
    void Start()
    {
        newPosition = Camera.transform.position - Player.transform.position;
    }
    void Update()   
    {
        transform.position = Player.transform.position + newPosition;
        /*if (PlayerMovements.StartBtnStatus == true)
        {
            Camera.transform.Translate(Vector2.right * CameraSpeed * Time.deltaTime);     
        }*/
    }


}
