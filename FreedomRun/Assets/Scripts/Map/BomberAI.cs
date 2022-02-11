using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberAI : MonoBehaviour
{
    public float laserLength = 20f; //raycast ýþýn uzunluðu
    public int health = 3; // Karakter can
    public int speed; //Karakter hýz
    [SerializeField] private Animator myDeadCarry; //Ölme animasyonu
    public GameObject carry1, carry2, carry3, bomb; // Karakter gameobjectleri
    public Transform carry_spot_Transform; // Karakterin gideceði spot koordinatý
     
    float timeLeft = -1f; // Zaman deðiþkeni
    void Start()
    {
        
    }

    private void FixedUpdate()
    {

    }
    void Update()
    {
        
        BomberAIRayCast();
        healtControl();
    }
    void healtControl() // Karakter can kontrol, animasyon oynatma ve karakteri silme
    {
        if (health == 0)
        {
            carry1.SetActive(false);
            carry2.SetActive(false);
            carry3.SetActive(false);
            myDeadCarry.SetBool("carry", true);
            Destroy(gameObject,0.3f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet" ) 
        {
            health--;
        }
    }
    void BomberAIRayCast() //2 farklý ýþýn ile çarpma kontrolü 
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(-5.5f, 5f, 0f), -Vector2.right, laserLength);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + new Vector3(-5.5f, 8f, 0f), -Vector2.right, laserLength);

        //Debug.DrawRay(transform.position + new Vector3(-5.5f, 5f, 0f), -Vector2.right, Color.blue,laserLength);
        //Debug.DrawRay(transform.position + new Vector3(-5.5f, 8f, 0f), -Vector2.right, Color.blue, laserLength);
        if (hit.collider != null )
        {

            if (hit.transform.name == "Player")
            {
                timeLeft -= Time.deltaTime;
                
                transform.position = Vector2.MoveTowards(transform.position, carry_spot_Transform.position, speed * Time.deltaTime);
                if (timeLeft<0)
                {
                    Instantiate(bomb, transform.position + new Vector3(-4f, 0f, 0f), Quaternion.identity);
                    timeLeft = 2f;
                }
                
            }
        }
        if (hit2.collider != null)
        {
            if (hit2.transform.name == "Player")
            {
                timeLeft -= Time.deltaTime;

                transform.position = Vector2.MoveTowards(transform.position, carry_spot_Transform.position, speed * Time.deltaTime);
                if (timeLeft < 0)
                {
                    Instantiate(bomb, transform.position + new Vector3(-1f, 0f, 0f), Quaternion.identity);
                    timeLeft = 2f;
                }
            }
        }
    }
}
