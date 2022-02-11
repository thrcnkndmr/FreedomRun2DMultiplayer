using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerControl : MonoBehaviour
{
    public Transform FirePoint; // Mermi çýkýþ noktasý
    public GameObject bullet; //Mermi nesnesi
    int counter = 1; //Karakterin zemine temasý için kontrol sayacý
    int diamondcounter = 0;
    public float speed = 10f; //Karakter hýzý
    Rigidbody2D rb;
    public float jump; //Karakter zýplama yüksekliði
    bool StartBtnStatus = false; //Baþlama esnasýndaki buton durumu kontrolü
    bool JumpStatus = false; //Oyun baþlangýç esnasýnda jump kontrolü
    float PlayerlaserLength = 0.1f;
    float PlayerlaserLength2 = 0.1f;
    [SerializeField] private Transform player;


    Vector2 CharacterStartPosition;
    Vector2 CharacterUpdatePosition;

    private const float Distance = 500f;

    public GameObject StartBtngameo; //Start Buton gameobjeti
    public Text DiamondTxtgameo; //Diamond text sayacý gameobjecti
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        CharacterStartPosition = player.position;
        //print(CharacterStartPosition); // Karakter baþlangýç posisyonunu yazdýr

    }
    private void FixedUpdate()
    {
        if (JumpStatus)
        {
            if (counter == 1)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    rb.velocity = new Vector2(0, jump);
                    counter = 0;
                }
            }
        }
    }
    void Shoot()
    {
        Instantiate(bullet, FirePoint.position, FirePoint.rotation);
    }
    void raycastControl()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(1.6f, -0.5f, 0f), Vector2.right, PlayerlaserLength);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + new Vector3(0, -3.1f, 0f), Vector2.down, PlayerlaserLength2);
        //Debug.DrawRay(transform.position + new Vector3(0, -3.1f, 0f), Vector2.down, Color.cyan, PlayerlaserLength2);
        //Debug.DrawRay(transform.position + new Vector3(1.6f, -0.5f, 0f), Vector2.right, Color.black, PlayerlaserLength);
        if (hit.collider != null)
        {
            if (hit.transform.name == "carry_side" || hit.transform.name == "enemy_pink" || hit.transform.name == "bomb_carry(Clone)" || hit.collider.gameObject.tag == "ground")
            {
                SceneManager.LoadScene(0);
                StartBtngameo.SetActive(true);
            }

        }
        if (hit2.collider != null)
        {
            if (hit2.transform.name == "carry_side" || hit2.transform.name == "enemy_pink" || hit2.transform.name == "bomb_carry(Clone)")
            {
                SceneManager.LoadScene(0);
                StartBtngameo.SetActive(true);
            }
            if (hit2.collider.gameObject.tag == "ground")
            {
                counter = 1;
            }
        }
    }
    private void Update()
    {
        raycastControl();
        if (StartBtnStatus)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (Input.GetButtonDown("Fire1")) //Sol týk kontrol
            {
                Shoot();
            }
        }

        CharacterUpdatePosition = player.position;
        if (Vector2.Distance(CharacterStartPosition, CharacterUpdatePosition) > Distance)
        {
            //print("Sýnýr Aþýldý"); //2 vector arasýndaki mesafe > Distance deðerinden büyük ise çalýþýr.
        }
       // DiamondTxtgameo.text = diamondcounter.ToString();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "gold")
        {
            Destroy(col.gameObject);
            diamondcounter = (diamondcounter + 1);
        }
        if (col.tag == "bullet-col" || col.tag == "respawn" || col.tag == "bomb")
        {
            SceneManager.LoadScene(0);
            StartBtngameo.SetActive(true);
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        /*if (col.tag == "ground")
         {
             counter = 1;
         }*/
    }
    void OnTriggerExit2D(Collider2D col)
    {
        //counter = 0;
    }
    public void StartBtn()
    {
        StartBtnStatus = true;
        StartBtngameo.SetActive(false);
        JumpStatus = true;
    }
}