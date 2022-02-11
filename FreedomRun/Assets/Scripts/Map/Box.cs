using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    /* public GameObject image1, image2, image3, image4, image5, image6, image7, image8, image9,
     image10, image11, image12, image13, image14, image15, image16;*/

    //Collider2D boxObject;
    //int speed = 20;
    public GameObject boxObject;
    Rigidbody2D rb;


    void Start()
    {
        //boxObject = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            
           
           /* transform.Translate(Vector2.up * speed * Time.deltaTime);
            boxObject.isTrigger = true;
            image1.AddComponent<Rigidbody2D>();
            image2.AddComponent<Rigidbody2D>();
            image3.AddComponent<Rigidbody2D>();
            image4.AddComponent<Rigidbody2D>();
            image5.AddComponent<Rigidbody2D>();
            image6.AddComponent<Rigidbody2D>();
            image7.AddComponent<Rigidbody2D>();
            image8.AddComponent<Rigidbody2D>();
            image9.AddComponent<Rigidbody2D>();
            image10.AddComponent<Rigidbody2D>();
            image11.AddComponent<Rigidbody2D>();
            image12.AddComponent<Rigidbody2D>();
            image13.AddComponent<Rigidbody2D>();
            image14.AddComponent<Rigidbody2D>();
            image15.AddComponent<Rigidbody2D>();
            image16.AddComponent<Rigidbody2D>();*/
        }
    }
}
