using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerMovements : MonoBehaviour
{
    public Transform FirePoint; // Mermi ��k�� noktas�
    public GameObject bullet; //Mermi nesnesi
    public GameObject playerGameobject; // Karakter kol gameobject
    int counter = 1; //Karakterin zemine temas� i�in kontrol sayac�
    int diamondcounter = 0;
    public static float speed = 13f; //Karakter h�z�
    Rigidbody2D rb;
    public float jump; //Karakter z�plama y�ksekli�i
    public static bool StartBtnStatus; //Ba�lama esnas�ndaki buton durumu kontrol�
    bool JumpStatus = false; //Oyun ba�lang�� esnas�nda jump kontrol�
    float PlayerlaserLength = 0.1f;
    float PlayerlaserLength2 = 0.05f;
    [SerializeField] private Transform player;
    [SerializeField] private Animator playeranim; // Player animasyon kontrolc�


    Vector2 CharacterStartPosition;
    Vector2 CharacterUpdatePosition;

    private const float Distance = 500f;

    public GameObject StartBtngameo; //Start Buton gameobjeti
    public Text DiamondTxtgameo; //Diamond text sayac� gameobjecti
    private void Awake()
    {
        StartBtnStatus = false;
        rb = GetComponent<Rigidbody2D>();

        CharacterStartPosition = player.position;
        //print(CharacterStartPosition); // Karakter ba�lang�� posisyonunu yazd�r

    }
    private void FixedUpdate()
    {
        if (JumpStatus)
        {
            if (counter == 1)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    playeranim.SetBool("JumpBool", true);
                    rb.velocity = new Vector2(0, jump);
                    counter = 0;
                }
            }
        }
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; // Mouse position- Karakter position
        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        rotationZ = Mathf.Clamp(rotationZ, -35,90); // Kolunu max oynatma de�erleri
        playerGameobject.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ+90);
        
        
    }

    void Shoot()
    {
        Instantiate(bullet,FirePoint.position,FirePoint.rotation);
    }
    void raycastControl()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position+new Vector3(1.5f, -0.5f, 0f), Vector2.right, PlayerlaserLength);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + new Vector3(0, -3.3f, 0f), Vector2.down, PlayerlaserLength2);
        //Debug.DrawRay(transform.position + new Vector3(1.5f, -0.5f, 0f), Vector2.right,Color.cyan, PlayerlaserLength);
        //Debug.DrawRay(transform.position + new Vector3(0, -3.3f, 0f), Vector2.down, Color.black, PlayerlaserLength2);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "enemy" || hit.collider.gameObject.tag == "ground")
            {
                //hit.transform.name == "carry_side" */|| hit.transform.name == "enemy_pink"|| hit.transform.name == "bomb_carry(Clone)"
                SceneManager.LoadScene(0);
                StartBtngameo.SetActive(true);
            }

        }
        if (hit2.collider != null)
        {
            if (hit2.collider.gameObject.tag == "enemy")
            {
                //hit2.transform.name == "carry_side" || hit2.transform.name == "enemy_pink" || hit2.transform.name == "bomb_carry(Clone)"
                SceneManager.LoadScene(0);
                StartBtngameo.SetActive(true);
            }
            if(hit2.collider.gameObject.tag == "ground")
            {
                counter = 1;
                playeranim.SetBool("JumpBool", false);
            }
        }
    }
    private void Update()
    {
        raycastControl();
        if (StartBtnStatus)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (Input.GetButtonDown("Fire1")) //Sol t�k kontrol
            {
                Shoot();
            }
        }
        
        CharacterUpdatePosition = player.position;
        if (Vector2.Distance(CharacterStartPosition, CharacterUpdatePosition)>Distance)
        {
            //print("S�n�r A��ld�"); //2 vector aras�ndaki mesafe > Distance de�erinden b�y�k ise �al���r.
        }
        DiamondTxtgameo.text = diamondcounter.ToString();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "gold")
        {
            Destroy(col.gameObject);
            diamondcounter = (diamondcounter + 1);
        }
        if (col.tag == "bullet-col"|| col.tag == "respawn"|| col.tag == "bomb" || col.tag == "enemyBird")
        {
           SceneManager.LoadScene(0);
           StartBtngameo.SetActive(true);
        }
    }
    public void StartBtn()
    {
        playeranim.SetBool("RunBool", true);
        StartBtnStatus = true;
        StartBtngameo.SetActive(false);
        JumpStatus = true;
    }
}