using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb; // Mermi rigidbody
    public float bulletSpeed; // Mermi at�� h�z�
    public float endTime; //Merminin maksimumum gidece�i s�re
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Merminin rigidbodysine ula�mak
        rb.velocity = transform.right * bulletSpeed; // Mermiye sa�a do�ru kuvvet uygulamak
        Destroy(gameObject, endTime); // Belirtilen s�re sonra kendisi silmek
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy" || collision.gameObject.tag =="ground" || collision.gameObject.tag =="box") // E�er enemy ile �arp��t�ysa
        {
            Destroy(gameObject); //�arpma olursa kendisini sil
        }
       /*if (collision.CompareTag("ground"))
        {
            Destroy(gameObject);
        }*/
    }
}
