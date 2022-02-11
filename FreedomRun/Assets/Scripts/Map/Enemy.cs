using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] spots; // Nesne patrolling pozisyonlarý
    float speed= 10f; // Düþman hýzý
    bool facingRight; // Saða ve sola dönme kontrol
    float waitTime; //bekleme süresi
    float startTime = 1f; //baþlangýç süresini 1.5f belirle
    public int health = 3;

    void Start()
    {
        waitTime = startTime; //wait time ý starttime a eþitle
        facingRight = true; //facingright deðerini true yap
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement(); //Fonksiyon çalýþtýrma
        EnemyHealtyStatus(); //Fonksiyon çalýþtýrma
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
        if (health == 0) //Can sýfýrlanýrsa
        {
            Destroy(gameObject); //Objeyi yok et
            //world_bomb_explosion animasyon ismi
        }
    }
    void EnemyMovement()
    {
        if (facingRight) // bool deðiþkeni true olduðunda gir
        {
            if (waitTime <= 0) // bekleme süresi 0 a küçük veya eþit ise gir
            {
                transform.position = Vector2.MoveTowards(transform.position, spots[1].position, speed * Time.deltaTime); // Baþlangýç noktasýndan ve bitiþ noktasýna haraket ettir.

                if (Vector2.Distance(transform.position, spots[1].position) < 1f) // Karakter ve spot arasýndaki mesafe 1 den küçük ise koþulu çalýþtýr.
                {
                    Flip(); //flip fonksiyonunu çaðýr
                    waitTime = startTime; //waittime deðiþkenini starttime a eþitle
                }
            }
            else// koþul gerçekleþmez ise 
            {
                waitTime -= Time.deltaTime; // waittime deðiþkenini azalt
            }

        }
        else // koþul gerçekleþmez ise
        {
            if (waitTime <= 0) // bekleme süresi 0 a küçük veya eþit ise gir
            {
                transform.position = Vector2.MoveTowards(transform.position, spots[0].position, speed * Time.deltaTime); // Baþlangýç noktasýndan ve bitiþ noktasýna haraket ettir.

                if (Vector2.Distance(transform.position, spots[0].position) < 1f) // Karakter ve spot arasýndaki mesafe 1 den küçük ise koþulu çalýþtýr.
                {
                    Flip();  //flip fonksiyonunu çaðýr
                    waitTime = startTime; //waittime deðiþkenini starttimea eþitle
                }
            }
            else // koþul gerçekleþmez ise
            {
                waitTime -= Time.deltaTime; // waittime deðiþkenini azalt
            }

        }
    }
    void Flip()
    {
        facingRight = !facingRight; //facingright'ý deðiline eþitle

        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y); // Karakterin yüzünü saða ve sola çevirme
    }
}
