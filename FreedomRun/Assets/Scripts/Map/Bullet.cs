using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb; // Mermi rigidbody
    public float bulletSpeed; // Mermi atýþ hýzý
    public float endTime; //Merminin maksimumum gideceði süre
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Merminin rigidbodysine ulaþmak
        rb.velocity = transform.right * bulletSpeed; // Mermiye saða doðru kuvvet uygulamak
        Destroy(gameObject, endTime); // Belirtilen süre sonra kendisi silmek
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy" || collision.gameObject.tag =="ground" || collision.gameObject.tag =="box") // Eðer enemy ile çarpýþtýysa
        {
            Destroy(gameObject); //Çarpma olursa kendisini sil
        }
       /*if (collision.CompareTag("ground"))
        {
            Destroy(gameObject);
        }*/
    }
}
