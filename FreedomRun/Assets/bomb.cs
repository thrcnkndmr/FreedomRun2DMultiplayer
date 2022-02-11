using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bomb : MonoBehaviour
{
    Rigidbody2D rb; // Mermi rigidbody
    public float bombSpeed; // Mermi atýþ hýzý
    public float endTime; //Merminin maksimumum gideceði süre
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Merminin rigidbodysine ulaþmak
        rb.velocity = -transform.right * bombSpeed;
        Destroy(gameObject, endTime);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
