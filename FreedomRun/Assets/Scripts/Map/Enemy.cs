using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] spots; // Nesne patrolling pozisyonlar�
    float speed= 10f; // D��man h�z�
    bool facingRight; // Sa�a ve sola d�nme kontrol
    float waitTime; //bekleme s�resi
    float startTime = 1f; //ba�lang�� s�resini 1.5f belirle
    public int health = 3;

    void Start()
    {
        waitTime = startTime; //wait time � starttime a e�itle
        facingRight = true; //facingright de�erini true yap
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement(); //Fonksiyon �al��t�rma
        EnemyHealtyStatus(); //Fonksiyon �al��t�rma
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            health--;
        }
    }
    void EnemyHealtyStatus()
    {
        if (health == 0) //Can s�f�rlan�rsa
        {
            Destroy(gameObject); //Objeyi yok et
            //world_bomb_explosion animasyon ismi
        }
    }
    void EnemyMovement()
    {
        if (facingRight) // bool de�i�keni true oldu�unda gir
        {
            if (waitTime <= 0) // bekleme s�resi 0 a k���k veya e�it ise gir
            {
                transform.position = Vector2.MoveTowards(transform.position, spots[1].position, speed * Time.deltaTime); // Ba�lang�� noktas�ndan ve biti� noktas�na haraket ettir.

                if (Vector2.Distance(transform.position, spots[1].position) < 1f) // Karakter ve spot aras�ndaki mesafe 1 den k���k ise ko�ulu �al��t�r.
                {
                    Flip(); //flip fonksiyonunu �a��r
                    waitTime = startTime; //waittime de�i�kenini starttime a e�itle
                }
            }
            else// ko�ul ger�ekle�mez ise 
            {
                waitTime -= Time.deltaTime; // waittime de�i�kenini azalt
            }

        }
        else // ko�ul ger�ekle�mez ise
        {
            if (waitTime <= 0) // bekleme s�resi 0 a k���k veya e�it ise gir
            {
                transform.position = Vector2.MoveTowards(transform.position, spots[0].position, speed * Time.deltaTime); // Ba�lang�� noktas�ndan ve biti� noktas�na haraket ettir.

                if (Vector2.Distance(transform.position, spots[0].position) < 1f) // Karakter ve spot aras�ndaki mesafe 1 den k���k ise ko�ulu �al��t�r.
                {
                    Flip();  //flip fonksiyonunu �a��r
                    waitTime = startTime; //waittime de�i�kenini starttimea e�itle
                }
            }
            else // ko�ul ger�ekle�mez ise
            {
                waitTime -= Time.deltaTime; // waittime de�i�kenini azalt
            }

        }
    }
    void Flip()
    {
        facingRight = !facingRight; //facingright'� de�iline e�itle

        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y); // Karakterin y�z�n� sa�a ve sola �evirme
    }
}
