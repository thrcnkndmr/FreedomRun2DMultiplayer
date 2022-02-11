using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bomb : MonoBehaviour
{
    Rigidbody2D rb; // Mermi rigidbody
    public float bombSpeed; // Mermi at�� h�z�
    public float endTime; //Merminin maksimumum gidece�i s�re
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Merminin rigidbodysine ula�mak
        rb.velocity = -transform.right * bombSpeed;
        Destroy(gameObject, endTime);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
